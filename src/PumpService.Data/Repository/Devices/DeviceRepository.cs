using PumpService.Core;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Devices;

namespace PumpService.Data.Repository.Devices
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        #region Fields

        private readonly IDeviceTypeRepository _deviceTypeRepository;

        #endregion Fields

        #region Constructor

        public DeviceRepository(IDbContext context, IDeviceTypeRepository deviceTypeRepository)
            : base(context)
        {
            _deviceTypeRepository = deviceTypeRepository;
        }

        #endregion Constructor

        #region Methods

        public IPagedList<Device> SearchDevices(DeviceSearch deviceSearch)
        {
            var query = Table;
            AddQueryCriteria(query, deviceSearch);

            return new PagedList<Device>(query, deviceSearch.Page - 1, deviceSearch.PageSize);
        }

        public IList<Device> SearchAllDevices(DeviceSearch deviceSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, deviceSearch);

            return query.ToList();
        }

        public List<Device> GetAllDevices()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        public List<Device> GetDevices(LookupTable deviceTypeGroup, LookupTable deviceTypeType)
        {
            var query = from device in Table
                        join deviceType in _deviceTypeRepository.Table on device.DeviceTypeId equals deviceType.Id
                        where deviceType.TypeId == deviceTypeType.Id && deviceType.GroupId == deviceTypeGroup.Id
                        select device;

            //query = query.Join(Table, x => x.Id, y => y.DeviceTypeId,
            //        (x, y) => new { DeviceType = x, Device = y })
            //        .Where(z => z.Device.DeviceType.TypeId == deviceTypeType.Id && z.Device.DeviceType.GroupId == deviceTypeGroup.Id)
            //        .Select(z => z.Device);

            return query.ToList();
        }

        public List<Device> GetChildDevices(Device parentDevice, LookupTable deviceTypeGroup, LookupTable deviceTypeType)
        {
            var query = from device in Table
                        join deviceType in _deviceTypeRepository.Table on device.DeviceTypeId equals deviceType.Id
                        where deviceType.TypeId == deviceTypeType.Id && deviceType.GroupId == deviceTypeGroup.Id && device.ParentDeviceId == parentDevice.Id
                        select device;

            return query.ToList();
        }

        private IQueryable<Device> AddQueryCriteria(IQueryable<Device> query, DeviceSearch deviceSearch)
        {
            if (!string.IsNullOrEmpty(deviceSearch.Name))
                query = query.Where(s => s.Name.Contains(deviceSearch.Name));

            return LinqHelper<Device>.OrderBy(query, deviceSearch.OrderMember, deviceSearch.OrderByAsc);
        }

        #endregion Methods
    }
}
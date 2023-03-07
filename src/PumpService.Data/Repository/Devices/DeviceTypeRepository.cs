using PumpService.Core;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Devices;

namespace PumpService.Data.Repository.Devices
{
    public class DeviceTypeRepository : BaseRepository<DeviceType>, IDeviceTypeRepository
    {
        #region Constructor

        public DeviceTypeRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<DeviceType> SearchDeviceTypes(DeviceTypeSearch deviceTypeSearch)
        {
            var query = Table;
            AddQueryCriteria(query, deviceTypeSearch);

            return new PagedList<DeviceType>(query, deviceTypeSearch.Page - 1, deviceTypeSearch.PageSize);
        }

        public IList<DeviceType> SearchAllDeviceTypes(DeviceTypeSearch deviceTypeSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, deviceTypeSearch);

            return query.ToList();
        }

        public List<DeviceType> GetAllDeviceTypes()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<DeviceType> AddQueryCriteria(IQueryable<DeviceType> query, DeviceTypeSearch deviceTypeSearch)
        {
            if (!string.IsNullOrEmpty(deviceTypeSearch.Name))
                query = query.Where(s => s.Name.Contains(deviceTypeSearch.Name));

            return LinqHelper<DeviceType>.OrderBy(query, deviceTypeSearch.OrderMember, deviceTypeSearch.OrderByAsc);
        }

        #endregion Methods
    }
}
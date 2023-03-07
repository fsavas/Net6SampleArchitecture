using PumpService.Core;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Devices;

namespace PumpService.Data.Repository.Devices
{
    public class DeviceParameterRepository : BaseRepository<DeviceParameter>, IDeviceParameterRepository
    {
        #region Constructor

        public DeviceParameterRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<DeviceParameter> SearchDeviceParameters(DeviceParameterSearch deviceParameterSearch)
        {
            var query = Table;
            AddQueryCriteria(query, deviceParameterSearch);

            return new PagedList<DeviceParameter>(query, deviceParameterSearch.Page - 1, deviceParameterSearch.PageSize);
        }

        public IList<DeviceParameter> SearchAllDeviceParameters(DeviceParameterSearch deviceParameterSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, deviceParameterSearch);

            return query.ToList();
        }

        public List<DeviceParameter> GetAllDeviceParameters()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<DeviceParameter> AddQueryCriteria(IQueryable<DeviceParameter> query, DeviceParameterSearch deviceParameterSearch)
        {
            if (!string.IsNullOrEmpty(deviceParameterSearch.Value))
                query = query.Where(s => s.Value.Contains(deviceParameterSearch.Value));

            return LinqHelper<DeviceParameter>.OrderBy(query, deviceParameterSearch.OrderMember, deviceParameterSearch.OrderByAsc);
        }

        #endregion Methods
    }
}
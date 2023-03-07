using PumpService.Core;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Stations;

namespace PumpService.Data.Repository.SerialPortDefinitions
{
    public class SerialPortDefinitionRepository : BaseRepository<SerialPortDefinition>, ISerialPortDefinitionRepository
    {
        public SerialPortDefinitionRepository(IDbContext context)
            : base(context)
        {
        }

        public IPagedList<SerialPortDefinition> SearchSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, serialPortDefinitionSearch);

            return new PagedList<SerialPortDefinition>(query, serialPortDefinitionSearch.Page - 1, serialPortDefinitionSearch.PageSize);
        }

        public IList<SerialPortDefinition> SearchAllSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, serialPortDefinitionSearch);

            return query.ToList();
        }

        public List<SerialPortDefinition> GetAllSerialPortDefinitions()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<SerialPortDefinition> AddQueryCriteria(IQueryable<SerialPortDefinition> query, SerialPortDefinitionSearch serialPortDefinitionSearch)
        {
            if (!string.IsNullOrEmpty(serialPortDefinitionSearch.PortName))
                query = query.Where(s => s.PortName.Contains(serialPortDefinitionSearch.PortName));

            return LinqHelper<SerialPortDefinition>.OrderBy(query, serialPortDefinitionSearch.OrderMember, serialPortDefinitionSearch.OrderByAsc);
        }
    }
}
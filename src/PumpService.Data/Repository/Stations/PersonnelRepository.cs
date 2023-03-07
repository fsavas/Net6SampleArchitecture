using PumpService.Core;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Stations;

namespace PumpService.Data.Repository.Stations
{
    public class PersonnelRepository : BaseRepository<Personnel>, IPersonnelRepository
    {
        public PersonnelRepository(IDbContext context)
            : base(context)
        {
        }

        public IPagedList<Personnel> SearchPersonnels(PersonnelSearch personnelSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, personnelSearch);

            return new PagedList<Personnel>(query, personnelSearch.Page - 1, personnelSearch.PageSize);
        }

        public IList<Personnel> SearchAllPersonnels(PersonnelSearch personnelSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, personnelSearch);

            return query.ToList();
        }

        public List<Personnel> GetAllPersonnels()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Personnel> AddQueryCriteria(IQueryable<Personnel> query, PersonnelSearch personnelSearch)
        {
            if (!string.IsNullOrEmpty(personnelSearch.Name))
                query = query.Where(s => s.Name.Contains(personnelSearch.Name));

            return LinqHelper<Personnel>.OrderBy(query, personnelSearch.OrderMember, personnelSearch.OrderByAsc);
        }
    }
}
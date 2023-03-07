using PumpService.Core;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Tanks;

namespace PumpService.Data.Repository.Tanks
{
    public class TankRepository : BaseRepository<Tank>, ITankRepository
    {
        public TankRepository(IDbContext context)
            : base(context)
        {
        }

        public IPagedList<Tank> SearchTanks(TankSearch tankSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, tankSearch);

            return new PagedList<Tank>(query, tankSearch.Page - 1, tankSearch.PageSize);
        }

        public IList<Tank> SearchAllTanks(TankSearch tankSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, tankSearch);

            return query.ToList();
        }

        public List<Tank> GetAllTanks()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Tank> AddQueryCriteria(IQueryable<Tank> query, TankSearch tankSearch)
        {
            if (!string.IsNullOrEmpty(tankSearch.Code))
                query = query.Where(s => s.Code.Contains(tankSearch.Code));

            return LinqHelper<Tank>.OrderBy(query, tankSearch.OrderMember, tankSearch.OrderByAsc);
        }
    }
}
using PumpService.Core;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Tanks;

namespace PumpService.Data.Repository.Tanks
{
    public class TankStatusRepository : BaseRepository<TankStatus>, ITankStatusRepository
    {
        public TankStatusRepository(IDbContext context)
            : base(context)
        {
        }

        public IPagedList<TankStatus> SearchTankStatuses(TankStatusSearch tankStatusSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, tankStatusSearch);

            return new PagedList<TankStatus>(query, tankStatusSearch.Page - 1, tankStatusSearch.PageSize);
        }

        public IList<TankStatus> SearchAllTankStatuses(TankStatusSearch tankStatusSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, tankStatusSearch);

            return query.ToList();
        }

        public List<TankStatus> GetAllTankStatuses()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<TankStatus> AddQueryCriteria(IQueryable<TankStatus> query, TankStatusSearch tankStatusSearch)
        {
            if (tankStatusSearch.TankId != null && tankStatusSearch.TankId > 0)
                query = query.Where(s => s.Tank.Id == tankStatusSearch.TankId);

            return LinqHelper<TankStatus>.OrderBy(query, tankStatusSearch.OrderMember, tankStatusSearch.OrderByAsc);
        }
    }
}
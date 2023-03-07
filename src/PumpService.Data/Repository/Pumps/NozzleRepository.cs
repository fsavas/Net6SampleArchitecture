using PumpService.Core;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Pumps;

namespace PumpService.Data.Repository.Pumps
{
    public class NozzleRepository : BaseRepository<Nozzle>, INozzleRepository
    {
        #region Constructor

        public NozzleRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<Nozzle> SearchNozzles(NozzleSearch nozzleSearch)
        {
            var query = Table;
            AddQueryCriteria(query, nozzleSearch);

            return new PagedList<Nozzle>(query, nozzleSearch.Page - 1, nozzleSearch.PageSize);
        }

        public IList<Nozzle> SearchAllNozzles(NozzleSearch nozzleSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, nozzleSearch);

            return query.ToList();
        }

        public List<Nozzle> GetAllNozzles()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Nozzle> AddQueryCriteria(IQueryable<Nozzle> query, NozzleSearch nozzleSearch)
        {
            if (nozzleSearch.ProductId > 0)
                query = query.Where(s => s.ProductId == nozzleSearch.ProductId);

            return LinqHelper<Nozzle>.OrderBy(query, nozzleSearch.OrderMember, nozzleSearch.OrderByAsc);
        }

        #endregion Methods
    }
}
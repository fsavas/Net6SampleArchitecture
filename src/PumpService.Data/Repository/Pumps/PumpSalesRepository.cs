using PumpService.Core;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Pumps;

namespace PumpService.Data.Repository.Pumps
{
    public class PumpSalesRepository : BaseRepository<PumpSales>, IPumpSalesRepository
    {
        #region Constructor

        public PumpSalesRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<PumpSales> SearchPumpSaless(PumpSalesSearch pumpSalesSearch)
        {
            var query = Table;
            AddQueryCriteria(query, pumpSalesSearch);

            return new PagedList<PumpSales>(query, pumpSalesSearch.Page - 1, pumpSalesSearch.PageSize);
        }

        public IList<PumpSales> SearchAllPumpSaless(PumpSalesSearch pumpSalesSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, pumpSalesSearch);

            return query.ToList();
        }

        public List<PumpSales> GetAllPumpSaless()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<PumpSales> AddQueryCriteria(IQueryable<PumpSales> query, PumpSalesSearch pumpSalesSearch)
        {
            if (pumpSalesSearch.TransactionStartTime != null)
                query = query.Where(s => s.TransactionStartTime >= pumpSalesSearch.TransactionStartTime);
            if (pumpSalesSearch.TransactionEndTime != null)
                query = query.Where(s => s.TransactionEndTime <= pumpSalesSearch.TransactionEndTime);

            return LinqHelper<PumpSales>.OrderBy(query, pumpSalesSearch.OrderMember, pumpSalesSearch.OrderByAsc);
        }

        #endregion Methods
    }
}
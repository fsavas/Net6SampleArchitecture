using PumpService.Core.Domain.Pumps;

namespace PumpService.Core.Repository.Pumps
{
    public partial interface IPumpSalesRepository : IBaseRepository<PumpSales>
    {
        #region Methods

        IPagedList<PumpSales> SearchPumpSaless(PumpSalesSearch pumpSalesSearch);

        List<PumpSales> GetAllPumpSaless();

        IList<PumpSales> SearchAllPumpSaless(PumpSalesSearch pumpSalesSearch);

        #endregion Methods
    }
}
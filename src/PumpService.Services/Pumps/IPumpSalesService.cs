using PumpService.Core;
using PumpService.Core.Domain.Pumps;

namespace PumpService.Services.Pumps
{
    public partial interface IPumpSalesService : IBaseService
    {
        List<PumpSales> GetAllPumpSaless();

        PumpSales GetPumpSalesById(long pumpSalesId);

        IPagedList<PumpSales> SearchPumpSaless(PumpSalesSearch pumpSalesSearch);

        string ExportPumpSaless(PumpSalesSearch pumpSalesSearch);
    }
}
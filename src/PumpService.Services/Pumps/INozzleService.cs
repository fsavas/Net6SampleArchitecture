using PumpService.Core;
using PumpService.Core.Domain.Pumps;

namespace PumpService.Services.Pumps
{
    public partial interface INozzleService : IBaseService
    {
        void DeleteNozzle(long nozzleId);

        List<Nozzle> GetAllNozzles();

        Nozzle GetNozzleById(long nozzleId);

        void InsertNozzle(Nozzle nozzle);

        void UpdateNozzle(Nozzle nozzle);

        IPagedList<Nozzle> SearchNozzles(NozzleSearch nozzleSearch);

        string ExportNozzles(NozzleSearch nozzleSearch);
    }
}
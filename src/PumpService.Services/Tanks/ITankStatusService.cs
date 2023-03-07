using PumpService.Core;
using PumpService.Core.Domain.Tanks;

namespace PumpService.Services.Tanks
{
    public partial interface ITankStatusService : IBaseService
    {
        void DeleteTankStatus(long tankStatusId);

        List<TankStatus> GetAllTankStatuses();

        IPagedList<TankStatus> SearchTankStatuses(TankStatusSearch tankStatusSearch);

        TankStatus GetTankStatusById(long tankStatusId);

        void InsertTankStatus(TankStatus tankStatus);

        void UpdateTankStatus(TankStatus tankStatus);

        string ExportTankStatuses(TankStatusSearch tankStatusSearch);
    }
}
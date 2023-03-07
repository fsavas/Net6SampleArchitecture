using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Tanks;

namespace PumpService.Services.Tanks
{
    public partial interface ITankService : IBaseService
    {
        void DeleteTank(long tankId);

        List<Tank> GetAllTanks();

        IPagedList<Tank> SearchTanks(TankSearch tankSearch);

        Tank GetTankById(long tankId);

        void InsertTank(Tank tank);

        void UpdateTank(Tank tank);

        string ExportTanks(TankSearch tankSearch);

        double FindProcessedHeightOfTankStatus(Tank tank, double pmeasuredHeight, EnumClasses.LiquidType pLiquidtype);

        double FindVolumesOfLiquid(Tank tank, double pmeasuredHeight, EnumClasses.LiquidType pLiquidtype);
    }
}
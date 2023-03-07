using PumpService.Core.Domain.Tanks;

namespace PumpService.Core.Repository.Tanks
{
    public partial interface ITankStatusRepository : IBaseRepository<TankStatus>
    {
        #region Methods

        IPagedList<TankStatus> SearchTankStatuses(TankStatusSearch tankStatusSearch);

        List<TankStatus> GetAllTankStatuses();

        IList<TankStatus> SearchAllTankStatuses(TankStatusSearch tankStatusSearch);

        #endregion Methods
    }
}
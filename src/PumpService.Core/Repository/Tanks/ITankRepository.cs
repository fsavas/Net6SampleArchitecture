using PumpService.Core.Domain.Tanks;

namespace PumpService.Core.Repository.Tanks
{
    public partial interface ITankRepository : IBaseRepository<Tank>
    {
        #region Methods

        IPagedList<Tank> SearchTanks(TankSearch tankSearch);

        List<Tank> GetAllTanks();

        IList<Tank> SearchAllTanks(TankSearch tankSearch);

        #endregion Methods
    }
}
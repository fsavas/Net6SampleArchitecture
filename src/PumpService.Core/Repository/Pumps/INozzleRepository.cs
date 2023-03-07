using PumpService.Core.Domain.Pumps;

namespace PumpService.Core.Repository.Pumps
{
    public partial interface INozzleRepository : IBaseRepository<Nozzle>
    {
        #region Methods

        IPagedList<Nozzle> SearchNozzles(NozzleSearch nozzleSearch);

        List<Nozzle> GetAllNozzles();

        IList<Nozzle> SearchAllNozzles(NozzleSearch nozzleSearch);

        #endregion Methods
    }
}
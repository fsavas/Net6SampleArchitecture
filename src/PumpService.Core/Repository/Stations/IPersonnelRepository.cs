using PumpService.Core.Domain.Stations;

namespace PumpService.Core.Repository.Stations
{
    public partial interface IPersonnelRepository : IBaseRepository<Personnel>
    {
        #region Methods

        IPagedList<Personnel> SearchPersonnels(PersonnelSearch personnelSearch);

        List<Personnel> GetAllPersonnels();

        IList<Personnel> SearchAllPersonnels(PersonnelSearch personnelSearch);

        #endregion Methods
    }
}
using PumpService.Core.Domain.Pumps;

namespace PumpService.Core.Repository.Pumps
{
    public partial interface IFillingPointRepository : IBaseRepository<FillingPoint>
    {
        #region Methods

        IPagedList<FillingPoint> SearchFillingPoints(FillingPointSearch fillingPointSearch);

        List<FillingPoint> GetAllFillingPoints();

        IList<FillingPoint> SearchAllFillingPoints(FillingPointSearch fillingPointSearch);

        #endregion Methods
    }
}
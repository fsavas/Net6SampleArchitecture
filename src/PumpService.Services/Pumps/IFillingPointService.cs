using PumpService.Core;
using PumpService.Core.Domain.Pumps;

namespace PumpService.Services.Pumps
{
    public partial interface IFillingPointService : IBaseService
    {
        void DeleteFillingPoint(long fillingPointId);

        List<FillingPoint> GetAllFillingPoints();

        FillingPoint GetFillingPointById(long fillingPointId);

        void InsertFillingPoint(FillingPoint fillingPoint);

        void UpdateFillingPoint(FillingPoint fillingPoint);

        IPagedList<FillingPoint> SearchFillingPoints(FillingPointSearch fillingPointSearch);

        string ExportFillingPoints(FillingPointSearch fillingPointSearch);
    }
}
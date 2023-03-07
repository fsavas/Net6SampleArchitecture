using PumpService.Core;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Repository.Pumps;
using PumpService.Services.Events;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Pumps
{
    public partial class FillingPointService : BaseService, IFillingPointService
    {
        #region Fields

        private readonly IFillingPointRepository _fillingPointRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<FillingPointGrid, FillingPoint> _exportManager;

        #endregion Fields

        #region Constructor

        public FillingPointService(IUnitOfWork unitOfWork, IFillingPointRepository fillingPointRepository, IEventPublisher eventPublisher, IExportManager<FillingPointGrid, FillingPoint> exportManager)
            : base(unitOfWork)
        {
            _fillingPointRepository = fillingPointRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteFillingPoint(long fillingPointId)
        {
            var fillingPoint = GetFillingPointById(fillingPointId);

            if (fillingPoint == null)
                throw new ArgumentNullException(nameof(fillingPoint));

            fillingPoint.IsDeleted = true;
            _fillingPointRepository.Update(fillingPoint);
            _unitOfWork.SaveChanges();
        }

        public virtual List<FillingPoint> GetAllFillingPoints()
        {
            List<FillingPoint> LoadFillingPointsFunc()
            {
                var query = from s in _fillingPointRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadFillingPointsFunc();
        }

        public virtual FillingPoint GetFillingPointById(long fillingPointId)
        {
            if (fillingPointId == 0)
                return null;

            FillingPoint LoadFillingPointFunc()
            {
                return _fillingPointRepository.GetById(fillingPointId);
            }

            return LoadFillingPointFunc();
        }

        public virtual void InsertFillingPoint(FillingPoint fillingPoint)
        {
            if (fillingPoint == null)
                throw new ArgumentNullException(nameof(fillingPoint));

            _fillingPointRepository.Insert(fillingPoint);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateFillingPoint(FillingPoint fillingPoint)
        {
            if (fillingPoint == null)
                throw new ArgumentNullException(nameof(fillingPoint));

            _fillingPointRepository.Update(fillingPoint);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<FillingPoint> SearchFillingPoints(FillingPointSearch fillingPointSearch)
        {
            return _fillingPointRepository.SearchFillingPoints(fillingPointSearch);
        }

        public string ExportFillingPoints(FillingPointSearch fillingPointSearch)
        {
            var list = (List<FillingPoint>)_fillingPointRepository.SearchAllFillingPoints(fillingPointSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
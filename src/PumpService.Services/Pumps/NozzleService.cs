using PumpService.Core;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Repository.Pumps;
using PumpService.Services.Events;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Pumps
{
    public partial class NozzleService : BaseService, INozzleService
    {
        #region Fields

        private readonly INozzleRepository _nozzleRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<NozzleGrid, Nozzle> _exportManager;

        #endregion Fields

        #region Constructor

        public NozzleService(IUnitOfWork unitOfWork, INozzleRepository nozzleRepository, IEventPublisher eventPublisher, IExportManager<NozzleGrid, Nozzle> exportManager)
            : base(unitOfWork)
        {
            _nozzleRepository = nozzleRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteNozzle(long nozzleId)
        {
            var nozzle = GetNozzleById(nozzleId);

            if (nozzle == null)
                throw new ArgumentNullException(nameof(nozzle));

            nozzle.IsDeleted = true;
            _nozzleRepository.Update(nozzle);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Nozzle> GetAllNozzles()
        {
            List<Nozzle> LoadNozzlesFunc()
            {
                var query = from s in _nozzleRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadNozzlesFunc();
        }

        public virtual Nozzle GetNozzleById(long nozzleId)
        {
            if (nozzleId == 0)
                return null;

            Nozzle LoadNozzleFunc()
            {
                return _nozzleRepository.GetById(nozzleId);
            }

            return LoadNozzleFunc();
        }

        public virtual void InsertNozzle(Nozzle nozzle)
        {
            if (nozzle == null)
                throw new ArgumentNullException(nameof(nozzle));

            _nozzleRepository.Insert(nozzle);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateNozzle(Nozzle nozzle)
        {
            if (nozzle == null)
                throw new ArgumentNullException(nameof(nozzle));

            _nozzleRepository.Update(nozzle);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Nozzle> SearchNozzles(NozzleSearch nozzleSearch)
        {
            return _nozzleRepository.SearchNozzles(nozzleSearch);
        }

        public string ExportNozzles(NozzleSearch nozzleSearch)
        {
            var list = (List<Nozzle>)_nozzleRepository.SearchAllNozzles(nozzleSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
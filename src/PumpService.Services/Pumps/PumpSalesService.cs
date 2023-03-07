using PumpService.Core;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Repository.Pumps;
using PumpService.Services.Events;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Pumps
{
    public partial class PumpSalesService : BaseService, IPumpSalesService
    {
        #region Fields

        private readonly IPumpSalesRepository _pumpSalesRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<PumpSalesGrid, PumpSales> _exportManager;

        #endregion Fields

        #region Constructor

        public PumpSalesService(IUnitOfWork unitOfWork, IPumpSalesRepository pumpSalesRepository, IEventPublisher eventPublisher, IExportManager<PumpSalesGrid, PumpSales> exportManager)
            : base(unitOfWork)
        {
            _pumpSalesRepository = pumpSalesRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual List<PumpSales> GetAllPumpSaless()
        {
            List<PumpSales> LoadPumpSalessFunc()
            {
                var query = from s in _pumpSalesRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadPumpSalessFunc();
        }

        public virtual PumpSales GetPumpSalesById(long pumpSalesId)
        {
            if (pumpSalesId == 0)
                return null;

            PumpSales LoadPumpSalesFunc()
            {
                return _pumpSalesRepository.GetById(pumpSalesId);
            }

            return LoadPumpSalesFunc();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<PumpSales> SearchPumpSaless(PumpSalesSearch pumpSalesSearch)
        {
            return _pumpSalesRepository.SearchPumpSaless(pumpSalesSearch);
        }

        public string ExportPumpSaless(PumpSalesSearch pumpSalesSearch)
        {
            var list = (List<PumpSales>)_pumpSalesRepository.SearchAllPumpSaless(pumpSalesSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
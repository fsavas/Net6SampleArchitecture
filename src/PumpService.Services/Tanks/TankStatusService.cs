using PumpService.Core;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Repository.Tanks;
using PumpService.Services.ExportImport;
using Serilog;

namespace PumpService.Services.Tanks
{
    public partial class TankStatusService : BaseService, ITankStatusService
    {
        #region Fields

        private readonly ITankStatusRepository _tankStatusRepository;
        private readonly IExportManager<TankStatusGrid, TankStatus> _exportManager;

        #endregion Fields

        #region Constructor

        public TankStatusService(IUnitOfWork unitOfWork, ITankStatusRepository tankStatusRepository, IExportManager<TankStatusGrid, TankStatus> exportManager)
            : base(unitOfWork)
        {
            _tankStatusRepository = tankStatusRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteTankStatus(long tankStatusId)
        {
            var tankStatus = GetTankStatusById(tankStatusId);

            if (tankStatus == null)
                throw new ArgumentNullException(nameof(tankStatus));

            _tankStatusRepository.Delete(tankStatus);
            _unitOfWork.SaveChanges();
        }

        public virtual List<TankStatus> GetAllTankStatuses()
        {
            List<TankStatus> LoadTankStatusesFunc()
            {
                var query = from s in _tankStatusRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadTankStatusesFunc();
        }

        public virtual TankStatus GetTankStatusById(long tankStatusId)
        {
            if (tankStatusId == 0)
                return null;

            TankStatus LoadTankStatusFunc()
            {
                return _tankStatusRepository.GetById(tankStatusId);
            }

            return LoadTankStatusFunc();
        }

        public virtual void InsertTankStatus(TankStatus tankStatus)
        {
            if (tankStatus == null)
                throw new ArgumentNullException(nameof(tankStatus));

            _tankStatusRepository.Insert(tankStatus);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateTankStatus(TankStatus tankStatus)
        {
            if (tankStatus == null)
                throw new ArgumentNullException(nameof(tankStatus));

            Log.Logger.ForContext("User", "Fatih").Information("Update tankStatus service starting");
            _tankStatusRepository.Update(tankStatus);
            _unitOfWork.SaveChanges();
            Log.Logger.ForContext("User", "Fatih").Information("Update tankStatus service ending");
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<TankStatus> SearchTankStatuses(TankStatusSearch tankStatusSearch)
        {
            return _tankStatusRepository.SearchTankStatuses(tankStatusSearch);
        }

        public string ExportTankStatuses(TankStatusSearch tankStatusSearch)
        {
            var list = (List<TankStatus>)_tankStatusRepository.SearchAllTankStatuses(tankStatusSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
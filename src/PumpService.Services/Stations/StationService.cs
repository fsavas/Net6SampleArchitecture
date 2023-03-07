using PumpService.Core;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Repository.Stations;
using PumpService.Services.Events;
using PumpService.Services.ExportImport;
using Serilog;

namespace PumpService.Services.Stations
{
    public partial class StationService : BaseService, IStationService
    {
        #region Fields

        private readonly IStationRepository _stationRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<StationGrid, Station> _exportManager;

        #endregion Fields

        #region Constructor

        public StationService(IUnitOfWork unitOfWork, IStationRepository stationRepository, IEventPublisher eventPublisher, IExportManager<StationGrid, Station> exportManager)
            : base(unitOfWork)
        {
            _stationRepository = stationRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteStation(long stationId)
        {
            var station = GetStationById(stationId);

            if (station == null)
                throw new ArgumentNullException(nameof(station));

            _stationRepository.Delete(station);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Station> GetAllStations()
        {
            List<Station> LoadStationsFunc()
            {
                var query = from s in _stationRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadStationsFunc();
        }

        public virtual Station GetStationById(long stationId)
        {
            if (stationId == 0)
                return null;

            Station LoadStationFunc()
            {
                return _stationRepository.GetById(stationId);
            }

            return LoadStationFunc();
        }

        public virtual void InsertStation(Station station)
        {
            if (station == null)
                throw new ArgumentNullException(nameof(station));

            _stationRepository.Insert(station);
            _unitOfWork.SaveChanges();

            _eventPublisher.EntityInserted(station);
        }

        public virtual void UpdateStation(Station station)
        {
            if (station == null)
                throw new ArgumentNullException(nameof(station));

            Log.Logger.ForContext("User", "Fatih").Information("Update station service starting");
            _stationRepository.Update(station);
            _unitOfWork.SaveChanges();
            Log.Logger.ForContext("User", "Fatih").Information("Update station service ending");

            _eventPublisher.EntityUpdated(station);
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Station> SearchStations(StationSearch stationSearch)
        {
            return _stationRepository.SearchStations(stationSearch);
        }

        public string ExportStations(StationSearch stationSearch)
        {
            var list = (List<Station>)_stationRepository.SearchAllStations(stationSearch);

            return _exportManager.ExportToExcel(list);
        }

        public void Batch()
        {
            _stationRepository.Batch();
        }

        #endregion Methods
    }
}
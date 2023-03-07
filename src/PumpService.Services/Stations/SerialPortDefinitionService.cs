using PumpService.Core;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Repository.Stations;
using PumpService.Services.ExportImport;
using Serilog;

namespace PumpService.Services.Stations
{
    public partial class SerialPortDefinitionService : BaseService, ISerialPortDefinitionService
    {
        #region Fields

        private readonly ISerialPortDefinitionRepository _serialPortDefinitionRepository;
        private readonly IExportManager<SerialPortDefinitionGrid, SerialPortDefinition> _exportManager;

        #endregion Fields

        #region Constructor

        public SerialPortDefinitionService(IUnitOfWork unitOfWork, ISerialPortDefinitionRepository serialPortDefinitionRepository, IExportManager<SerialPortDefinitionGrid, SerialPortDefinition> exportManager)
            : base(unitOfWork)
        {
            _serialPortDefinitionRepository = serialPortDefinitionRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteSerialPortDefinition(long serialPortDefinitionId)
        {
            var serialPortDefinition = GetSerialPortDefinitionById(serialPortDefinitionId);

            if (serialPortDefinition == null)
                throw new ArgumentNullException(nameof(serialPortDefinition));

            _serialPortDefinitionRepository.Delete(serialPortDefinition);
            _unitOfWork.SaveChanges();
        }

        public virtual List<SerialPortDefinition> GetAllSerialPortDefinitions()
        {
            List<SerialPortDefinition> LoadSerialPortDefinitionsFunc()
            {
                var query = from s in _serialPortDefinitionRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadSerialPortDefinitionsFunc();
        }

        public virtual SerialPortDefinition GetSerialPortDefinitionById(long serialPortDefinitionId)
        {
            if (serialPortDefinitionId == 0)
                return null;

            SerialPortDefinition LoadSerialPortDefinitionFunc()
            {
                return _serialPortDefinitionRepository.GetById(serialPortDefinitionId);
            }

            return LoadSerialPortDefinitionFunc();
        }

        public virtual void InsertSerialPortDefinition(SerialPortDefinition serialPortDefinition)
        {
            if (serialPortDefinition == null)
                throw new ArgumentNullException(nameof(serialPortDefinition));

            _serialPortDefinitionRepository.Insert(serialPortDefinition);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateSerialPortDefinition(SerialPortDefinition serialPortDefinition)
        {
            if (serialPortDefinition == null)
                throw new ArgumentNullException(nameof(serialPortDefinition));

            Log.Logger.ForContext("User", "Fatih").Information("Update serialPortDefinition service starting");
            _serialPortDefinitionRepository.Update(serialPortDefinition);
            _unitOfWork.SaveChanges();
            Log.Logger.ForContext("User", "Fatih").Information("Update serialPortDefinition service ending");
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<SerialPortDefinition> SearchSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch)
        {
            return _serialPortDefinitionRepository.SearchSerialPortDefinitions(serialPortDefinitionSearch);
        }

        public string ExportSerialPortDefinitions(SerialPortDefinitionSearch serialPortDefinitionSearch)
        {
            var list = (List<SerialPortDefinition>)_serialPortDefinitionRepository.SearchAllSerialPortDefinitions(serialPortDefinitionSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
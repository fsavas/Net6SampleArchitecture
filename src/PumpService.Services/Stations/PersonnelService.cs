using PumpService.Core;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Repository.Stations;
using PumpService.Services.ExportImport;
using Serilog;

namespace PumpService.Services.Stations
{
    public partial class PersonnelService : BaseService, IPersonnelService
    {
        #region Fields

        private readonly IPersonnelRepository _personnelRepository;
        private readonly IExportManager<PersonnelGrid, Personnel> _exportManager;

        #endregion Fields

        #region Constructor

        public PersonnelService(IUnitOfWork unitOfWork, IPersonnelRepository personnelRepository, IExportManager<PersonnelGrid, Personnel> exportManager)
            : base(unitOfWork)
        {
            _personnelRepository = personnelRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeletePersonnel(long personnelId)
        {
            var personnel = GetPersonnelById(personnelId);

            if (personnel == null)
                throw new ArgumentNullException(nameof(personnel));

            _personnelRepository.Delete(personnel);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Personnel> GetAllPersonnels()
        {
            List<Personnel> LoadPersonnelsFunc()
            {
                var query = from s in _personnelRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadPersonnelsFunc();
        }

        public virtual Personnel GetPersonnelById(long personnelId)
        {
            if (personnelId == 0)
                return null;

            Personnel LoadPersonnelFunc()
            {
                return _personnelRepository.GetById(personnelId);
            }

            return LoadPersonnelFunc();
        }

        public virtual void InsertPersonnel(Personnel personnel)
        {
            if (personnel == null)
                throw new ArgumentNullException(nameof(personnel));

            _personnelRepository.Insert(personnel);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdatePersonnel(Personnel personnel)
        {
            if (personnel == null)
                throw new ArgumentNullException(nameof(personnel));

            Log.Logger.ForContext("User", "Fatih").Information("Update personnel service starting");
            _personnelRepository.Update(personnel);
            _unitOfWork.SaveChanges();
            Log.Logger.ForContext("User", "Fatih").Information("Update personnel service ending");
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Personnel> SearchPersonnels(PersonnelSearch personnelSearch)
        {
            return _personnelRepository.SearchPersonnels(personnelSearch);
        }

        public string ExportPersonnels(PersonnelSearch personnelSearch)
        {
            var list = (List<Personnel>)_personnelRepository.SearchAllPersonnels(personnelSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
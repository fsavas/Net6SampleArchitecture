using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Repository.Lookups;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Lookups
{
    public partial class LookupTableService : BaseService, ILookupTableService
    {
        #region Fields

        private readonly ILookupTableRepository _lookupTableRepository;
        private readonly IExportManager<LookupTableGrid, LookupTable> _exportManager;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public LookupTableService(IUnitOfWork unitOfWork, ILookupTableRepository lookupTableRepository, IExportManager<LookupTableGrid, LookupTable> exportManager, IMemoryCache memoryCache)
            : base(unitOfWork)
        {
            _lookupTableRepository = lookupTableRepository;
            _exportManager = exportManager;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        public virtual List<LookupTable> GetAllLookupTables()
        {
            List<LookupTable> LoadLookupTablesFunc()
            {
                var query = from s in _lookupTableRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadLookupTablesFunc();
        }

        public virtual LookupTable GetLookupTableById(long lookupTableId)
        {
            if (lookupTableId == 0)
                return null;

            LookupTable LoadLookupTableFunc()
            {
                return _lookupTableRepository.GetById(lookupTableId);
            }

            return LoadLookupTableFunc();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<LookupTable> SearchLookupTables(LookupTableSearch lookupTableSearch)
        {
            return _lookupTableRepository.SearchLookupTables(lookupTableSearch);
        }

        public string ExportLookupTables(LookupTableSearch lookupTableSearch)
        {
            var list = (List<LookupTable>)_lookupTableRepository.SearchAllLookupTables(lookupTableSearch);

            return _exportManager.ExportToExcel(list);
        }

        public LookupTable GetByTypeName(EnumClasses.LookupTypes lookupType, string name)
        {
            return _lookupTableRepository.GetByTypeName(lookupType, name);
        }

        public List<SelectListItem> GetByType(EnumClasses.LookupTypes lookupType)
        {
            var selectListItems = new List<SelectListItem>();
            var lookupTables = _lookupTableRepository.GetByType(lookupType);

            foreach (var item in lookupTables)
            {
                selectListItems.Add(new SelectListItem() { Text = item.Description, Value = item.Id.ToString() });
            }

            return selectListItems;
        }

        public void LoadLookupTable()
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.NeverRemove);

            var lookupTables = _lookupTableRepository.GetAllLookupTables();

            foreach (var lookupTable in lookupTables)
            {
                _memoryCache.Set(string.Join(MemoryCacheKeys.KeySeperator, lookupTable.LookupType, lookupTable.Name), lookupTable, cacheOptions);
            }
        }

        #endregion Methods
    }
}
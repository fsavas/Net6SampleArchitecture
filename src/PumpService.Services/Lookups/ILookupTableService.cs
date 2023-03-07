using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;

namespace PumpService.Services.Lookups
{
    public partial interface ILookupTableService : IBaseService
    {
        List<LookupTable> GetAllLookupTables();

        LookupTable GetLookupTableById(long lookupTableId);

        IPagedList<LookupTable> SearchLookupTables(LookupTableSearch lookupTableSearch);

        string ExportLookupTables(LookupTableSearch lookupTableSearch);

        LookupTable GetByTypeName(EnumClasses.LookupTypes lookupType, string name);

        List<SelectListItem> GetByType(EnumClasses.LookupTypes lookupType);

        void LoadLookupTable();
    }
}
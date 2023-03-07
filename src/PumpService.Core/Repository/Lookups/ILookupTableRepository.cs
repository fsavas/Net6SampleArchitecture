using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;

namespace PumpService.Core.Repository.Lookups
{
    public partial interface ILookupTableRepository : IBaseRepository<LookupTable>
    {
        #region Methods

        IPagedList<LookupTable> SearchLookupTables(LookupTableSearch lookupTableSearch);

        List<LookupTable> GetAllLookupTables();

        IList<LookupTable> SearchAllLookupTables(LookupTableSearch lookupTableSearch);

        LookupTable GetByTypeName(EnumClasses.LookupTypes lookupType, string name);

        List<LookupTable> GetByType(EnumClasses.LookupTypes lookupType);

        LookupTable GetByName(string name);

        #endregion Methods
    }
}
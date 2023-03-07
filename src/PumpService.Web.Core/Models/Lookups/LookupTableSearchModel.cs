using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Lookups
{
    public partial class LookupTableSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}
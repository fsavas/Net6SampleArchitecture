using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Localizations
{
    public partial class LocaleResourceSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_Value_DisplayName)]
        public string Value { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceSearchModel_LanguageId_DisplayName)]
        public long LanguageId { get; set; }

        #endregion Properties
    }
}
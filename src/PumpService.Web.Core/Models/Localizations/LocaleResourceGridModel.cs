using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Localizations
{
    public partial class LocaleResourceGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Value_DisplayName)]
        public string Value { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceGridModel_Language_Name_DisplayName)]
        public string Language_Name { get; set; }

        #endregion Properties
    }
}
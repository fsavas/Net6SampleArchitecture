using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Localizations
{
    public partial class LanguageGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LanguageGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LanguageGridModel_Culture_DisplayName)]
        public string Culture { get; set; }

        #endregion Properties
    }
}
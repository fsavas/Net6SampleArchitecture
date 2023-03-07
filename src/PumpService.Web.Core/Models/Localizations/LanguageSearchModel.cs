using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Localizations
{
    public partial class LanguageSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LanguageSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}
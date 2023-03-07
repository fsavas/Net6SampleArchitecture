using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Localizations
{
    public partial class LanguageModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LanguageModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LanguageModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LanguageModel_Culture_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LanguageModel_Culture_DisplayName)]
        public string Culture { get; set; }

        #endregion Properties
    }
}
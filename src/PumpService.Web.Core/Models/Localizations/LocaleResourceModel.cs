using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Localizations
{
    public partial class LocaleResourceModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Value_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceModel_Value_DisplayName)]
        public string Value { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_DisplayName)]
        public long? LanguageId { get; set; }

        #endregion Properties
    }
}
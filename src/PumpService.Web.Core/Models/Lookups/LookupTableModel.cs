using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Lookups
{
    public partial class LookupTableModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_LookupType_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_LookupType_DisplayName)]
        public int LookupType { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_Name_DisplayName)]
        public string Name { get; set; }

        //[Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_Value_Required)]
        //[DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_Value_DisplayName)]
        //public int Value { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_Description_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_Description_DisplayName)]
        public string Description { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableModel_LookupType_DisplayName)]
        public List<SelectListItemModel> LookupTypes { get; set; }

        #endregion Properties
    }
}
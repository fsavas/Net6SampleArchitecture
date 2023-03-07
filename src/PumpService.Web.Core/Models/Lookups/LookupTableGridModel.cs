using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Lookups
{
    public partial class LookupTableGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableGridModel_LookupType_DisplayName)]
        public string LookupType { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Name_DisplayName)]
        public string Name { get; set; }

        //[DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Value_DisplayName)]
        //public int Value { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableGridModel_Description_DisplayName)]
        public string Description { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Lookups_LookupTableGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
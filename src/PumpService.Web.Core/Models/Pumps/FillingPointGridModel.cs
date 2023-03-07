using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class FillingPointGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointGridModel_Address_DisplayName)]
        public int Address { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
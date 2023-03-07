using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class FillingPointModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointModel_Code_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointModel_Code_DisplayName)]
        public string Code { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointModel_Address_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointModel_Address_DisplayName)]
        public int Address { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_FillingPointModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
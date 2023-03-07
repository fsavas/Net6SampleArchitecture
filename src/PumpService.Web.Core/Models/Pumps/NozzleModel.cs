using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class NozzleModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_Address_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_Address_DisplayName)]
        public int Address { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_FillingPointId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_FillingPointId_DisplayName)]
        public long FillingPointId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_ProductId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_ProductId_DisplayName)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
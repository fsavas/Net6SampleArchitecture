using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Devices
{
    public partial class DeviceParameterModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_DeviceParameters_DeviceParameterModel_Value_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_DeviceParameters_DeviceParameterModel_Value_DisplayName)]
        public string Value { get; set; }

        #endregion Properties
    }
}
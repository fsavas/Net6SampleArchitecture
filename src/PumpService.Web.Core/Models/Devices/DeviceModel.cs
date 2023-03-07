using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Devices
{
    public partial class DeviceModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Devices_DeviceModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Devices_DeviceModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}
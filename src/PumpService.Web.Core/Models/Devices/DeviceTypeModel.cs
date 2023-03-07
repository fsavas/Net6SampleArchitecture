using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Devices
{
    public partial class DeviceTypeModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_DeviceTypes_DeviceTypeModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_DeviceTypes_DeviceTypeModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}
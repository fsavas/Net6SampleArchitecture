using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Devices
{
    public partial class DeviceTypeGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_DeviceTypes_DeviceTypeGridModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}
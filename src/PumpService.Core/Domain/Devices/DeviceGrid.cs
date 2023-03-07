using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Devices
{
    public partial class DeviceGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Devices_DeviceGridModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}
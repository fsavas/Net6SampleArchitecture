using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Stations
{
    public partial class SerialPortDefinitionSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_SerialPortDefinitionSearchModel_PortName_DisplayName)]
        public string PortName { get; set; }

        #endregion Properties
    }
}
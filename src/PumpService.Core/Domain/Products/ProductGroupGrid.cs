using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Products
{
    public partial class ProductGroupGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
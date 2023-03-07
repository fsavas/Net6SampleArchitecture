using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Products
{
    public partial class ProductGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGridModel_UnitPrice_DisplayName)]
        public decimal UnitPrice { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGridModel_ProductGroup_Name_DisplayName)]
        public string ProductGroup_Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
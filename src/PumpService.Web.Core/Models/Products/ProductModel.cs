using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Products
{
    public partial class ProductModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_Code_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_Code_DisplayName)]
        public string Code { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_UnitPrice_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_UnitPrice_DisplayName)]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_ProductGroupId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_ProductGroupId_DisplayName)]
        public long ProductGroupId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
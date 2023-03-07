using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Products
{
    public partial class ProductGroupModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupModel_Code_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupModel_Code_DisplayName)]
        public string Code { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductGroupModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}
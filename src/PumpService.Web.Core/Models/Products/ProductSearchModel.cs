using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Products
{
    public partial class ProductSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Products_ProductSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}
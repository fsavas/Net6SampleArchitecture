using PumpService.Core;
using PumpService.Core.Domain.Products;

namespace PumpService.Services.Products
{
    public partial interface IProductGroupService : IBaseService
    {
        void DeleteProductGroup(long productGroupId);

        List<ProductGroup> GetAllProductGroups();

        ProductGroup GetProductGroupById(long productGroupId);

        void InsertProductGroup(ProductGroup productGroup);

        void UpdateProductGroup(ProductGroup productGroup);

        IPagedList<ProductGroup> SearchProductGroups(ProductGroupSearch productGroupSearch);

        string ExportProductGroups(ProductGroupSearch productGroupSearch);
    }
}
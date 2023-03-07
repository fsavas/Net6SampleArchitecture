using PumpService.Core;
using PumpService.Core.Domain.Products;

namespace PumpService.Services.Products
{
    public partial interface IProductService : IBaseService
    {
        void DeleteProduct(long productId);

        List<Product> GetAllProducts();

        Product GetProductById(long productId);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        IPagedList<Product> SearchProducts(ProductSearch productSearch);

        string ExportProducts(ProductSearch productSearch);
    }
}
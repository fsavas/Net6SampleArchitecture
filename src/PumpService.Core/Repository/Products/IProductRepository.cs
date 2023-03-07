using PumpService.Core.Domain.Products;

namespace PumpService.Core.Repository.Products
{
    public partial interface IProductRepository : IBaseRepository<Product>
    {
        #region Methods

        IPagedList<Product> SearchProducts(ProductSearch productSearch);

        List<Product> GetAllProducts();

        IList<Product> SearchAllProducts(ProductSearch productSearch);

        #endregion Methods
    }
}
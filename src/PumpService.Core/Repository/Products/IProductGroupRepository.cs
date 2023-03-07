using PumpService.Core.Domain.Products;

namespace PumpService.Core.Repository.Products
{
    public partial interface IProductGroupRepository : IBaseRepository<ProductGroup>
    {
        #region Methods

        IPagedList<ProductGroup> SearchProductGroups(ProductGroupSearch productGroupSearch);

        List<ProductGroup> GetAllProductGroups();

        IList<ProductGroup> SearchAllProductGroups(ProductGroupSearch productGroupSearch);

        #endregion Methods
    }
}
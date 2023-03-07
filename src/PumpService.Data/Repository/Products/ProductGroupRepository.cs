using PumpService.Core;
using PumpService.Core.Domain.Products;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Products;

namespace PumpService.Data.Repository.Products
{
    public class ProductGroupRepository : BaseRepository<ProductGroup>, IProductGroupRepository
    {
        #region Constructor

        public ProductGroupRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<ProductGroup> SearchProductGroups(ProductGroupSearch productGroupSearch)
        {
            var query = Table;
            AddQueryCriteria(query, productGroupSearch);

            return new PagedList<ProductGroup>(query, productGroupSearch.Page - 1, productGroupSearch.PageSize);
        }

        public IList<ProductGroup> SearchAllProductGroups(ProductGroupSearch productGroupSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, productGroupSearch);

            return query.ToList();
        }

        public List<ProductGroup> GetAllProductGroups()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<ProductGroup> AddQueryCriteria(IQueryable<ProductGroup> query, ProductGroupSearch productGroupSearch)
        {
            if (!string.IsNullOrEmpty(productGroupSearch.Name))
                query = query.Where(s => s.Name.Contains(productGroupSearch.Name));

            return LinqHelper<ProductGroup>.OrderBy(query, productGroupSearch.OrderMember, productGroupSearch.OrderByAsc);
        }

        #endregion Methods
    }
}
using PumpService.Core;
using PumpService.Core.Domain.Products;
using PumpService.Core.Helpers;
using PumpService.Core.Repository.Products;

namespace PumpService.Data.Repository.Products
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        #region Constructor

        public ProductRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<Product> SearchProducts(ProductSearch productSearch)
        {
            var query = Table;
            AddQueryCriteria(query, productSearch);

            return new PagedList<Product>(query, productSearch.Page - 1, productSearch.PageSize);
        }

        public IList<Product> SearchAllProducts(ProductSearch productSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, productSearch);

            return query.ToList();
        }

        public List<Product> GetAllProducts()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Product> AddQueryCriteria(IQueryable<Product> query, ProductSearch productSearch)
        {
            if (!string.IsNullOrEmpty(productSearch.Name))
                query = query.Where(s => s.Name.Contains(productSearch.Name));

            return LinqHelper<Product>.OrderBy(query, productSearch.OrderMember, productSearch.OrderByAsc);
        }

        #endregion Methods
    }
}
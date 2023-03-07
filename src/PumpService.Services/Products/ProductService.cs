using PumpService.Core;
using PumpService.Core.Domain.Products;
using PumpService.Core.Repository.Products;
using PumpService.Services.Events;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Products
{
    public partial class ProductService : BaseService, IProductService
    {
        #region Fields

        private readonly IProductRepository _productRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<ProductGrid, Product> _exportManager;

        #endregion Fields

        #region Constructor

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, IEventPublisher eventPublisher, IExportManager<ProductGrid, Product> exportManager)
            : base(unitOfWork)
        {
            _productRepository = productRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteProduct(long productId)
        {
            var product = GetProductById(productId);

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            product.IsDeleted = true;
            _productRepository.Update(product);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Product> GetAllProducts()
        {
            List<Product> LoadProductsFunc()
            {
                var query = from s in _productRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadProductsFunc();
        }

        public virtual Product GetProductById(long productId)
        {
            if (productId == 0)
                return null;

            Product LoadProductFunc()
            {
                return _productRepository.GetById(productId);
            }

            return LoadProductFunc();
        }

        public virtual void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productRepository.Insert(product);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _productRepository.Update(product);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Product> SearchProducts(ProductSearch productSearch)
        {
            return _productRepository.SearchProducts(productSearch);
        }

        public string ExportProducts(ProductSearch productSearch)
        {
            var list = (List<Product>)_productRepository.SearchAllProducts(productSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
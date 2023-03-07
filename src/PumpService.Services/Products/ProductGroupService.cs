using PumpService.Core;
using PumpService.Core.Domain.Products;
using PumpService.Core.Repository.Products;
using PumpService.Services.Events;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Products
{
    public partial class ProductGroupService : BaseService, IProductGroupService
    {
        #region Fields

        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IExportManager<ProductGroupGrid, ProductGroup> _exportManager;

        #endregion Fields

        #region Constructor

        public ProductGroupService(IUnitOfWork unitOfWork, IProductGroupRepository productGroupRepository, IEventPublisher eventPublisher, IExportManager<ProductGroupGrid, ProductGroup> exportManager)
            : base(unitOfWork)
        {
            _productGroupRepository = productGroupRepository;
            _eventPublisher = eventPublisher;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteProductGroup(long productGroupId)
        {
            var productGroup = GetProductGroupById(productGroupId);

            if (productGroup == null)
                throw new ArgumentNullException(nameof(productGroup));

            productGroup.IsDeleted = true;
            _productGroupRepository.Update(productGroup);
            _unitOfWork.SaveChanges();
        }

        public virtual List<ProductGroup> GetAllProductGroups()
        {
            List<ProductGroup> LoadProductGroupsFunc()
            {
                var query = from s in _productGroupRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadProductGroupsFunc();
        }

        public virtual ProductGroup GetProductGroupById(long productGroupId)
        {
            if (productGroupId == 0)
                return null;

            ProductGroup LoadProductGroupFunc()
            {
                return _productGroupRepository.GetById(productGroupId);
            }

            return LoadProductGroupFunc();
        }

        public virtual void InsertProductGroup(ProductGroup productGroup)
        {
            if (productGroup == null)
                throw new ArgumentNullException(nameof(productGroup));

            _productGroupRepository.Insert(productGroup);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateProductGroup(ProductGroup productGroup)
        {
            if (productGroup == null)
                throw new ArgumentNullException(nameof(productGroup));

            _productGroupRepository.Update(productGroup);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<ProductGroup> SearchProductGroups(ProductGroupSearch productGroupSearch)
        {
            return _productGroupRepository.SearchProductGroups(productGroupSearch);
        }

        public string ExportProductGroups(ProductGroupSearch productGroupSearch)
        {
            var list = (List<ProductGroup>)_productGroupRepository.SearchAllProductGroups(productGroupSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}
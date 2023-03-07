using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Products;
using PumpService.Services.Products;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Products;

namespace PumpService.Web.Controllers.Products
{
    public class ProductController : BaseController
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public ProductController(IProductService productService, IMapper mapper, IMemoryCache memoryCache)
        {
            _productService = productService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Product
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] ProductSearchModel value)
        {
            try
            {
                var productSearch = _mapper.Map<ProductSearch>(value);
                var productPagedList = (PagedList<Product>)_productService.SearchProducts(productSearch);
                var data = _mapper.Map<PagedList<Product>, PagedList<ProductGridModel>>(productPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Product
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] ProductSearchModel value)
        {
            try
            {
                var productSearch = _mapper.Map<ProductSearch>(value);
                var data = _productService.ExportProducts(productSearch);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Language
        [HttpPost("New")]
        public ServiceResult PostNew()
        {
            try
            {
                var data = InitializeProduct();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Product/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                var data = _mapper.Map<ProductModel>(product);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Product
        [HttpPost]
        public ServiceResult Post([FromBody] ProductModel value)
        {
            try
            {
                var product = _mapper.Map<Product>(value);

                if (product.Id > 0)
                    _productService.UpdateProduct(product);
                else
                    _productService.InsertProduct(product);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _productService.DeleteProduct(id);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Base Methods

        #region Methods

        private ProductModel InitializeProduct()
        {
            return new ProductModel();
        }

        #endregion Methods
    }
}
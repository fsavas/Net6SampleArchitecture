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
    public class ProductGroupController : BaseController
    {
        #region Fields

        private readonly IProductGroupService _productGroupService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public ProductGroupController(IProductGroupService productGroupService, IMapper mapper, IMemoryCache memoryCache)
        {
            _productGroupService = productGroupService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/ProductGroup
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] ProductGroupSearchModel value)
        {
            try
            {
                var productGroupSearch = _mapper.Map<ProductGroupSearch>(value);
                var productGroupPagedList = (PagedList<ProductGroup>)_productGroupService.SearchProductGroups(productGroupSearch);
                var data = _mapper.Map<PagedList<ProductGroup>, PagedList<ProductGroupGridModel>>(productGroupPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/ProductGroup
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] ProductGroupSearchModel value)
        {
            try
            {
                var productGroupSearch = _mapper.Map<ProductGroupSearch>(value);
                var data = _productGroupService.ExportProductGroups(productGroupSearch);

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
                var data = InitializeProductGroup();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/ProductGroup/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var productGroup = _productGroupService.GetProductGroupById(id);
                var data = _mapper.Map<ProductGroupModel>(productGroup);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/ProductGroup
        [HttpPost]
        public ServiceResult Post([FromBody] ProductGroupModel value)
        {
            try
            {
                var productGroup = _mapper.Map<ProductGroup>(value);

                if (productGroup.Id > 0)
                    _productGroupService.UpdateProductGroup(productGroup);
                else
                    _productGroupService.InsertProductGroup(productGroup);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/ProductGroup/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _productGroupService.DeleteProductGroup(id);

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

        private ProductGroupModel InitializeProductGroup()
        {
            return new ProductGroupModel();
        }

        #endregion Methods
    }
}
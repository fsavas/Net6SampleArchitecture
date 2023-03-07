using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Security;
using PumpService.Core.Repository.Security;
using PumpService.Services.Events;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Permissions
{
    public partial class PermissionService : BaseService, IPermissionService
    {
        #region Fields

        private readonly IPermissionRepository _permissionRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMemoryCache _memoryCache;
        private readonly IExportManager<PermissionGrid, Permission> _exportManager;

        #endregion Fields

        #region Constructor

        public PermissionService(IUnitOfWork unitOfWork, IPermissionRepository permissionRepository, IEventPublisher eventPublisher, IMemoryCache memoryCache, IExportManager<PermissionGrid, Permission> exportManager)
            : base(unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _eventPublisher = eventPublisher;
            _memoryCache = memoryCache;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeletePermission(long permissionId)
        {
            var permission = GetPermissionById(permissionId);

            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            permission.IsDeleted = true;
            _permissionRepository.Update(permission);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Permission> GetAllPermissions()
        {
            List<Permission> LoadPermissionsFunc()
            {
                var query = from s in _permissionRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadPermissionsFunc();
        }

        public virtual Permission GetPermissionById(long permissionId)
        {
            if (permissionId == 0)
                return null;

            Permission LoadPermissionFunc()
            {
                return _permissionRepository.GetById(permissionId);
            }

            return LoadPermissionFunc();
        }

        public virtual void InsertPermission(Permission permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRepository.Insert(permission);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdatePermission(Permission permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRepository.Update(permission);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Permission> SearchPermissions(PermissionSearch permissionSearch)
        {
            return _permissionRepository.SearchPermissions(permissionSearch);
        }

        public string ExportPermissions(PermissionSearch permissionSearch)
        {
            var list = (List<Permission>)_permissionRepository.SearchAllPermissions(permissionSearch);

            return _exportManager.ExportToExcel(list);
        }

        public bool HavePermission(string permissionCode)
        {
            if (_memoryCache.TryGetValue(MemoryCacheKeys.Permissions, out List<string> permissions))
            {
                if (permissions.Where(x => x == permissionCode).Any())
                    return true;
            }

            return false;
        }

        public List<string> GetPermissionsByPrefix(string permissionCode)
        {
            var permissionList = new List<string>();

            if (_memoryCache.TryGetValue(MemoryCacheKeys.Permissions, out List<string> permissions))
            {
                permissionList.AddRange(permissions.Where(x => x.StartsWith(permissionCode)).Select(x => x).ToList());
            }

            return permissionList;
        }

        #endregion Methods
    }
}
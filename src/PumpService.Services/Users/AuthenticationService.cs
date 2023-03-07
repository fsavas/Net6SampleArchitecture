using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Users;
using PumpService.Core.Encryption;
using PumpService.Core.Repository.Users;

namespace PumpService.Services.Users
{
    public partial class AuthenticationService : BaseService, IAuthenticationService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly IEncryptionManager _encryptionManager;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public AuthenticationService(IUnitOfWork unitOfWork, IUserRepository userRepository, IEncryptionManager encryptionManager, IMemoryCache memoryCache)
            : base(unitOfWork)
        {
            _userRepository = userRepository;
            _encryptionManager = encryptionManager;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public bool Login(User user)
        {
            var userDb = _userRepository.GetByUsername(user.Username);

            if (userDb != null)
            {
                var key = _encryptionManager.GetKey(user.Password, userDb.Salt);

                if (key.SequenceEqual(userDb.Key))
                {
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.NeverRemove);
                    //.SetSlidingExpiration(TimeSpan.FromMinutes(10))//todo veritabanından al
                    //.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));//todo veritabanından al

                    _memoryCache.Set(MemoryCacheKeys.User, userDb, cacheOptions);

                    var roles = userDb.Roles;

                    if (roles != null)
                    {
                        var permissionList = (from role in roles
                                              let permissions = role.Permissions
                                              where permissions != null
                                              from permission in permissions
                                              select permission.Code).ToList();

                        if (permissionList != null)
                            _memoryCache.Set(MemoryCacheKeys.Permissions, permissionList, cacheOptions);
                    }

                    return true;
                }
            }

            return false;
        }

        public bool Logout()
        {
            _memoryCache.Remove(MemoryCacheKeys.User);
            _memoryCache.Remove(MemoryCacheKeys.Permissions);

            return true;
        }

        #endregion Methods
    }
}
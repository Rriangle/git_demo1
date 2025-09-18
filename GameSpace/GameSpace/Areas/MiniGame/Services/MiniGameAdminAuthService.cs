using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Services
{
    public class MiniGameAdminAuthService : IMiniGameAdminAuthService
    {
        private readonly GameSpacedatabaseContext _context;
        private readonly ILogger<MiniGameAdminAuthService> _logger;

        public MiniGameAdminAuthService(GameSpacedatabaseContext context, ILogger<MiniGameAdminAuthService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permissionType)
        {
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                return false;
            }

            var managerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (managerIdClaim == null || !int.TryParse(managerIdClaim.Value, out int managerId))
            {
                _logger.LogWarning("Manager ID claim not found or invalid for user: {UserName}", user.Identity?.Name);
                return false;
            }

            var manager = await _context.ManagerData
                .Include(m => m.ManagerRoles)
                .FirstOrDefaultAsync(m => m.ManagerId == managerId);

            if (manager == null)
            {
                _logger.LogWarning("Manager with ID {ManagerId} not found in database.", managerId);
                return false;
            }

            // Check for specific permission based on permissionType
            foreach (var managerRole in manager.ManagerRoles)
            {
                switch (permissionType)
                {
                    case "CanManageShopping":
                        if (managerRole.ShoppingPermissionManagement == true) return true;
                        break;
                    case "CanAdmin":
                        if (managerRole.AdministratorPrivilegesManagement == true) return true;
                        break;
                    case "CanMessage":
                        if (managerRole.MessagePermissionManagement == true) return true;
                        break;
                    case "CanUserStatus":
                        if (managerRole.UserStatusManagement == true) return true;
                        break;
                    case "CanPet":
                        if (managerRole.PetRightsManagement == true) return true;
                        break;
                    case "CanCS":
                        if (managerRole.CustomerService == true) return true;
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        public async Task<bool> IsAdminAsync(ClaimsPrincipal user)
        {
            // A simple check if the user has any admin-level permission
            return await HasPermissionAsync(user, "CanAdmin") ||
                   await HasPermissionAsync(user, "CanManageShopping") ||
                   await HasPermissionAsync(user, "CanMessage") ||
                   await HasPermissionAsync(user, "CanUserStatus") ||
                   await HasPermissionAsync(user, "CanPet") ||
                   await HasPermissionAsync(user, "CanCS");
        }

        public async Task<bool> CanAccessAsync(int managerId)
        {
            var manager = await _context.ManagerData
                .Include(m => m.ManagerRoles)
                .FirstOrDefaultAsync(m => m.ManagerId == managerId);

            if (manager == null)
            {
                return false;
            }

            // Check if manager has any admin permissions
            return manager.ManagerRoles.Any(role => 
                role.AdministratorPrivilegesManagement == true ||
                role.ShoppingPermissionManagement == true ||
                role.MessagePermissionManagement == true ||
                role.UserStatusManagement == true ||
                role.PetRightsManagement == true ||
                role.CustomerService == true);
        }

        public async Task<bool> CanAccessModuleAsync(int managerId, string moduleName)
        {
            var manager = await _context.ManagerData
                .Include(m => m.ManagerRoles)
                .FirstOrDefaultAsync(m => m.ManagerId == managerId);

            if (manager == null)
            {
                return false;
            }

            // Check for specific module permission
            foreach (var managerRole in manager.ManagerRoles)
            {
                switch (moduleName.ToLower())
                {
                    case "wallet":
                    case "shopping":
                        if (managerRole.ShoppingPermissionManagement == true) return true;
                        break;
                    case "admin":
                        if (managerRole.AdministratorPrivilegesManagement == true) return true;
                        break;
                    case "message":
                        if (managerRole.MessagePermissionManagement == true) return true;
                        break;
                    case "userstatus":
                    case "signin":
                        if (managerRole.UserStatusManagement == true) return true;
                        break;
                    case "pet":
                        if (managerRole.PetRightsManagement == true) return true;
                        break;
                    case "cs":
                    case "customerservice":
                        if (managerRole.CustomerService == true) return true;
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        public int GetCurrentManagerId(ClaimsPrincipal user)
        {
            var managerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (managerIdClaim != null && int.TryParse(managerIdClaim.Value, out int managerId))
            {
                return managerId;
            }
            return 0;
        }
    }
}

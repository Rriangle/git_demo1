using Microsoft.AspNetCore.Http;
using GameSpace.Models;
using Microsoft.EntityFrameworkCore;
using GameSpace.Data;

namespace GameSpace.Areas.MiniGame.Services
{
    public class MiniGameAdminAuthService : IMiniGameAdminAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public MiniGameAdminAuthService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> HasPermissionAsync(string permission)
        {
            var manager = await GetCurrentManagerAsync();
            if (manager == null) return false;

            var role = await _context.ManagerRoles
                .Include(r => r.ManagerRolePermissions)
                .FirstOrDefaultAsync(r => r.RoleId == manager.RoleId);

            if (role == null) return false;

            return role.ManagerRolePermissions.Any(p => p.PermissionName == permission);
        }

        public async Task<ManagerData?> GetCurrentManagerAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null;

            var managerIdClaim = httpContext.User.FindFirst("ManagerId");
            if (managerIdClaim == null || !int.TryParse(managerIdClaim.Value, out int managerId))
                return null;

            return await _context.ManagerData
                .Include(m => m.ManagerRole)
                .FirstOrDefaultAsync(m => m.ManagerId == managerId);
        }

        public async Task<bool> CanManageShoppingAsync()
        {
            return await HasPermissionAsync("ManageShopping");
        }

        public async Task<bool> CanManagePetAsync()
        {
            return await HasPermissionAsync("ManagePet");
        }

        public async Task<bool> CanManageUserStatusAsync()
        {
            return await HasPermissionAsync("ManageUserStatus");
        }

        public async Task<bool> CanManageCSAsync()
        {
            return await HasPermissionAsync("ManageCS");
        }

        public async Task<bool> CanManageMessageAsync()
        {
            return await HasPermissionAsync("ManageMessage");
        }

        public async Task<bool> IsAdministratorAsync()
        {
            return await HasPermissionAsync("Administrator");
        }
    }
}

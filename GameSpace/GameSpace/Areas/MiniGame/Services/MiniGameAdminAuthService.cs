using GameSpace.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameSpace.Areas.MiniGame.Services
{
    /// <summary>
    /// MiniGame Admin 認證服務實作
    /// 支援不同模組權限檢查
    /// </summary>
    public class MiniGameAdminAuthService : IMiniGameAdminAuthService
    {
        private readonly GameSpacedatabaseContext _context;
        private readonly ILogger<MiniGameAdminAuthService> _logger;

        public MiniGameAdminAuthService(GameSpacedatabaseContext context, ILogger<MiniGameAdminAuthService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 檢查管理員是否可以存取 MiniGame Admin 功能
        /// 預設檢查 PetRightsManagement 權限（向後相容）
        /// </summary>
        /// <param name="managerId">管理員 ID</param>
        /// <returns>true 表示具備 PetRightsManagement 權限</returns>
        public async Task<bool> CanAccessAsync(int managerId)
        {
            return await CanAccessModuleAsync(managerId, "Pet");
        }

        /// <summary>
        /// 檢查管理員是否具備指定模組權限
        /// </summary>
        /// <param name="managerId">管理員 ID</param>
        /// <param name="module">模組名稱：Pet, UserWallet, UserSignIn, MiniGame</param>
        /// <returns>true 表示具備權限，false 表示拒絕存取</returns>
        public async Task<bool> CanAccessModuleAsync(int managerId, string module)
        {
            try
            {
                var hasPermission = module.ToLower() switch
                {
                    "pet" => await CheckPermissionAsync(managerId, mrp => mrp.PetRightsManagement),
                    "userwallet" or "usersignin" or "minigame" => await CheckPermissionAsync(managerId, mrp => mrp.UserStatusManagement),
                    _ => false
                };

                _logger.LogInformation("MiniGame 模組權限檢查: ManagerId={ManagerId}, Module={Module}, HasPermission={HasPermission}", 
                    managerId, module, hasPermission);

                return hasPermission;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MiniGame 模組權限檢查失敗: ManagerId={ManagerId}, Module={Module}", managerId, module);
                return false; // 預設拒絕存取
            }
        }

        /// <summary>
        /// 檢查特定權限
        /// </summary>
        private async Task<bool> CheckPermissionAsync(int managerId, Func<ManagerRolePermission, bool?> permissionSelector)
        {
            return await (from mr in _context.ManagerRolePermissions.AsNoTracking()
                         join mrp in _context.ManagerRolePermissions.AsNoTracking()
                           on mr.ManagerRoleId equals mrp.ManagerRoleId
                         where mr.ManagerRoleId == managerId
                         select mrp)
                         .AnyAsync(mrp => permissionSelector(mrp) == true);
        }

        /// <summary>
        /// 從 HttpContext 取得當前管理員 ID
        /// </summary>
        /// <param name="user">當前用戶 Principal</param>
        /// <returns>管理員 ID，若無效則返回 null</returns>
        public int? GetCurrentManagerId(ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return null;

            // 嘗試多種 claims 類型
            var managerIdclaim = user.FindFirst("ManagerId") ?? 
                                user.FindFirst("Manager_Id") ?? 
                                user.FindFirst("sub") ?? 
                                user.FindFirst("id") ??
                                user.FindFirst(ClaimTypes.NameIdentifier);

            if (managerIdclaim != null && int.TryParse(managerIdclaim.Value, out var managerId))
            {
                return managerId;
            }

            return null;
        }
    }
}

using System.Security.Claims;
namespace GameSpace.Areas.MiniGame.Services
{
    /// <summary>
    /// MiniGame Admin 認證服務介面
    /// 支援不同模組的權限檢查
    /// </summary>
    public interface IMiniGameAdminAuthService
    {
        /// <summary>
        /// 檢查管理員是否可以存取 MiniGame Admin 功能
        /// 預設檢查 Pet_Rights_Management 權限（向後相容）
        /// </summary>
        /// <param name="managerId">管理員 ID</param>
        /// <returns>true 表示具備權限，false 表示拒絕存取</returns>
        Task<bool> CanAccessAsync(int managerId);

        /// <summary>
        /// 檢查管理員是否具備指定模組權限
        /// </summary>
        /// <param name="managerId">管理員 ID</param>
        /// <param name="module">模組名稱：Pet, UserWallet, UserSignIn, MiniGame</param>
        /// <returns>true 表示具備權限，false 表示拒絕存取</returns>
        Task<bool> CanAccessModuleAsync(int managerId, string module);

        /// <summary>
        /// 從 HttpContext 取得當前管理員 ID
        /// </summary>
        /// <param name="user">當前用戶 Principal</param>
        /// <returns>管理員 ID，若無效則返回 null</returns>
        int? GetCurrentManagerId(ClaimsPrincipal user);
    }
}

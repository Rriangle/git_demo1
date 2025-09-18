using System.Security.Claims;
using System.Threading.Tasks;

namespace GameSpace.Areas.MiniGame.Services
{
    public interface IMiniGameAdminAuthService
    {
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permissionType);
        Task<bool> IsAdminAsync(ClaimsPrincipal user);
        Task<bool> CanAccessAsync(int managerId);
        Task<bool> CanAccessModuleAsync(int managerId, string moduleName);
        int GetCurrentManagerId(ClaimsPrincipal user);
    }
}

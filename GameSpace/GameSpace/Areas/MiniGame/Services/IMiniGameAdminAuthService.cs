using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Services
{
    public interface IMiniGameAdminAuthService
    {
        Task<bool> HasPermissionAsync(string permission);
        Task<ManagerData?> GetCurrentManagerAsync();
        Task<bool> CanManageShoppingAsync();
        Task<bool> CanManagePetAsync();
        Task<bool> CanManageUserStatusAsync();
        Task<bool> CanManageCSAsync();
        Task<bool> CanManageMessageAsync();
        Task<bool> IsAdministratorAsync();
    }
}

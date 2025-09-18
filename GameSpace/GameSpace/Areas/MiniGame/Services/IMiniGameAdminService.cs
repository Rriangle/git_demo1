using GameSpace.Areas.MiniGame.Models;

namespace GameSpace.Areas.MiniGame.Services
{
    public interface IMiniGameAdminService
    {
        // SignIn 相關方法
        Task<SignInSummaryReadModel> GetSignInSummaryAsync();
        Task<PagedResult<SignInStatsReadModel>> GetSignInStatsAsync(SignInQueryModel query);
        Task<IEnumerable<UserSignInHistoryReadModel>> GetUserSignInHistoryAsync(int userId);
        Task<UserInfoReadModel> GetUserInfoAsync(int userId);
        Task<bool> AddUserSignInRecordAsync(int userId, DateTime signInDate);
        Task<bool> RemoveUserSignInRecordAsync(int userId, DateTime signInDate);
        Task<bool> ToggleSignInAsync(int userId, bool isAdd);
        Task<bool> BulkToggleSignInAsync(List<int> userIds, bool isAdd);

        // Pet 相關方法
        Task<PagedResult<PetReadModel>> GetPetsAsync(PetQueryModel query);
        Task<PetSummaryReadModel> GetPetSummaryAsync();
        Task<PetDetailReadModel> GetPetDetailAsync(int petId);

        // Game 相關方法
        Task<PagedResult<GameRecordReadModel>> GetGameRecordsAsync(GameQueryModel query);
        Task<GameSummaryReadModel> GetGameSummaryAsync();
        Task<GameDetailReadModel> GetGameDetailAsync(int gameId);

        // Coupon 相關方法
        Task<PagedResult<UserCouponReadModel>> QueryUserCouponsAsync(CouponQueryModel query);
        Task<bool> IssueCouponToUserAsync(int userId, int couponId, int quantity);

        // EVoucher 相關方法
        Task<PagedResult<UserEVoucherReadModel>> QueryUserEVouchersAsync(EVoucherQueryModel query);
        Task<bool> IssueEVoucherToUserAsync(int userId, int eVoucherId, int quantity);
    }
}

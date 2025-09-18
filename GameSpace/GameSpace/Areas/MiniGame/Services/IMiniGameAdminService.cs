using GameSpace.Areas.MiniGame.Models;
using GameSpace.Models; // For PagedResult

namespace GameSpace.Areas.MiniGame.Services
{
    public interface IMiniGameAdminService
    {
        // 會員點數系統相關方法
        Task<PagedResult<WalletReadModel>> GetUserWalletsAsync(WalletQueryModel query);
        Task<WalletSummaryReadModel> GetWalletSummaryAsync();
        Task<WalletDetailReadModel> GetWalletDetailAsync(int userId);
        Task<bool> AdjustUserPointsAsync(int userId, int pointsChange, string description);
        Task<PagedResult<UserCouponReadModel>> QueryUserCouponsAsync(CouponQueryModel query);
        Task<bool> IssueCouponToUserAsync(int userId, int couponTypeId, int quantity);
        Task<bool> RemoveUserCouponAsync(int userCouponId);
        Task<PagedResult<UserEVoucherReadModel>> QueryUserEVouchersAsync(EVoucherQueryModel query);
        Task<bool> IssueEVoucherToUserAsync(int userId, int eVoucherTypeId, int quantity);
        Task<bool> RemoveUserEVoucherAsync(int userEVoucherId);
        Task<PagedResult<WalletTransactionReadModel>> GetWalletHistoryAsync(WalletQueryModel query);

        // 會員簽到系統相關方法
        Task<SignInRuleReadModel> GetSignInRuleAsync();
        Task<bool> UpdateSignInRuleAsync(SignInRuleUpdateModel model);
        Task<PagedResult<SignInStatsReadModel>> GetSignInStatsAsync(SignInQueryModel query);
        Task<IEnumerable<UserSignInHistoryReadModel>> GetUserSignInHistoryAsync(int userId);
        Task<bool> AddUserSignInRecordAsync(int userId, DateTime signInDate);
        Task<bool> RemoveUserSignInRecordAsync(int userId, DateTime signInDate);

        // 寵物系統相關方法
        Task<PetRuleReadModel> GetPetRuleAsync();
        Task<bool> UpdatePetRuleAsync(PetRuleUpdateModel model);
        Task<PagedResult<PetReadModel>> GetPetsAsync(PetQueryModel query);
        Task<PetSummaryReadModel> GetPetSummaryAsync();
        Task<PetDetailReadModel> GetPetDetailAsync(int petId);
        Task<bool> UpdatePetDetailsAsync(int petId, PetUpdateModel model);
        Task<PagedResult<PetSkinColorChangeLogReadModel>> GetPetSkinColorChangeLogsAsync(PetQueryModel query);
        Task<PagedResult<PetBackgroundColorChangeLogReadModel>> GetPetBackgroundColorChangeLogsAsync(PetQueryModel query);

        // 小遊戲系統相關方法
        Task<GameRuleReadModel> GetGameRuleAsync();
        Task<bool> UpdateGameRuleAsync(GameRuleUpdateModel model);
        Task<PagedResult<GameRecordReadModel>> GetGameRecordsAsync(GameQueryModel query);
        Task<GameSummaryReadModel> GetGameSummaryAsync();
        Task<GameDetailReadModel> GetGameDetailAsync(int gameId);
    }
}

using GameSpace.Models;
using GameSpace.Areas.MiniGame.Models;

namespace GameSpace.Areas.MiniGame.Services
{
    public interface IMiniGameAdminService
    {
        // 錢包相關
        Task<PagedResult<UserPointsReadModel>> QueryUserPointsAsync(CouponQueryModel query);
        Task<PagedResult<CouponReadModel>> QueryUserCouponsAsync(CouponQueryModel query);
        Task<PagedResult<EVoucherReadModel>> QueryUserEVouchersAsync(EVoucherQueryModel query);
        Task<PagedResult<WalletTransactionReadModel>> QueryWalletTransactionsAsync(CouponQueryModel query);
        Task<bool> AdjustUserPointsAsync(int userId, int points, string reason);
        Task<bool> IssueCouponToUserAsync(int userId, int couponTypeId, int quantity);
        Task<bool> IssueEVoucherToUserAsync(int userId, int evoucherTypeId, int quantity);
        Task<bool> RemoveCouponFromUserAsync(int userId, int couponId);
        Task<bool> RemoveEVoucherFromUserAsync(int userId, int evoucherId);

        // 簽到相關
        Task<PagedResult<SignInStatsReadModel>> GetSignInStatsAsync(CouponQueryModel query);
        Task<SignInRuleReadModel?> GetSignInRuleAsync();
        Task<bool> UpdateSignInRuleAsync(SignInRuleUpdateModel model);
        Task<bool> AddUserSignInRecordAsync(int userId, DateTime signInDate);
        Task<bool> RemoveUserSignInRecordAsync(int userId, DateTime signInDate);

        // 寵物相關
        Task<PagedResult<PetReadModel>> GetPetsAsync(CouponQueryModel query);
        Task<PetSummaryReadModel> GetPetSummaryAsync();
        Task<PetRuleReadModel?> GetPetRuleAsync();
        Task<bool> UpdatePetRuleAsync(PetRuleUpdateModel model);
        Task<PetReadModel?> GetPetDetailAsync(int petId);
        Task<bool> UpdatePetDetailsAsync(int petId, PetReadModel model);
        Task<List<PetSkinColorChangeLogReadModel>> GetPetSkinColorChangeLogsAsync(int petId);
        Task<List<PetBackgroundColorChangeLogReadModel>> GetPetBackgroundColorChangeLogsAsync(int petId);

        // 小遊戲相關
        Task<GameSummaryReadModel> GetGameSummaryAsync();
        Task<GameRuleReadModel?> GetGameRuleAsync();
        Task<bool> UpdateGameRuleAsync(GameRuleUpdateModel model);
        Task<PagedResult<GameRecordReadModel>> GetGameRecordsAsync(CouponQueryModel query);
        Task<GameRecordReadModel?> GetGameDetailAsync(int gameId);

        // 用戶相關
        Task<GameSpace.Models.User?> GetUserByIdAsync(int userId);
        Task<List<GameSpace.Models.User>> GetUsersAsync();

        // 統計相關
        Task<WalletSummaryReadModel> GetWalletSummaryAsync();

        // 獲取優惠券類型
        Task<List<GameSpace.Models.CouponType>> GetCouponTypesAsync();

        // 獲取電子優惠券類型
        Task<List<GameSpace.Models.EVoucherType>> GetEVoucherTypesAsync();
    }
}

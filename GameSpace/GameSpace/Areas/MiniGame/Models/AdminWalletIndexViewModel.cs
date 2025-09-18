using GameSpace.Areas.MiniGame.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminWalletIndexViewModel
    {
        public PagedResult<UserPointsReadModel> UserPoints { get; set; } = new();
        public PagedResult<CouponReadModel> UserCoupons { get; set; } = new();
        public PagedResult<EVoucherReadModel> UserEVouchers { get; set; } = new();
        public PagedResult<WalletTransactionReadModel> WalletTransactions { get; set; } = new();
        public WalletSummaryReadModel WalletSummary { get; set; } = new();
        public List<CouponType> CouponTypes { get; set; } = new();
        public List<EVoucherType> EVoucherTypes { get; set; } = new();
        public CouponQueryModel Query { get; set; } = new();
    }
}

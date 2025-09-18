using GameSpace.Models;
using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminWalletIndexViewModel
    {
        public PagedResult<WalletReadModel> Wallets { get; set; } = new();
        public WalletSummaryReadModel WalletSummary { get; set; } = new();
        public WalletQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminWalletDetailsViewModel
    {
        public WalletDetailReadModel WalletDetail { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminWalletStatisticsViewModel
    {
        public WalletSummaryReadModel Summary { get; set; } = new();
        public List<WalletReadModel> TopWallets { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminWalletAdjustPointsViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int CurrentPoints { get; set; }

        [Required(ErrorMessage = "請輸入調整點數")]
        [Range(-999999, 999999, ErrorMessage = "調整點數必須在 -999999 到 999999 之間")]
        public int Points { get; set; }

        [Required(ErrorMessage = "請輸入調整原因")]
        [StringLength(200, ErrorMessage = "調整原因不能超過 200 個字元")]
        public string Reason { get; set; } = string.Empty;

        public string Sidebar { get; set; } = "admin";
    }

    public class AdminWalletTransactionHistoryViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<WalletTransactionReadModel> Transactions { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminWalletCouponsViewModel
    {
        public PagedResult<CouponReadModel> Coupons { get; set; } = new();
        public CouponQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminWalletEVouchersViewModel
    {
        public PagedResult<EVoucherReadModel> EVouchers { get; set; } = new();
        public EVoucherQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }
}

using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    // 優惠券相關模型
    public class CouponReadModel
    {
        public int CouponID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int CouponTypeID { get; set; }
        public string CouponTypeName { get; set; } = string.Empty;
        public string CouponCode { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime? UsedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public int DiscountValue { get; set; }
    }

    public class UserCouponReadModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int TotalCoupons { get; set; }
        public int UsedCoupons { get; set; }
        public int ActiveCoupons { get; set; }
        public int ExpiredCoupons { get; set; }
    }

    // 電子優惠券相關模型
    public class EVoucherReadModel
    {
        public int EVoucherID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int EVoucherTypeID { get; set; }
        public string EVoucherTypeName { get; set; } = string.Empty;
        public string EVoucherCode { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime? UsedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public int Value { get; set; }
        public int TokenId { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public class UserEVoucherReadModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int TotalEVouchers { get; set; }
        public int UsedEVouchers { get; set; }
        public int ActiveEVouchers { get; set; }
        public int ExpiredEVouchers { get; set; }
    }

    // 簽到規則相關模型
    public class SignInRuleReadModel
    {
        public int RuleID { get; set; }
        public string RuleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PointsReward { get; set; }
        public int ExpReward { get; set; }
        public int CouponReward { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    // 寵物規則相關模型
    public class PetRuleReadModel
    {
        public int RuleID { get; set; }
        public string RuleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LevelUpExp { get; set; }
        public int InteractionPoints { get; set; }
        public int ColorChangeCost { get; set; }
        public int BackgroundChangeCost { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

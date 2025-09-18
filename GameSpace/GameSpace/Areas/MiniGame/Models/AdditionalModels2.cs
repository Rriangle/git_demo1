using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    // 優惠券相關模型
    public class CouponQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }

    public class CouponReadModel
    {
        public int CouponId { get; set; }
        public string CouponName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DiscountAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class UserCouponReadModel
    {
        public int UserCouponId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int CouponId { get; set; }
        public string CouponName { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime? UsedDate { get; set; }
        public bool IsUsed { get; set; }
    }

    // 電子禮券相關模型
    public class EVoucherQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }

    public class EVoucherReadModel
    {
        public int EVoucherId { get; set; }
        public string EVoucherName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class UserEVoucherReadModel
    {
        public int UserEVoucherId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int EVoucherId { get; set; }
        public string EVoucherName { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime? UsedDate { get; set; }
        public bool IsUsed { get; set; }
    }

    // 規則相關模型
    public class SignInRuleReadModel
    {
        public int RuleId { get; set; }
        public int PointsPerSignIn { get; set; }
        public int ConsecutiveBonus { get; set; }
        public int MaxConsecutiveDays { get; set; }
    }

    public class SignInRuleUpdateModel
    {
        public int PointsPerSignIn { get; set; }
        public int ConsecutiveBonus { get; set; }
        public int MaxConsecutiveDays { get; set; }
    }

    public class PetRuleReadModel
    {
        public int RuleId { get; set; }
        public int PointsPerLevelUp { get; set; }
        public int PointsPerSkinChange { get; set; }
        public int PointsPerBackgroundChange { get; set; }
    }

    public class PetRuleUpdateModel
    {
        public int PointsPerLevelUp { get; set; }
        public int PointsPerSkinChange { get; set; }
        public int PointsPerBackgroundChange { get; set; }
    }

    public class GameRuleReadModel
    {
        public int RuleId { get; set; }
        public int PointsPerGame { get; set; }
        public int PointsPerWin { get; set; }
        public int PointsPerLoss { get; set; }
    }

    public class GameRuleUpdateModel
    {
        public int PointsPerGame { get; set; }
        public int PointsPerWin { get; set; }
        public int PointsPerLoss { get; set; }
    }
}

using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    // 優惠券查詢模型
    public class CouponQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }

    // 電子優惠券查詢模型
    public class EVoucherQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }

    // 簽到規則更新模型
    public class SignInRuleUpdateModel
    {
        public int RuleID { get; set; }
        public string RuleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PointsReward { get; set; }
        public int ExpReward { get; set; }
        public int CouponReward { get; set; }
        public bool IsActive { get; set; }
    }

    // 寵物規則更新模型
    public class PetRuleUpdateModel
    {
        public int RuleID { get; set; }
        public string RuleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LevelUpExp { get; set; }
        public int InteractionPoints { get; set; }
        public int ColorChangeCost { get; set; }
        public int BackgroundChangeCost { get; set; }
        public bool IsActive { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    // Admin 調整會員點數/優惠券/電子禮券
    public class AdminAdjustUserPointsModel
    {
        [Required(ErrorMessage = "用戶ID為必填")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "點數變更數量為必填")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "點數變更數量必須是有效數字")]
        public int PointsChange { get; set; }

        [StringLength(255, ErrorMessage = "描述長度不能超過255個字元")]
        public string? Description { get; set; }
    }

    public class AdminAdjustUserCouponModel
    {
        [Required(ErrorMessage = "用戶ID為必填")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "優惠券ID為必填")]
        public int CouponId { get; set; }

        [Required(ErrorMessage = "數量為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "數量必須大於0")]
        public int Quantity { get; set; }

        public bool IsAdd { get; set; } // true for add, false for remove
    }

    public class AdminAdjustUserEVoucherModel
    {
        [Required(ErrorMessage = "用戶ID為必填")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "電子禮券ID為必填")]
        public int EVoucherId { get; set; }

        [Required(ErrorMessage = "數量為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "數量必須大於0")]
        public int Quantity { get; set; }

        public bool IsAdd { get; set; } // true for add, false for remove
    }

    // Admin 簽到規則設定
    public class AdminSignInRuleUpdateModel
    {
        [Required(ErrorMessage = "每日簽到點數為必填")]
        [Range(0, int.MaxValue, ErrorMessage = "點數必須大於或等於0")]
        public int PointsPerSignIn { get; set; }

        [Required(ErrorMessage = "連續簽到獎勵為必填")]
        [Range(0, int.MaxValue, ErrorMessage = "獎勵必須大於或等於0")]
        public int ConsecutiveBonus { get; set; }

        [Required(ErrorMessage = "最大連續簽到天數為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "天數必須大於0")]
        public int MaxConsecutiveDays { get; set; }
    }

    // Admin 寵物系統規則設定
    public class AdminPetRuleUpdateModel
    {
        [Required(ErrorMessage = "升級所需點數為必填")]
        [Range(0, int.MaxValue, ErrorMessage = "點數必須大於或等於0")]
        public int PointsPerLevelUp { get; set; }

        [Required(ErrorMessage = "換膚色所需點數為必填")]
        [Range(0, int.MaxValue, ErrorMessage = "點數必須大於或等於0")]
        public int PointsPerSkinChange { get; set; }

        [Required(ErrorMessage = "換背景所需點數為必填")]
        [Range(0, int.MaxValue, ErrorMessage = "點數必須大於或等於0")]
        public int PointsPerBackgroundChange { get; set; }
    }

    // Admin 小遊戲系統規則設定
    public class AdminGameRuleUpdateModel
    {
        [Required(ErrorMessage = "每關怪物數量為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "怪物數量必須大於0")]
        public int MonsterCountPerLevel { get; set; }

        [Required(ErrorMessage = "怪物行進速率為必填")]
        [Range(0.1, double.MaxValue, ErrorMessage = "速率必須大於0")]
        public decimal MonsterSpeedMultiplier { get; set; }

        [Required(ErrorMessage = "遊戲勝利獎勵點數為必填")]
        [Range(0, int.MaxValue, ErrorMessage = "點數必須大於或等於0")]
        public int PointsPerWin { get; set; }

        [Required(ErrorMessage = "每日遊戲次數限制為必填")]
        [Range(1, int.MaxValue, ErrorMessage = "次數必須大於0")]
        public int DailyPlayLimit { get; set; }
    }
}

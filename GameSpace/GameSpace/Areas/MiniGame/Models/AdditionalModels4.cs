using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    // 寵物換膚色記錄
    public class PetSkinColorChangeLogReadModel
    {
        public int LogId { get; set; }
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string OldSkinColor { get; set; } = string.Empty;
        public string NewSkinColor { get; set; } = string.Empty;
        public int PointsCost { get; set; }
        public DateTime ChangeDate { get; set; }
    }

    // 寵物換背景記錄
    public class PetBackgroundColorChangeLogReadModel
    {
        public int LogId { get; set; }
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string OldBackgroundColor { get; set; } = string.Empty;
        public string NewBackgroundColor { get; set; } = string.Empty;
        public int PointsCost { get; set; }
        public DateTime ChangeDate { get; set; }
    }

    // 遊戲規則更新模型（更新版本）
    public class GameRuleUpdateModel
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

    // 遊戲規則讀取模型（更新版本）
    public class GameRuleReadModel
    {
        public int RuleId { get; set; }
        public int MonsterCountPerLevel { get; set; }
        public decimal MonsterSpeedMultiplier { get; set; }
        public int PointsPerWin { get; set; }
        public int DailyPlayLimit { get; set; }
    }
}

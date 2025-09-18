using GameSpace.Models;
using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminMiniGameSettingsViewModel
    {
        public GameSettings GameSettings { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class GameSettings
    {
        public int MaxDailyGames { get; set; } = 3;
        public int MaxLevel { get; set; } = 3;
        public decimal BaseSpeed { get; set; } = 1.0m;
        public int BaseMonsterCount { get; set; } = 6;
        public int BaseExpReward { get; set; } = 50;
        public int BasePointReward { get; set; } = 10;
        public bool EnableCouponReward { get; set; } = true;
        public int CouponRewardLevel { get; set; } = 3;
    }
}

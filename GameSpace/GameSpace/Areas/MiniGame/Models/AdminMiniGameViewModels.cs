using GameSpace.Models;
using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminMiniGameIndexViewModel
    {
        public PagedResult<GameRecordReadModel> GameRecords { get; set; } = new();
        public GameSummaryReadModel GameSummary { get; set; } = new();
        public GameQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminMiniGameRulesViewModel
    {
        public GameRuleReadModel GameRule { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminMiniGameDetailsViewModel
    {
        public GameDetailReadModel Game { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }
}

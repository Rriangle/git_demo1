using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminMiniGameIndexViewModel
    {
        public PagedResult<GameRecordReadModel> GameRecords { get; set; } = new();
        public GameSummaryReadModel GameSummary { get; set; } = new();
        public GameQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminMiniGameDetailsViewModel
    {
        public GameDetailReadModel GameDetail { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminMiniGameStatisticsViewModel
    {
        public GameSummaryReadModel Summary { get; set; } = new();
        public List<GameRecordReadModel> RecentGames { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminMiniGameRulesViewModel
    {
        public GameRuleReadModel Rules { get; set; } = new();
        public GameRuleUpdateModel? UpdateModel { get; set; }
        public string Sidebar { get; set; } = "admin";
    }
}

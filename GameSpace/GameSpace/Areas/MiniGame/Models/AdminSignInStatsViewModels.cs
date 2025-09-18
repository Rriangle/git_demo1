using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminSignInStatsIndexViewModel
    {
        public PagedResult<SignInStatsReadModel> SignInStats { get; set; } = new();
        public SignInSummaryReadModel SignInSummary { get; set; } = new();
        public SignInQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminSignInStatsDetailsViewModel
    {
        public UserInfoReadModel UserInfo { get; set; } = new();
        public List<UserSignInHistoryReadModel> UserSignInHistory { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminSignInStatsStatisticsViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public decimal SignInRate { get; set; }
        public List<TopUserReadModel> TopUsers { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminSignInStatsRulesViewModel
    {
        public SignInRuleReadModel Rules { get; set; } = new();
        public SignInRuleUpdateModel? UpdateModel { get; set; }
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminSignInStatsAddSignInViewModel
    {
        public int UserId { get; set; }
        public DateTime SignInDate { get; set; }
        public string Sidebar { get; set; } = "admin";
    }

    // 添加缺少的模型
    public class SignInStatsViewModel
    {
        public PagedResult<SignInStatsReadModel> SignInStats { get; set; } = new();
        public SignInSummaryReadModel Summary { get; set; } = new();
        public SignInQueryModel Query { get; set; } = new();
    }

    public class SignInToggleModel
    {
        public int UserId { get; set; }
        public string Action { get; set; } = string.Empty;
    }

    public class BulkSignInToggleModel
    {
        public List<int> UserIds { get; set; } = new();
        public string Action { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminSignInRulesViewModel
    {
        public SignInRuleReadModel SignInRule { get; set; } = new();
        public SidebarViewModel Sidebar { get; set; } = new();
    }

    public class AdminSignInUserHistoryViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<GameSpace.Models.UserSignInStat> SignInHistory { get; set; } = new();
        public SidebarViewModel Sidebar { get; set; } = new();
    }
}

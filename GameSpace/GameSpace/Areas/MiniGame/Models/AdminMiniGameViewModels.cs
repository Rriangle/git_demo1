using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminMiniGameIndexViewModel
    {
        public List<GameSpace.Models.MiniGame> GameRecords { get; set; } = new();
        public SidebarViewModel Sidebar { get; set; } = new();
    }

    public class AdminMiniGameRulesViewModel
    {
        public GameRule GameRule { get; set; } = new();
        public SidebarViewModel Sidebar { get; set; } = new();
    }

    public class AdminMiniGameDetailsViewModel
    {
        public GameSpace.Models.MiniGame GameRecord { get; set; } = new();
        public SidebarViewModel Sidebar { get; set; } = new();
    }
}

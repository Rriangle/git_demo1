using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminSignInIndexViewModel
    {
        public List<SignInStatsReadModel> SignInStats { get; set; } = new List<SignInStatsReadModel>();
        public List<User> Users { get; set; } = new List<User>();
        public string? SearchTerm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}

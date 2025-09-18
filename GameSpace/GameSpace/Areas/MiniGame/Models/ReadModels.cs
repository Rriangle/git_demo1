using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class SignInSummaryReadModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public decimal SignInRate { get; set; }
    }

    public class SignInStatsReadModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int SignInCount { get; set; }
        public DateTime LastSignIn { get; set; }
    }

    public class UserSignInHistoryReadModel
    {
        public int UserId { get; set; }
        public DateTime SignInDate { get; set; }
        public int PointsEarned { get; set; }
    }

    public class UserInfoReadModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class SignInQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }
}

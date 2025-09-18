using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    // 遊戲相關模型
    public class GameQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }

    public class GameRecordReadModel
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string GameResult { get; set; } = string.Empty;
        public int PointsEarned { get; set; }
        public int ExpEarned { get; set; }
        public string? CouponEarned { get; set; }
    }

    public class GameSummaryReadModel
    {
        public int TotalPlays { get; set; }
        public int TotalUsers { get; set; }
        public int AverageScore { get; set; }
    }

    public class GameDetailReadModel
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string GameData { get; set; } = string.Empty;
        public string GameResult { get; set; } = string.Empty;
        public int PointsEarned { get; set; }
        public int ExpEarned { get; set; }
        public string? CouponEarned { get; set; }
    }

    // 寵物相關模型
    public class PetQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }

    public class PetReadModel
    {
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public int PetLevel { get; set; }
        public int PetExp { get; set; }
        public string PetColor { get; set; } = string.Empty;
        public string PetBackground { get; set; } = string.Empty;
        public int Hunger { get; set; }
        public int Happiness { get; set; }
        public int Cleanliness { get; set; }
        public int Energy { get; set; }
        public int Health { get; set; }
    }

    public class PetSummaryReadModel
    {
        public int TotalPets { get; set; }
        public int ActivePets { get; set; }
        public int AverageLevel { get; set; }
    }

    public class PetDetailReadModel
    {
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public int PetLevel { get; set; }
        public int PetExp { get; set; }
        public string PetColor { get; set; } = string.Empty;
        public string PetBackground { get; set; } = string.Empty;
        public int Hunger { get; set; }
        public int Happiness { get; set; }
        public int Cleanliness { get; set; }
        public int Energy { get; set; }
        public int Health { get; set; }
    }

    public class PetUpdateModel
    {
        public string PetName { get; set; } = string.Empty;
        public int PetLevel { get; set; }
        public int PetExp { get; set; }
        public string PetColor { get; set; } = string.Empty;
        public string PetBackground { get; set; } = string.Empty;
        public int Hunger { get; set; }
        public int Happiness { get; set; }
        public int Cleanliness { get; set; }
        public int Energy { get; set; }
        public int Health { get; set; }
    }

    // 錢包相關模型
    public class WalletQueryModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageNumberSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
    }

    public class WalletReadModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserPoint { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class WalletSummaryReadModel
    {
        public int TotalUsers { get; set; }
        public int TotalPoints { get; set; }
        public int AveragePoints { get; set; }
    }

    public class WalletDetailReadModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserPoint { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class WalletTransactionReadModel
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TopUserReadModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserPoint { get; set; }
        public int Rank { get; set; }
    }
}

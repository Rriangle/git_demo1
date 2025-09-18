using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSpace.Areas.MiniGame.Models
{
    // 會員點數系統相關模型
    [Table("CouponType")]
    public class CouponType
    {
        [Key]
        public int CouponTypeID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CouponName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public int DiscountAmount { get; set; }
        
        [Required]
        public DateTime ExpiryDate { get; set; }
        
        [Required]
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }

    [Table("Coupon")]
    public class Coupon
    {
        [Key]
        public int CouponID { get; set; }
        
        [Required]
        public int UserID { get; set; }
        
        [Required]
        public int CouponTypeID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string CouponCode { get; set; } = string.Empty;
        
        [Required]
        public DateTime IssuedDate { get; set; }
        
        public DateTime? UsedDate { get; set; }
        
        [Required]
        public DateTime ExpiryDate { get; set; }
        
        // Navigation properties
        [ForeignKey("CouponTypeID")]
        public virtual CouponType CouponType { get; set; } = null!;
        
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
    }

    [Table("EVoucherType")]
    public class EVoucherType
    {
        [Key]
        public int EVoucherTypeID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string EVoucherName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public int Value { get; set; }
        
        [Required]
        public DateTime ExpiryDate { get; set; }
        
        [Required]
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual ICollection<EVoucher> EVouchers { get; set; } = new List<EVoucher>();
    }

    [Table("EVoucher")]
    public class EVoucher
    {
        [Key]
        public int EVoucherID { get; set; }
        
        [Required]
        public int UserID { get; set; }
        
        [Required]
        public int EVoucherTypeID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string EVoucherCode { get; set; } = string.Empty;
        
        [Required]
        public DateTime IssuedDate { get; set; }
        
        public DateTime? UsedDate { get; set; }
        
        [Required]
        public DateTime ExpiryDate { get; set; }
        
        // Navigation properties
        [ForeignKey("EVoucherTypeID")]
        public virtual EVoucherType EVoucherType { get; set; } = null!;
        
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
    }

    [Table("EVoucherToken")]
    public class EVoucherToken
    {
        [Key]
        public int TokenID { get; set; }
        
        [Required]
        public int EVoucherID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string TokenValue { get; set; } = string.Empty;
        
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? UsedDate { get; set; }
        
        // Navigation properties
        [ForeignKey("EVoucherID")]
        public virtual EVoucher EVoucher { get; set; } = null!;
    }

    [Table("EVoucherRedeemLog")]
    public class EVoucherRedeemLog
    {
        [Key]
        public int LogID { get; set; }
        
        [Required]
        public int EVoucherID { get; set; }
        
        [Required]
        public int UserID { get; set; }
        
        [Required]
        public DateTime RedeemDate { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        // Navigation properties
        [ForeignKey("EVoucherID")]
        public virtual EVoucher EVoucher { get; set; } = null!;
        
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
    }

    [Table("WalletHistory")]
    public class WalletHistory
    {
        [Key]
        public int TransactionId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int Amount { get; set; }
        
        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }

    [Table("UserSignInStats")]
    public class UserSignInStats
    {
        [Key]
        public int StatsID { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public DateTime SignTime { get; set; }
        
        [Required]
        public int SignInCount { get; set; } = 1;
        
        [Required]
        public DateTime LastSignIn { get; set; }
        
        [Required]
        public int PointsEarned { get; set; } = 0;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }

    [Table("Pet")]
    public class Pet
    {
        [Key]
        public int PetID { get; set; }
        
        [Required]
        public int UserID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string PetName { get; set; } = string.Empty;
        
        [Required]
        public int Level { get; set; } = 1;
        
        [Required]
        public int Experience { get; set; } = 0;
        
        [Required]
        [StringLength(20)]
        public string SkinColor { get; set; } = "Default";
        
        [Required]
        [StringLength(20)]
        public string BackgroundColor { get; set; } = "Default";
        
        [Required]
        public int Hunger { get; set; } = 100;
        
        [Required]
        public int Happiness { get; set; } = 100;
        
        [Required]
        public int Health { get; set; } = 100;
        
        [Required]
        public int Energy { get; set; } = 100;
        
        [Required]
        public int Cleanliness { get; set; } = 100;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        
        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
    }

    [Table("MiniGame")]
    public class MiniGame
    {
        [Key]
        public int GameID { get; set; }
        
        [Required]
        public int UserID { get; set; }
        
        [Required]
        public int Score { get; set; } = 0;
        
        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;
        
        public DateTime? EndTime { get; set; }
        
        [StringLength(50)]
        public string? GameData { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Playing"; // Playing, Completed, Aborted
        
        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
    }

    [Table("ManagerData")]
    public class ManagerData
    {
        [Key]
        public int Manager_Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ManagerName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string ManagerAccount { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string ManagerPassword { get; set; } = string.Empty;
        
        [Required]
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime LastLogin { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual ICollection<ManagerRole> ManagerRoles { get; set; } = new List<ManagerRole>();
    }

    [Table("ManagerRole")]
    public class ManagerRole
    {
        [Key]
        public int RoleID { get; set; }
        
        [Required]
        public int Manager_Id { get; set; }
        
        [Required]
        public int ManagerRolePermissionID { get; set; }
        
        [Required]
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        [ForeignKey("Manager_Id")]
        public virtual ManagerData ManagerData { get; set; } = null!;
        
        [ForeignKey("ManagerRolePermissionID")]
        public virtual ManagerRolePermission ManagerRolePermission { get; set; } = null!;
    }

    [Table("ManagerRolePermission")]
    public class ManagerRolePermission
    {
        [Key]
        public int ManagerRolePermissionID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string RoleName { get; set; } = string.Empty;
        
        public bool AdministratorPrivilegesManagement { get; set; } = false;
        public bool ShoppingPermissionManagement { get; set; } = false;
        public bool MessagePermissionManagement { get; set; } = false;
        public bool UserStatusManagement { get; set; } = false;
        public bool Pet_Rights_Management { get; set; } = false;
        public bool customer_service { get; set; } = false;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual ICollection<ManagerRole> ManagerRoles { get; set; } = new List<ManagerRole>();
    }

    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public int UserPoint { get; set; } = 0;
        
        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
        public virtual ICollection<EVoucher> EVouchers { get; set; } = new List<EVoucher>();
        public virtual ICollection<WalletHistory> WalletHistories { get; set; } = new List<WalletHistory>();
        public virtual ICollection<UserSignInStats> UserSignInStats { get; set; } = new List<UserSignInStats>();
        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
        public virtual ICollection<MiniGame> MiniGames { get; set; } = new List<MiniGame>();
    }
}

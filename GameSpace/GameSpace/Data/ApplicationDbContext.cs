using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GameSpace.Models;

namespace GameSpace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // MiniGame Area 相關的 DbSet
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponType> CouponTypes { get; set; }
        public DbSet<EVoucher> EVouchers { get; set; }
        public DbSet<EVoucherType> EVoucherTypes { get; set; }
        public DbSet<EVoucherToken> EVoucherTokens { get; set; }
        public DbSet<EVoucherRedeemLog> EVoucherRedeemLogs { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<MiniGame> MiniGames { get; set; }
        public DbSet<SignIn> SignIns { get; set; }
        public DbSet<UserPoints> UserPoints { get; set; }
        public DbSet<UserSignInStats> UserSignInStats { get; set; }
        public DbSet<ManagerData> ManagerData { get; set; }
        public DbSet<ManagerRole> ManagerRoles { get; set; }
        public DbSet<ManagerRolePermission> ManagerRolePermissions { get; set; }
        public DbSet<WalletHistory> WalletHistories { get; set; }
    }
}

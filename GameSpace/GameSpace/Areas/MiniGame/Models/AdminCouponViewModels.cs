using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminCouponIndexViewModel
    {
        public PagedResult<UserCouponReadModel> UserCoupons { get; set; } = new();
        public CouponQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminCouponDetailsViewModel
    {
        public Coupon Coupon { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminCouponEditViewModel
    {
        public Coupon Coupon { get; set; } = new();
        public List<CouponType> CouponTypes { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminCouponDeleteViewModel
    {
        public Coupon Coupon { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }
}

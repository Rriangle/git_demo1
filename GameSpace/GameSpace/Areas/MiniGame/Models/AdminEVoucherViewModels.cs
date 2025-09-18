using GameSpace.Models;

namespace GameSpace.Areas.MiniGame.Models
{
    public class AdminEVoucherIndexViewModel
    {
        public PagedResult<UserEVoucherReadModel> UserEVouchers { get; set; } = new();
        public EVoucherQueryModel Query { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminEVoucherDetailsViewModel
    {
        public Evoucher Evoucher { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminEVoucherEditViewModel
    {
        public Evoucher Evoucher { get; set; } = new();
        public List<EvoucherType> EvoucherTypes { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }

    public class AdminEVoucherDeleteViewModel
    {
        public Evoucher Evoucher { get; set; } = new();
        public string Sidebar { get; set; } = "admin";
    }
}

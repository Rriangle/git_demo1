using GameSpace.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    public class AdminWalletController : Controller
    {
        private readonly GameSpacedatabaseContext _context;

        public AdminWalletController(GameSpacedatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public class WalletTransactionModel
        {
            [Required(ErrorMessage = "用戶ID為必填")]
            public int UserId { get; set; }

            [Required(ErrorMessage = "金額為必填")]
            [Range(0.01, double.MaxValue, ErrorMessage = "金額必須大於0")]
            public decimal Amount { get; set; }

            [Required(ErrorMessage = "交易類型為必填")]
            [StringLength(50, ErrorMessage = "交易類型長度不能超過50個字元")]
            public string TransactionType { get; set; } = string.Empty;

            [StringLength(500, ErrorMessage = "備註長度不能超過500個字元")]
            public string? Description { get; set; }
        }
    }
}

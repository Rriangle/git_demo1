using Microsoft.AspNetCore.Mvc;
using GameSpace.Areas.MiniGame.Services;
using GameSpace.Areas.MiniGame.Models;
using GameSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    public class ShopController : Controller
    {
        private readonly GameSpacedatabaseContext _context;
        private readonly ILogger<ShopController> _logger;

        public ShopController(GameSpacedatabaseContext context, ILogger<ShopController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = GetCurrentUserId();
                
                // 獲取可用的優惠券類型
                var couponTypes = await _context.CouponTypes
                    .Where(ct => ct.ValidFrom <= DateTime.Now && ct.ValidTo >= DateTime.Now)
                    .Select(ct => new ShopItemViewModel
                    {
                        ItemId = ct.CouponTypeId,
                        Name = ct.Name,
                        Description = ct.Description,
                        PointsCost = ct.PointsCost,
                        ItemType = "Coupon"
                    })
                    .ToListAsync();

                // 獲取可用的電子禮券類型
                var evoucherTypes = await _context.EvoucherTypes
                    .Where(et => et.ValidFrom <= DateTime.Now && et.ValidTo >= DateTime.Now)
                    .Select(et => new ShopItemViewModel
                    {
                        ItemId = et.EvoucherTypeId,
                        Name = et.Name,
                        Description = et.Description,
                        PointsCost = et.PointsCost,
                        ItemType = "EVoucher"
                    })
                    .ToListAsync();

                var allItems = couponTypes.Concat(evoucherTypes).ToList();

                var userWallet = await _context.UserWallets
                    .FirstOrDefaultAsync(w => w.UserId == userId);

                var viewModel = new ShopViewModels.ShopIndexViewModel
                {
                    Items = allItems,
                    Wallet = userWallet ?? new UserWallet(),
                    SenderID = userId
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得商店頁面時發生錯誤");
                return View(new ShopViewModels.ShopIndexViewModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuyCoupon(int couponTypeId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var couponType = await _context.CouponTypes.FindAsync(couponTypeId);

                if (couponType == null || couponType.ValidFrom > DateTime.Now || couponType.ValidTo < DateTime.Now)
                {
                    return Json(new { success = false, message = "優惠券類型不存在或已下架" });
                }

                var wallet = await _context.UserWallets.FirstOrDefaultAsync(w => w.UserId == userId);
                if (wallet == null)
                {
                    return Json(new { success = false, message = "錢包不存在" });
                }

                if (wallet.UserPoint < couponType.PointsCost)
                {
                    return Json(new { success = false, message = "點數不足" });
                }

                // 扣除點數
                wallet.UserPoint -= couponType.PointsCost;

                // 創建優惠券
                var coupon = new Coupon
                {
                    CouponCode = GenerateCouponCode(),
                    CouponTypeId = couponTypeId,
                    UserId = userId,
                    IsUsed = false
                };

                _context.Coupons.Add(coupon);
                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    message = "優惠券購買成功！",
                    remainingPoints = wallet.UserPoint,
                    couponCode = coupon.CouponCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "購買優惠券時發生錯誤");
                return Json(new { success = false, message = "購買失敗" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuyEVoucher(int evoucherTypeId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var evoucherType = await _context.EvoucherTypes.FindAsync(evoucherTypeId);

                if (evoucherType == null || evoucherType.ValidFrom > DateTime.Now || evoucherType.ValidTo < DateTime.Now)
                {
                    return Json(new { success = false, message = "電子禮券類型不存在或已下架" });
                }

                var wallet = await _context.UserWallets.FirstOrDefaultAsync(w => w.UserId == userId);
                if (wallet == null)
                {
                    return Json(new { success = false, message = "錢包不存在" });
                }

                if (wallet.UserPoint < evoucherType.PointsCost)
                {
                    return Json(new { success = false, message = "點數不足" });
                }

                // 扣除點數
                wallet.UserPoint -= evoucherType.PointsCost;

                // 創建電子禮券
                var evoucher = new Evoucher
                {
                    EvoucherCode = GenerateEVoucherCode(),
                    EvoucherTypeId = evoucherTypeId,
                    UserId = userId,
                    IsUsed = false
                };

                _context.Evouchers.Add(evoucher);
                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    message = "電子禮券購買成功！",
                    remainingPoints = wallet.UserPoint,
                    evoucherCode = evoucher.EvoucherCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "購買電子禮券時發生錯誤");
                return Json(new { success = false, message = "購買失敗" });
            }
        }

        private string GenerateCouponCode()
        {
            return "CPN-" + DateTime.Now.ToString("yyMMdd") + "-" + Guid.NewGuid().ToString("N")[..6].ToUpper();
        }

        private string GenerateEVoucherCode()
        {
            return "EV-" + DateTime.Now.ToString("yyMMdd") + "-" + Guid.NewGuid().ToString("N")[..6].ToUpper();
        }

        private int GetCurrentUserId()
        {
            return 1;
        }
    }
}

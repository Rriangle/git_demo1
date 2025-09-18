using GameSpace.Areas.MiniGame.Models;
using GameSpace.Areas.MiniGame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    [Authorize(Policy = "CanManageShopping")] // Requires Shopping permission
    public class AdminWalletController : Controller
    {
        private readonly IMiniGameAdminService _adminService;
        private readonly IMiniGameAdminAuthService _authService;

        public AdminWalletController(IMiniGameAdminService adminService, IMiniGameAdminAuthService authService)
        {
            _adminService = adminService;
            _authService = authService;
        }

        public async Task<IActionResult> Index(WalletQueryModel query)
        {
            var wallets = await _adminService.GetUserWalletsAsync(query);
            return View(wallets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdjustPoints(AdminAdjustUserPointsModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.AdjustUserPointsAsync(model.UserId, model.PointsChange, model.Description ?? "");
                if (success)
                {
                    TempData["SuccessMessage"] = "點數調整成功";
                }
                else
                {
                    TempData["ErrorMessage"] = "點數調整失敗";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IssueCoupon(AdminAdjustUserCouponModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.IssueCouponToUserAsync(model.UserId, model.CouponId, model.Quantity);
                if (success)
                {
                    TempData["SuccessMessage"] = "優惠券發放成功";
                }
                else
                {
                    TempData["ErrorMessage"] = "優惠券發放失敗";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCoupon(int userCouponId)
        {
            var success = await _adminService.RemoveUserCouponAsync(userCouponId);
            if (success)
            {
                TempData["SuccessMessage"] = "優惠券移除成功";
            }
            else
            {
                TempData["ErrorMessage"] = "優惠券移除失敗";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IssueEVoucher(AdminAdjustUserEVoucherModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.IssueEVoucherToUserAsync(model.UserId, model.EVoucherId, model.Quantity);
                if (success)
                {
                    TempData["SuccessMessage"] = "電子禮券發放成功";
                }
                else
                {
                    TempData["ErrorMessage"] = "電子禮券發放失敗";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveEVoucher(int userEVoucherId)
        {
            var success = await _adminService.RemoveUserEVoucherAsync(userEVoucherId);
            if (success)
            {
                TempData["SuccessMessage"] = "電子禮券移除成功";
            }
            else
            {
                TempData["ErrorMessage"] = "電子禮券移除失敗";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> WalletHistory(WalletQueryModel query)
        {
            var history = await _adminService.GetWalletHistoryAsync(query);
            return View(history);
        }
    }
}

using GameSpace.Areas.MiniGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameSpace.Areas.MiniGame.Filters
{
    /// <summary>
    /// MiniGame 模組權限檢查過濾器
    /// 根據模組類型檢查對應權限
    /// </summary>
    public sealed class MiniGameModulePermissionAttribute : ActionFilterAttribute
    {
        private readonly string _module;

        /// <summary>
        /// 初始化模組權限檢查
        /// </summary>
        /// <param name="module">模組名稱：Pet, UserWallet, UserSignIn, MiniGame</param>
        public MiniGameModulePermissionAttribute(string module)
        {
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        /// <summary>
        /// 在 Action 執行前檢查模組權限
        /// </summary>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IMiniGameAdminAuthService>();
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<MiniGameModulePermissionAttribute>>();
            
            // 取得當前管理員 ID
            var managerId = authService.GetCurrentManagerId(context.HttpContext.User);
            
            if (managerId == null)
            {
                logger.LogWarning("MiniGame 模組存取被拒：無法識別管理員身份, Module={Module}, TraceId={TraceId}", 
                    _module, context.HttpContext.TraceIdentifier);
                
                context.Result = new ForbidResult();
                return;
            }

            // 檢查模組權限
            var hasPermission = await authService.CanAccessModuleAsync(managerId.Value, _module);
            
            if (!hasPermission)
            {
                logger.LogWarning("MiniGame 模組存取被拒：權限不足, ManagerId={ManagerId}, Module={Module}, TraceId={TraceId}", 
                    managerId.Value, _module, context.HttpContext.TraceIdentifier);
                
                // 返回 403 Forbidden 或重導向到無權限頁面
                context.Result = new ViewResult
                {
                    ViewName = "~/Areas/MiniGame/Views/Shared/NoPermission.cshtml",
                    StatusCode = 403
                };
                return;
            }

            // 權限檢查通過，繼續執行
            logger.LogInformation("MiniGame 模組存取允許: ManagerId={ManagerId}, Module={Module}, Action={Action}, TraceId={TraceId}", 
                managerId.Value, _module, context.ActionDescriptor.DisplayName, context.HttpContext.TraceIdentifier);
            
            await next();
        }
    }
}

using GameSpace.Areas.MiniGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameSpace.Areas.MiniGame.Filters
{
    /// <summary>
    /// MiniGame Admin 專用?��??�濾??
    /// 檢查 ManagerRolePermission.PetRightsManagement 權�?
    /// </summary>
    public sealed class MiniGameAdminAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// ??Action ?��??�檢??MiniGame Admin 權�?
        /// </summary>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IMiniGameAdminAuthService>();
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<MiniGameAdminAuthorizeAttribute>>();
            
            // ?��??��?管�???ID
            var managerId = authService.GetCurrentManagerId(context.HttpContext.User);
            
            if (managerId == null)
            {
                logger.LogWarning("MiniGame Admin 存�?被�?：無法�??�管?�員身份, TraceId={TraceId}", 
                    context.HttpContext.TraceIdentifier);
                
                context.Result = new ForbidResult();
                return;
            }

            // 檢查 PetRightsManagement 權�?
            var hasPermission = await authService.CanAccessAsync(managerId.Value);
            
            if (!hasPermission)
            {
                logger.LogWarning("MiniGame Admin 存�?被�?：�??��?�? ManagerId={ManagerId}, TraceId={TraceId}", 
                    managerId.Value, context.HttpContext.TraceIdentifier);
                
                // 返�? 403 Forbidden ?��?導�??�無權�??�面
                context.Result = new ViewResult
                {
                    ViewName = "~/Areas/MiniGame/Views/Shared/NoPermission.cshtml",
                    StatusCode = 403
                };
                return;
            }

            // 權�?檢查?��?，繼續執�?
            logger.LogInformation("MiniGame Admin 存�??�許: ManagerId={ManagerId}, Action={Action}, TraceId={TraceId}", 
                managerId.Value, context.ActionDescriptor.DisplayName, context.HttpContext.TraceIdentifier);
            
            await next();
        }
    }
}

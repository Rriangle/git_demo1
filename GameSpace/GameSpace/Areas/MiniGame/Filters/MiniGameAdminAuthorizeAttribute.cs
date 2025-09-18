using GameSpace.Areas.MiniGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameSpace.Areas.MiniGame.Filters
{
    /// <summary>
    /// MiniGame Admin å°ˆç”¨?ˆæ??æ¿¾??
    /// æª¢æŸ¥ ManagerRolePermission.PetRightsManagement æ¬Šé?
    /// </summary>
    public sealed class MiniGameAdminAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// ??Action ?·è??æª¢??MiniGame Admin æ¬Šé?
        /// </summary>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetRequiredService<IMiniGameAdminAuthService>();
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<MiniGameAdminAuthorizeAttribute>>();
            
            // ?–å??¶å?ç®¡ç???ID
            var managerId = authService.GetCurrentManagerId(context.HttpContext.User);
            
            if (managerId == null)
            {
                logger.LogWarning("MiniGame Admin å­˜å?è¢«æ?ï¼šç„¡æ³•è??¥ç®¡?†å“¡èº«ä»½, TraceId={TraceId}", 
                    context.HttpContext.TraceIdentifier);
                
                context.Result = new ForbidResult();
                return;
            }

            // æª¢æŸ¥ PetRightsManagement æ¬Šé?
            var hasPermission = await authService.CanAccessAsync(managerId.Value);
            
            if (!hasPermission)
            {
                logger.LogWarning("MiniGame Admin å­˜å?è¢«æ?ï¼šæ??ä?è¶? ManagerId={ManagerId}, TraceId={TraceId}", 
                    managerId.Value, context.HttpContext.TraceIdentifier);
                
                // è¿”å? 403 Forbidden ?–é?å°å??°ç„¡æ¬Šé??é¢
                context.Result = new ViewResult
                {
                    ViewName = "~/Areas/MiniGame/Views/Shared/NoPermission.cshtml",
                    StatusCode = 403
                };
                return;
            }

            // æ¬Šé?æª¢æŸ¥?šé?ï¼Œç¹¼çºŒåŸ·è¡?
            logger.LogInformation("MiniGame Admin å­˜å??è¨±: ManagerId={ManagerId}, Action={Action}, TraceId={TraceId}", 
                managerId.Value, context.ActionDescriptor.DisplayName, context.HttpContext.TraceIdentifier);
            
            await next();
        }
    }
}

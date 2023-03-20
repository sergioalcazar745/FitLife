using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;
using Fitlife.Helpers;

namespace FitLife.Filters
{
    public class EncryptedParameters : ActionFilterAttribute
    {
        private CryptoEngine cryptoEngine;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (context.HttpContext.Request.Query.Count != 0)
            {
                this.cryptoEngine =
                    context.HttpContext.RequestServices.GetService<CryptoEngine>();
                var encrypted = context.HttpContext.Request.Query["nombre"].FirstOrDefault();
                var decrypted = this.cryptoEngine.Decrypt(encrypted);
                var collection = HttpUtility.ParseQueryString(decrypted);
                var actionParameters = context.ActionDescriptor.Parameters;
                context.RouteData.Values["nombre"] = decrypted;
                context.ActionArguments["nombre"] = decrypted;
            }

        }
    }
}
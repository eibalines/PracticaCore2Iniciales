using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;


namespace PracticaCore2Iniciales.Filters
{
    public class AthorizeUserAttribute : AuthorizeAttribute,
        IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                string controller = context.RouteData.Values["Controller"].ToString();
                string action = context.RouteData.Values["Action"].ToString();
                ITempDataProvider provider =
                    context.HttpContext.RequestServices.GetService(typeof(ITempDataProvider)) as ITempDataProvider;
                var TempData = provider.LoadTempData(context.HttpContext);
                TempData["controller"] = controller;
                TempData["action"] = action;
                provider.SaveTempData(context.HttpContext, TempData);
                context.Result = this.GetRouteRedirect("Manage", "Login");

            }


        }

        private RedirectToRouteResult GetRouteRedirect
            (string controller, string action)
        {
            RouteValueDictionary ruta =
                new RouteValueDictionary(new
                {
                    controller = controller,
                    action = action
                });
            RedirectToRouteResult result =
                new RedirectToRouteResult(ruta);
            return result;
        }


    }
}
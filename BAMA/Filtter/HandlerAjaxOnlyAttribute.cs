using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAMA.Filtter
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HandlerAjaxOnlyAttribute: ActionMethodSelectorAttribute
    {
        public bool Ignore { get; set; }
        public HandlerAjaxOnlyAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (Ignore)
                return true;
            //return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
            return true;
        }
    }
}

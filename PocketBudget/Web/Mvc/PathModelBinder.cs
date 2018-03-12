using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocketBudget.Web.Mvc
{
    public class PathModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            return model;
        }
    }
}
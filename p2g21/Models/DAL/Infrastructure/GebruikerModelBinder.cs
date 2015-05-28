using p2g21.Models.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace p2g21.Infrastructure
{
    public class GebruikerModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext context, ModelBindingContext bindingContext)
        {
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                IGebruikerRepository rep = (IGebruikerRepository)DependencyResolver.Current.GetService(typeof(IGebruikerRepository));
                return rep.FindGebruikerByName(context.HttpContext.User.Identity.Name);
            }
            return null;
        }
    }
}
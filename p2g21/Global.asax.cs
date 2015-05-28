using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using p2g21.Models.DAL;
using p2g21.App_Start;
using System.Web.Optimization;
using p2g21.Infrastructure;
using System.Web.Security;
using System.Security.Principal;
using p2g21.Models.Domain;

namespace p2g21
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new BachelorProefInitializer());
            new BachelorProefContext().Gebruikers.ToList();
            ModelBinders.Binders.Add(typeof(Gebruiker), new GebruikerModelBinder());

        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e) {
            HttpCookie authCookie = 
                Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null) {
                FormsAuthenticationTicket authTicket = 
                                  FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(new Char[] { ',' });
                GenericPrincipal userPrincipal =
                       new GenericPrincipal(new GenericIdentity(authTicket.Name),
                                            roles);
            Context.User = userPrincipal;
    }
  }
}
    }

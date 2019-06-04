using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Castle.Windsor;
using HospitalIS.BusinessLayer.Config;
using HospitalIS.WebApplication.App_Start.Windsor;

namespace HospitalIS.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            // configure DI            
            Container.Install(new BusinessLayerInstaller());
            Container.Install(new PresentationLayerInstaller());

            // set controller factory
            var controllerFactory = new WindsorControllerFactory(Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }

        /*protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];

            if (authCookie == null)
            {
                return;
            }
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }
            if (authTicket == null)
            {
                return;
            }
            string[] roles = authTicket.UserData.Split(new char[] { '|' });
            FormsIdentity id = new FormsIdentity(authTicket);
            GenericPrincipal principal = new GenericPrincipal(id, roles);

            Context.User = principal;
        }*/
    }
}

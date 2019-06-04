using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using HospitalIS.BusinessLayer.Config;
using HospitalWebAPI.App_Start.Windsor;

namespace HospitalWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container = new WindsorContainer();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            container.Install(new WebApiInstaller());
            container.Install(new BusinessLayerInstaller());

            GlobalConfiguration.Configuration.Services
                .Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }

        public override void Dispose()
        {
            container.Dispose();
            base.Dispose();
        }
    }
}

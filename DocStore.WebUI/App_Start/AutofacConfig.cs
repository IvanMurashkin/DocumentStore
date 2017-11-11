using Autofac;
using Autofac.Integration.Mvc;
using DocStore.Domain.NHiberante;
using DocStore.WebUI.Infrastructure.Abstract;
using DocStore.WebUI.Infrastructure.Concrete;
using Services.Manager;
using System.Web.Mvc;

namespace DocStore.WebUI.App_Start {
    public class AutofacConfig {

        public static void ConfigureContainer() {
  
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<DocumentManager>().As<IDocumentManager>();
            builder.RegisterType<FormAuthProvider>().As<IAuthProvider>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

}

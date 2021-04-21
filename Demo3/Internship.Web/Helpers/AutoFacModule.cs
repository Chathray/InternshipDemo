using Autofac;
using Internship.Application;
using Internship.Infrastructure;

namespace Internship.Web
{
    public class AutoFacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceFactory>()
                .As<IServiceFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UptimeService>()
                .SingleInstance();

            var assembly_app = typeof(InternService).Assembly;
            builder.RegisterAssemblyTypes(assembly_app)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var assembly_inf = typeof(InternRepository).Assembly;
            builder.RegisterAssemblyTypes(assembly_inf)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
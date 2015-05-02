using LifeTracker;
using LifeTracker.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(LifeTracker.Startup))]
namespace LifeTracker
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            
            HttpConfiguration config = new HttpConfiguration();

            var webApiRequestLifestyle = new WebApiRequestLifestyle();
            var dependencyContainer = new LifeTrackerApiDependencyContainer(config, webApiRequestLifestyle);

            // Simple Injector's web API request scoping in an OWIN context won't work without the following voodoo:
            app.Use(async (context, next) =>
            {
                using (var scope = dependencyContainer.Container.BeginExecutionContextScope())
                {
                    await next.Invoke();
                }
            });

            config.DependencyResolver = dependencyContainer;
            
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
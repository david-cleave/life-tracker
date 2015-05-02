using LifeTracker.Controllers;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace LifeTracker
{
    public class LifeTrackerApiDependencyContainer : IDependencyResolver
    {
        private Container _container { get; set; }

        private Lifestyle _lifeStyle { get; set; }

        public LifeTrackerApiDependencyContainer(HttpConfiguration httpConfiguration, Lifestyle lifeStyle)
        {
            _container = new Container();
            _container.Options.AllowOverridingRegistrations = true;
            
            _lifeStyle = lifeStyle;

            _container.Register<OrdersController>(lifeStyle);
            _container.Register<IOrderManager, OrderManager>(lifeStyle);
            _container.Register<IOrderManager, OrderManager2>(lifeStyle);
        }

        public Container Container
        {
            get
            {
                return _container;
            }
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container).GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}
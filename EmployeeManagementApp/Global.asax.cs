using EmployeeManagementApp.Controllers;
using EmployeeRepositorys.Implement;
using EmployeeRepositorys.Interfaces;
using EmployeeServices.Implements;
using EmployeeServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;

namespace EmployeeManagementApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new UnityContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            UnityConfig.RegisterComponents();
            container.RegisterType<EmployeeController>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

    }
}


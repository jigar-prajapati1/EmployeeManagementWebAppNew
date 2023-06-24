using EmployeeRepositorys.Implement;
using EmployeeRepositorys.Interfaces;
using EmployeeServices.Implements;
using EmployeeServices.Interfaces;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace EmployeeManagementApp
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; set; }

        public static void RegisterComponents()
        {
            Container = new UnityContainer();
            Container.AddExtension(new Diagnostic());

            Container.RegisterType<IEmployeeService, EmployeeService>();
            Container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}
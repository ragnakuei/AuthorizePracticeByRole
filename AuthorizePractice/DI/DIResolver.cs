using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AuthorizePractice.Controllers;
using DAL.EF;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizePractice.DI
{
    public class DIResolver : IDependencyResolver
    {
        #region implement

        public object GetService(Type serviceType)
        {
            var result = _provider.GetService(serviceType);
            DiPropery(result);
            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _provider.GetServices(serviceType);
        }

        #endregion

        private static readonly ServiceProvider _provider;

        static DIResolver()
        {
            var service = new ServiceCollection();

            service.AddDbContext<AccountDbContext>(options => options
                                                             .UseSqlServer(ConfigurationManager.ConnectionStrings["AuthorizePractice"].ConnectionString)
                                                             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
                                                   ServiceLifetime.Scoped,
                                                   ServiceLifetime.Scoped);
            service.AddTransient<AuthController>();
            service.AddTransient<HomeController>();
            service.AddTransient<ErrorController>();
            service.AddTransient<MemberController>();

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IAuthorizeRepository, AuthorizeRepository>();
            service.AddScoped<IRouteRoleRepository, RouteRoleRepository>();
            _provider = service.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            var result = _provider.GetService<T>();
            DiPropery(result);
            return result;
        }

        private static object GetServiceByType(Type serviceType)
        {
            var result = _provider.GetService(serviceType);
            DiPropery(result);
            return result;
        }

        private static void DiPropery<T>(T instance)
        {
            var properties = GetInjectableProperties(instance?.GetType());

            foreach (var prop in properties)
            {
                try
                {
                    var propertyInstance = GetServiceByType(prop.PropertyType);
                    if (propertyInstance != null)
                    {
                        // DI Property
                        prop.SetValue(instance, propertyInstance);
                    }
                }
                catch (Exception)
                {
                    // DiFactory 沒辦法解析的型別
                }
            }
        }

        /// <summary>
        /// 取得有 CustomAttribute DiPropertyAttribute 的 PropertyInfos
        /// </summary>
        private static IEnumerable<PropertyInfo> GetInjectableProperties(Type type)
        {
            return type?.GetProperties()
                        .Where(p => ContainsDiPropertyAttribute(p))
                ?? Enumerable.Empty<PropertyInfo>() ;
        }

        /// <summary>
        /// 判斷 PropertyInfo 是否有 CustomAttribute DiPropertyAttribute
        /// </summary>
        private static bool ContainsDiPropertyAttribute(PropertyInfo p)
        {
            return p.CustomAttributes.Any(a => a.AttributeType == typeof(DiPropertyAttribute));
        }
    }
}
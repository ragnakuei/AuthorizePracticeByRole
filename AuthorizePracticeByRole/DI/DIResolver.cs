using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AuthorizePracticeByRole.Controllers;
using AuthorizePracticeByRole.Controllers.Admin;
using DAL.Repository;
using DAL.Repository.@interface;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizePracticeByRole.DI
{
    public class DiResolver : IDependencyResolver
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

        static DiResolver()
        {
            var service = new ServiceCollection();
            
            service.AddTransient<AuthController>();
            service.AddTransient<HomeController>();
            service.AddTransient<ErrorController>();
            service.AddTransient<MemberController>();
            service.AddTransient<GroupsController>();

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IAuthorizeRepository, AuthorizeRepository>();
            service.AddScoped<IGroupRepository, GroupRepository>();
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
using System;

namespace SharedLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : Attribute
    {
        public CustomAuthorizeAttribute(string function)
        {
            Function = function;
        }

        public CustomAuthorizeAttribute(string function, string action)
        {
            Function = function;
            Action = action;
        }

        public string Action { get; }
        public string Function { get; }
    }
}
using System.Reflection;

namespace Portfolio.Management.Domain.Abstractions
{
    public static class CreatorHelper
    {
        private static readonly string _applicationIdentity = Assembly.GetExecutingAssembly().GetName().Name;
        private static readonly string _systemUser = Environment.UserName;
        private static readonly string _hostname = Environment.MachineName;

        public static string GetEntityCreatorIdentity() => $"{_systemUser}@{_hostname} ({_applicationIdentity})";
    }
}
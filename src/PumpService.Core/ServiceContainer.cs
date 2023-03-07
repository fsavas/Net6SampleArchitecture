using Microsoft.Extensions.DependencyInjection;

namespace PumpService.Core
{
    public static class ServiceContainer
    {
        public static IServiceScope Scope { get; set; }

        //public static IServiceScopeFactory ScopeFactory { get; set; }
        //public static IServiceProvider Provider { get; set; }
    }
}
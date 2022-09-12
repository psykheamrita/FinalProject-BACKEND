using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Caching.Microsoft;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceColection)
        {
            serviceColection.AddMemoryCache();
            serviceColection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceColection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceColection.AddSingleton<Stopwatch>();
        }
    }
}

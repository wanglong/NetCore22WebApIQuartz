using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebApIQuartz.Extensions
{
    public class ZookeeperBuilder : IZookeeperBuilder
    {
        public IServiceCollection Services { get; }

        public ZookeeperBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}

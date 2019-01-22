using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace WebApIQuartz.Extensions
{
    public interface IZookeeperBuilder
    {
        IServiceCollection Services { get; }
    }
}

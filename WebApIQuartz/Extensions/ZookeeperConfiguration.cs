using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApIQuartz.Extensions
{
    public class ZookeeperConfiguration
    {
        public IConfiguration Configuration { get; }

        public ZookeeperConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebApIQuartz.Extensions
{
    public class ZookeeperConfigurationOptions : IConfigureOptions<ZookeeperOptions>
    {
        private readonly IConfiguration _configuration;


        public ZookeeperConfigurationOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void Configure(ZookeeperOptions options)
        {
            System.Diagnostics.Debug.WriteLine("zookeeper配置类，适配方法。"
            + Newtonsoft.Json.JsonConvert.SerializeObject(options));
        }
    }
}

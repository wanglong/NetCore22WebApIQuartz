using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApIQuartz.Extensions
{
    public class ZookeeperOptions
    {
        /// <param name="connectstring">comma separated host:port pairs, each corresponding to a zk server. 
        /// e.g. "127.0.0.1:3000,127.0.0.1:3001,127.0.0.1:3002" If the optional chroot suffix is used the example would look like:
        /// "127.0.0.1:3000,127.0.0.1:3001,127.0.0.1:3002/app/a" where the client would be rooted at "/app/a" and all paths would 
        /// be relative to this root - ie getting/setting/etc... "/foo/bar" would result in operations being run on "/app/a/foo/bar" 
        /// (from the server perspective).</param>
        public string Connectstring { get; set; }

        public int SessionTimeout { get; set; }

        public int SessionId { get; set; }

        public string SessionPwd { get; set; }

        public bool IsReadOnly { get; set; }
    }
}

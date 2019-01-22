using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using org.apache.zookeeper;
using org.apache.zookeeper.data;
using static org.apache.zookeeper.ZooKeeper;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using static org.apache.zookeeper.Watcher.Event;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace WebApIQuartz.Extensions
{
    public interface IZookeeperService
    {
        Task<string> CreateZNode(string path, string data, CreateMode createMode, List<ACL> aclList);

        Task<DataResult> GetDataAsync(string path, Watcher watcher, bool isSync);

        Task<Stat> SetDataAsync(string path, string data, bool isSync);

        Task<ChildrenResult> GetChildrenAsync(string path, Watcher watcher, bool isSync);

        void DeleteNode(string path, String tempNode);

        string GetDataByLockNode(string path, string sequenceName, List<ACL> aclList, out string tempNodeOut);

        void Close(string tempNode);
    }
}

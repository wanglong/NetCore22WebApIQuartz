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
    public class WaitLockWatch : Watcher
    {
        private AutoResetEvent _autoResetEvent;
        private ILogger _logger;

        private string _path;

        private ZookeeperService _zookeeperService;

        public string _tempNode;

        public WaitLockWatch(AutoResetEvent autoResetEvent
        , ZookeeperService zookeeperService
        , ILogger logger, string path
        , string tempNode)
        {
            _autoResetEvent = autoResetEvent;
            _zookeeperService = zookeeperService;
            _logger = logger;
            _path = path;
            _tempNode = tempNode;
        }

        public override Task process(WatchedEvent @event)
        {
            _logger.LogDebug("{0}节点下子节点发生改变，激发监视方法。", _path);
            var childList = _zookeeperService.GetChildrenAsync(_path, null, true).Result;
            if (childList == null || childList.Children == null || childList.Children.Count < 1)
            {
                _logger.LogDebug("获取子序列失败，计数为零.path:{0}", _path);
                return Task.FromResult(0);
            }
            var top = childList.Children.OrderBy(or => or).First();
            if (_path + "/" + top == _tempNode)
            {
                _logger.LogDebug("释放阻塞");
                _autoResetEvent.Set();
            }

            return Task.FromResult(0);
        }
    }
}

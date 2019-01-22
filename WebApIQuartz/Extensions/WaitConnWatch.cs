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

    internal class WaitConnWatch : Watcher
    {
        private AutoResetEvent _autoResetEvent;
        private ILogger _logger;

        public WaitConnWatch(AutoResetEvent autoResetEvent
        , ILogger logger)
        {
            _autoResetEvent = autoResetEvent;
            _logger = logger;
        }

        public override Task process(WatchedEvent @event)
        {
            _logger.LogDebug("watch激发,回掉状态：{0}", @event.getState().ToString());
            if (@event.getState() == KeeperState.SyncConnected
            || @event.getState() == KeeperState.ConnectedReadOnly)
            {
                _logger.LogDebug("释放阻塞");
                _autoResetEvent.Set();
            }
            return Task.FromResult(0);
        }
    }
}

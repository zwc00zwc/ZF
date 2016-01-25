using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Redis
{
    public class RedisManager
    {
        /// <summary>
        /// 连接池进行创建连接
        /// </summary>
        /// <returns></returns>
        public IRedisClient GetClient(List<string> readwritehosts,List<string> readhosts)
        {
            RedisClientManagerConfig redisconfig = new RedisClientManagerConfig();
            redisconfig.MaxReadPoolSize = 5;
            redisconfig.MaxWritePoolSize = 5;
            redisconfig.AutoStart = true;

            PooledRedisClientManager poolredis = new PooledRedisClientManager(readwritehosts, readhosts, redisconfig);
            return poolredis.GetClient();
        }
    }
}

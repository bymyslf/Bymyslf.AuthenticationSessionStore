namespace Bymyslf.AuthenticationSessionStore.Redis
{
    using System;
    using Bymyslf.AuthenticationSessionStore.Extensions;
    using Bymyslf.AuthenticationSessionStore.Utils;
    using StackExchange.Redis;

    public class RedisConnection : IRedisConnection
    {
        private readonly ConnectionMultiplexer connection;

        public RedisConnection(RedisConnectionSettings settings)
        {
            Guard.Against<ArgumentException>(settings.IsNull(), "settings can't be null");

            var options = ConfigurationOptions.Parse(settings.ConnectionString);
            options.DefaultDatabase = settings.Database;

            this.connection = ConnectionMultiplexer.Connect(options);
        }

        public IDatabase Database
        {
            get
            {
                return this.connection.GetDatabase();
            }
        }
    }
}
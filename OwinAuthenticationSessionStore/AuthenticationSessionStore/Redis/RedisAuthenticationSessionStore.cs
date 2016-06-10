namespace Bymyslf.AuthenticationSessionStore.Redis
{
    using System;
    using System.Threading.Tasks;
    using Bymyslf.AuthenticationSessionStore.Extensions;
    using Bymyslf.AuthenticationSessionStore.Utils;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using StackExchange.Redis;

    public class RedisAuthenticationSessionStore : IAuthenticationSessionStore
    {
        private const int DefaultKeyExpiration = 20;

        private readonly IRedisConnection redisConnection;
        private readonly IAuthenticationTicketSerializer serializer;

        public RedisAuthenticationSessionStore(IRedisConnection redisConnection, IAuthenticationTicketSerializer serializer)
        {
            Guard.Against<ArgumentNullException>(redisConnection.IsNull(), "redisConnection can't be null");
            Guard.Against<ArgumentNullException>(serializer.IsNull(), "serializer can't be null");

            this.redisConnection = redisConnection;
            this.serializer = serializer;
        }

        private IDatabase Redis
        {
            get { return this.redisConnection.Database; }
        }

        public Task RemoveAsync(string key)
        {
            Guard.Against<ArgumentNullException>(key.IsNull(), "RemoveAsync - key can't be null");

            this.Redis.KeyDelete(key);
            return Task.FromResult(0);
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            Guard.Against<ArgumentNullException>(key.IsNull(), "RenewAsync - key can't be null");
            Guard.Against<ArgumentNullException>(ticket.IsNull(), "RenewAsync - ticket can't be null");

            var json = this.serializer.Serialize(ticket);
            this.Redis.StringSet(key, json, ticket.Properties.ExpiresUtcToTimeSpanOrDefault(DefaultKeyExpiration));
            return Task.FromResult(0);
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            Guard.Against<ArgumentNullException>(key.IsNull(), "RetrieveAsync - key can't be null");

            var result = this.Redis.StringGet(key);
            return Task.FromResult(this.serializer.Deserialize(result));
        }

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            Guard.Against<ArgumentNullException>(ticket.IsNull(), "StoreAsync - ticket can't be null");

            var key = Guid.NewGuid().ToString("N");
            var json = this.serializer.Serialize(ticket);
            this.Redis.StringSet(key, json, ticket.Properties.ExpiresUtcToTimeSpanOrDefault(DefaultKeyExpiration));
            return Task.FromResult(key);
        }
    }
}
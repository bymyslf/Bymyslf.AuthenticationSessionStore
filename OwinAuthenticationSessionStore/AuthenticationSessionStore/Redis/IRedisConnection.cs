namespace Bymyslf.AuthenticationSessionStore.Redis
{
    using StackExchange.Redis;

    public interface IRedisConnection
    {
        IDatabase Database { get; }
    }
}
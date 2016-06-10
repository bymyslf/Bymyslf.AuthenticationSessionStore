namespace Bymyslf.AuthenticationSessionStore.Redis
{
    public class RedisConnectionSettings
    {
        public RedisConnectionSettings(
            int database,
            string connectionString)
        {
            this.Database = database;
            this.ConnectionString = connectionString;
        }

        public int Database { get; private set; }
        public string ConnectionString { get; private set; }
    }
}
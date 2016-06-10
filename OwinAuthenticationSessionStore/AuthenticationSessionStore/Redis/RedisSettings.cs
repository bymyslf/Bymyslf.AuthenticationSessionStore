namespace Bymyslf.AuthenticationSessionStore.Redis
{
    using System.Configuration;

    public class RedisSettings : ConfigurationSection
    {
        private static RedisSettings settings = ConfigurationManager.GetSection("redisSettings") as RedisSettings;

        public static RedisSettings Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("database", IsRequired = true)]
        [IntegerValidator]
        public int Database
        {
            get { return (int)this["database"]; }
            set { this["database"] = value; }
        }

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }
    }
}
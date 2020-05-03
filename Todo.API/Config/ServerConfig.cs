namespace Todo.API
{
    public class ServerConfig
    {
        public MongoDBConfig MongoDb
        {
            get;
            private set;
        } = new MongoDBConfig();
    }
}
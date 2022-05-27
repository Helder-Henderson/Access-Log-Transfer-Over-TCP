using MongoDB.Driver;

namespace TCPServer
{
    internal class MongoDB
    {
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<Log> Collection { get; set; }
        public List<WriteModel<Log>> ListWrites { get; set; }

        public MongoDB()
        {
            MongoClient = new MongoClient(Settings.ConnectionString);
            Database = MongoClient.GetDatabase(Settings.DatabaseName);
            Collection = Database.GetCollection<Log>(Settings.ColletionName);
            ListWrites = new();
        }

        public void InsertToList(Log data)
        {
            ListWrites.Add(new InsertOneModel<Log>(data));        
        }

        public async Task<long> StartBulkInsert()
        {
            var resultWrites = await Collection.BulkWriteAsync(ListWrites, new BulkWriteOptions()
            {
                IsOrdered = false,
            });

            return resultWrites.InsertedCount;
        }

    }
}

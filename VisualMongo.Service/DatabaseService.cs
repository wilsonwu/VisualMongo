using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace VisualMongo.Service
{
    public class DatabaseService
    {
        public static MongoDatabase GetDatabase()
        {
            return null;
        }

        public static List<string> GetDatabases()
        {
            return VisualMongoDB.VMongoServer.GetDatabaseNames().ToList();
        }
    }
}

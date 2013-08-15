using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace VisualMongo.Service
{
    public class DatabaseService
    {
        public static bool ConnectDatabase(string databaseName)
        {
            bool result = false;
            try
            {
                VisualMongoDB.VMongoDatabase = VisualMongoDB.VMongoServer.GetDatabase(databaseName);
                return result;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public static List<string> GetDatabaseNames()
        {
            return VisualMongoDB.VMongoServer.GetDatabaseNames().ToList();
        }

        public static DatabaseStatsResult GetDatabaseStats(string databaseName)
        {
            DatabaseStatsResult result = null;
            try
            {
                result = VisualMongoDB.VMongoDatabase.GetStats();
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}

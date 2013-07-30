using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using VisualMongo.Entity;

namespace VisualMongo.Service
{
    public static class ConnectionService
    {
        public static bool InitConnection(bool isOverWrite = false)
        {
            bool result = false;
            bool isNeedCreate = false;
            string appDataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VisualMongo.xml");
            if (isOverWrite == false)
            {
                if (File.Exists(appDataFilePath) == false)
                {
                    isNeedCreate = true;
                }
            }
            else
            {
                isNeedCreate = true;
            }

            if (isNeedCreate == true)
            {
                //Create appdata file.
                try
                {
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }

        public static ConnectionEntity AddConnection()
        {
            return null;
        }

        public static ConnectionEntity UpdateConnection()
        {
            return null;
        }

        public static bool DeleteConnection()
        {
            return false;
        }

        public static ConnectionEntity GetConnection()
        {
            return null;
        }

        public static List<ConnectionEntity> GetAllConnections()
        {
            return null;
        }

        public static bool Connect()
        {
            bool result = false;
            try
            {
                VisualMongoDB.VMongoClient = new MongoClient("mongodb://localhost:27017");
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace VisualMongo.Service
{
    public class ServerService
    {
        public static string GetServer()
        {
            string result = string.Empty;
            try
            {
                VisualMongoDB.VMongoServer = VisualMongoDB.VMongoClient.GetServer();
                result = VisualMongoDB.VMongoServer.Instance.Address.Host;
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }
            return result;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace VisualMongo.Service
{
    public class ServerService
    {
        public static bool GetServer()
        {
            bool result = false;
            try
            {
                VisualMongoDB.VMongoServer = VisualMongoDB.VMongoClient.GetServer();
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

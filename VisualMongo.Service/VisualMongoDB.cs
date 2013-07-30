using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace VisualMongo.Service
{
    public static class VisualMongoDB
    {
        private static MongoClient _vMongoClient;

        public static MongoClient VMongoClient
        {
            get { return _vMongoClient; }
            set { _vMongoClient = value; }
        }

        private static MongoServer _vMongoServer;

        public static MongoServer VMongoServer
        {
            get { return VisualMongoDB._vMongoServer; }
            set { VisualMongoDB._vMongoServer = value; }
        }

    }
}

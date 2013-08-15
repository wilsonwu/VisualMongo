using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualMongo.Service
{
    public class CollectionService
    {
        public static List<string> GetCollectionNames()
        {
            return VisualMongoDB.VMongoDatabase.GetCollectionNames().ToList();
        }
    }
}

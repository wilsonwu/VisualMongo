using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace VisualMongo.Service
{
    public class EntityService
    {
        public static List<BsonDocument> GetEntities(string collectionName, int page, int count)
        {
            return VisualMongoDB.VMongoDatabase.GetCollection(collectionName).FindAll().SetLimit(count * page).Skip(count * (page - 1)).Take(count).ToList();
        }

        public static DataTable ConvertToDataTable(List<BsonDocument> bsons)
        {
            DataTable table = new DataTable();
            foreach (var bson in bsons)
            {
                for (int i = 0; i < bson.Elements.Count(); i++)
                {
                    if (table.Columns.Contains(bson.GetElement(i).Name) == false)
                    {
                        table.Columns.Add(bson.GetElement(i).Name);
                    }
                }
                table.Rows.Add(bson.GetElement(0).Value);
            }

            for (int j = 0; j < bsons.Count; j++)
            {
                foreach (var column in table.Columns)
                {
                    BsonElement tempBson = new BsonElement();
                    bsons[j].TryGetElement(column.ToString(), out tempBson);
                    if (tempBson != null)
                    {
                        table.Rows[j][column.ToString()] = tempBson.Value;
                    }
                }
            }

            return table;
        }

        public static StringBuilder ConvertToText(List<BsonDocument> bsons)
        {
            StringBuilder result = new StringBuilder();
            foreach (var bson in bsons)
            {
                result.Append(bson.ToJson());
            }

            return result;
        }
    }
}

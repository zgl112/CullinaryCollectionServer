using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace baseAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("caption")]
        public string Caption { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("history")]
        public string History { get; set; }

        [BsonElement("historyImage")]
        public string HistoryImage { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("stock")]
        public double Stock { get; set; }

        [BsonElement("tags")]
        public string Tags { get; set; }

        [BsonElement("country")]
        public string Country { get; set; }
    }
}

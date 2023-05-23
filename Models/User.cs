﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace baseAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("name")]
        public string Name { get; set; }


        [BsonElement("email")]
        public string Email { get; set; }


        [BsonElement("password")]
        public string Password { get; set; }


        [BsonElement("salt")]
        public string Salt { get; set; }

    }
}

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReadingIsGood.Domain.Models
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

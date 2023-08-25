using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkiomBackendDevTest.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public Dictionary<string, object> CustomFields { get; set; } = new Dictionary<string, object>();
    }
}

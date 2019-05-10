using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace AddToCartBusiness.Data
{
    [ProtoContract]
    public class Product
    {
        [ProtoMember(1)]
        [BsonId]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string ProductName { get; set; }

        [ProtoMember(3)]
        public long Quantity { get; set; }

        [ProtoMember(4)]
        public decimal Price { get; set; }
    }
}
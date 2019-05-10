using ProtoBuf;
using System.Collections.Generic;

namespace AddToCartBusiness.Data
{
    [ProtoContract]
    public class Cart
    {
        [ProtoMember(1)]
        public long Id { get; set; }

        [ProtoMember(2)]
        public decimal TotalAmount { get; set; }

        [ProtoMember(3)]
        public List<Product> Products { get; set; }

        [ProtoMember(4)]
        public bool IsComplete { get; set; }
    }
}
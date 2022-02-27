using DataAcess.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.DataModels
{
    public class Product : IProduct
    {
        public string Id { get; set; }
        [BsonIgnoreIfNull]
        public string Name { get; set; }
        [BsonIgnoreIfNull]
        public string DisplayName { get; set; }
    }
}

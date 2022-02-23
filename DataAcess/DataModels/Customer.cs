using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.DataModels
{
    public class Customer : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> ProductId { get; set; }
    }
}

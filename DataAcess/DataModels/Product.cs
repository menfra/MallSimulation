using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.DataModels
{
    public class Product : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}

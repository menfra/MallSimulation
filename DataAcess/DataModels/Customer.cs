using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.DataModels
{
    public class Customer : ICustomer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Product> Products { get; set; }
        public bool DoneShopping { get; set; } = false;
        public string CurrentStandJoined { get; set; }
    }
}

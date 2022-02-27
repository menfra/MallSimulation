using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.DataModels
{
    public class Transactions
    {
        public string CustomerId { get; set; }
        public List<Product> BoughtProducts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogics.DTO
{
    public class CustomerDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<ProductDTO> Products { get; set; }
        public bool DoneShopping { get; set; } = false;
        public string CurrentStandJoined { get; set; }

    }
}

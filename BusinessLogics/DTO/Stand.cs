using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogics.DTO
{
    public class StandDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> ProductId { get; set; }
    }
}

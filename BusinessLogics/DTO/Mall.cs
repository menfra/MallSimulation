using DataAcess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogics.DTO
{
    public class MallDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public States OpenedState { get; set; }
        public Capacity MallCapacity { get; set; }
        public int OpenClosedDuration { get; set; }
    }
}

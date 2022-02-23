using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.Interfaces
{
    public interface IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}

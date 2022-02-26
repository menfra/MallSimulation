using DataAcess.Interfaces;
using DataAcess.Enums;

namespace DataAcess.DataModels
{
    public class Mall : IMall
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public States OpenedState { get; set; }
        public Capacity MallCapacity { get; set; }
        public int OpenClosedDuration { get; set; }
    }
}

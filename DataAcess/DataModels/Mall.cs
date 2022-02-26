using DataAcess.Interfaces;
using DataAcess.Enums;

namespace DataAcess.DataModels
{
    public class Mall : IMall
    {
        public string Id { get; set; } = "A1";
        public string Name { get; set; } = "Accra Mall";
        public string DisplayName { get; set; } = "Accra Mall Spintex";
        public States OpenedState { get; set; } = States.Closed;
        public Capacity MallCapacity { get; set; } = Capacity.UnLimited;
        public int OpenClosedDuration { get; set; }
    }
}

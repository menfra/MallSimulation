using DataAcess.DataModels;
using DataAcess.DataServices;
using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogics.StandBusiness
{
    public class StandBusiness : IStandBusiness
    {
        private readonly Stand _stand;
        private readonly IDataServices _dataServices;
        public StandBusiness(IStand stand, IDataServices dataServices)
        {
            _stand = stand as Stand;
            _dataServices = dataServices;
        }
        public Task<Stand> AddStand(Stand stand)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStand(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Stand> GetStand(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Stand>> GetStands()
        {
            throw new NotImplementedException();
        }

        public Task<Stand> UpdateStand(string Id, Stand stand)
        {
            throw new NotImplementedException();
        }
    }
}

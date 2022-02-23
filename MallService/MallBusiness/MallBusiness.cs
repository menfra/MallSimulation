using DataAcess.DataModels;
using DataAcess.DataServices;
using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MallService.MallBusinessLayer
{
    public class MallBusiness : IMallBusiness
    {
        private readonly Mall _mall;
        private readonly IDataServices _dataServices;
        public MallBusiness(IEntity mall, IDataServices dataServices)
        {
            _mall = mall as Mall;
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

        public Task<Mall> GetMallOpenedStatus()
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

        public Task<Mall> SetMallCapacity()
        {
            throw new NotImplementedException();
        }

        public Task<Mall> SetMallOpenedStatus()
        {
            throw new NotImplementedException();
        }

        public Task<Stand> UpdateStand(string Id, Stand stand)
        {
            throw new NotImplementedException();
        }
    }
}

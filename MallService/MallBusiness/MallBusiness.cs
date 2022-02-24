using DataAcess.DataModels;
using DataAcess.DataServices;
using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DataAcess.Enums;
using System.Timers;
using MallService.Extensions;

namespace MallService.MallBusinessLayer
{
    public class MallBusiness : IMallBusiness
    {
        private readonly Mall _mall;
        private readonly IDataServices _dataServices;
        private readonly IConfiguration _configuration;

        public MallBusiness(IEntity mall, IDataServices dataServices, IConfiguration configuration)
        {
            _mall = mall as Mall;
            _dataServices = dataServices;
            _configuration = configuration;

            _mall.OpenedState = ConfigValues.MallOpenedStatus;
        }

        public Mall GetMallOpenedStatus()
        {
            return _mall;
        }

        public Mall GetMallOpenCloseDuration()
        {
            try
            {
                int.TryParse(ConfigValues.MallOpenCloseDuration, out int duration);
                _mall.OpenClosedDuration = duration;
                return _mall;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Task<Stand> AddStand(Stand stand)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStand(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Stand> GetStand(string Id)
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

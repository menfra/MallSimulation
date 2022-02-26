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
using BusinessLogics.Extensions;
using BusinessLogics.DTO;
using BusinessLogics.StandBusiness;

namespace BusinessLogics.MallBusiness
{
    public class MallBusiness : IMallBusiness
    {
        private readonly Mall _mall;
        private readonly IDataServices _dataServices;
        private readonly IConfiguration _configuration;
        private readonly IStandBusiness _standBusiness;

        public MallBusiness(IMall mall, IDataServices dataServices, IConfiguration configuration, IStandBusiness standBusiness)
        {
            _mall = mall as Mall;
            _dataServices = dataServices;
            _configuration = configuration;
            _standBusiness = standBusiness;

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
                _ = int.TryParse(ConfigValues.MallOpenCloseDuration, out int duration);
                _mall.OpenClosedDuration = duration;
                return _mall;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Stand> AddStand(StandDTO standDTO)
        {
            try
            {
                return await _standBusiness.AddStand(standDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteStand(string Id)
        {
            try
            {
                await _standBusiness.DeleteStand(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Stand> GetStand(string Id)
        {
            try
            {
                return await _standBusiness.GetStand(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Stand>> GetStands()
        {
            try
            {
                return await _standBusiness.GetStands();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateStand(StandDTO standDTO)
        {
            try
            {
                await _standBusiness.UpdateStand(standDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

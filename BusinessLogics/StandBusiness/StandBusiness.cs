using DataAcess.DataModels;
using DataAcess.DataServices;
using DataAcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogics.DTO;
using AutoMapper;

namespace BusinessLogics.StandBusiness
{
    public class StandBusiness : IStandBusiness
    {
        private readonly Stand _stand;
        private readonly IDataServices _dataServices;
        private readonly IMapper _mapper;
        public StandBusiness(IStand stand, IDataServices dataServices, IMapper mapper)
        {
            _stand = stand as Stand;
            _dataServices = dataServices;
            _mapper = mapper;
        }

        public async Task<Stand> AddStand(StandDTO standDTO)
        {
            try
            {
                var stand = _mapper.Map<Stand>(standDTO);
                return await _dataServices.AddData(stand);
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
                await _dataServices.DeleteData<Stand>(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteStandBulk(List<string> Ids)
        {
            try
            {
                await _dataServices.DeleteDataBulk<Stand>(Ids);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteStandProductId(string Id)
        {
            try
            {
                var stands = await GetStands();

                var standsWithProductMatch = stands.Where(p => p.ProductIds.Any(id => id == Id)).ToList();
                await _dataServices.DeleteDataBulk<Stand>(standsWithProductMatch.Select(i=>i.Id).ToList());
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
                return await _dataServices.GetDataByID<Stand>(Id);
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
                return await _dataServices.GetAllData<Stand>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Stand>> GetStandsProductId(string Id)
        {
            try
            {
                var stands = await _dataServices.GetAllData<Stand>();
                return stands.Where(s => s.ProductIds.Any(id=>id == Id)).ToList();
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
                //TODO: Only the duration can be updated

                var stand = _mapper.Map<Stand>(standDTO);
                await _dataServices.UpSertData(stand.Id, stand);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateStandBulk(List<StandDTO> standDTOs)
        {
            try
            {
                // Must be changed to an proper bulk update.
                foreach(var standDTO in standDTOs)
                {
                    var stand = _mapper.Map<Stand>(standDTO);
                    await _dataServices.UpSertData(stand.Id, stand);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

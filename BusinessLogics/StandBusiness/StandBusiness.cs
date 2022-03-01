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
                // Get the stand
                var stand = await GetStand(Id);
                var stands = await GetStands();
                // Get the next potential stand
                var nextPotentialstand = stands.Where(s => s.Id != Id).FirstOrDefault();

                if(stand != null && nextPotentialstand != null)
                {
                    nextPotentialstand.CustomerQueue.AddRange(stand.CustomerQueue);
                    await _dataServices.UpSertData(nextPotentialstand.Id, nextPotentialstand);
                    await _dataServices.DeleteData<Stand>(Id);
                }
                else
                {
                    await _dataServices.DeleteData<Stand>(Id);
                }
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

                // Get the stand
                var stands = await GetStands();
                // Get the next potential stands
                var nextPotentialstands = stands.Where(s => !Ids.Any(i => i == s.Id)).ToList();

                if (nextPotentialstands != null)
                {
                    var potentialStand = nextPotentialstands.FirstOrDefault();
                    foreach(var Id in Ids)
                    {
                        var stand = await GetStand(Id);
                        if (stand != null && potentialStand != null)
                        {
                            potentialStand.CustomerQueue.AddRange(stand.CustomerQueue);
                            await _dataServices.UpSertData(potentialStand.Id, potentialStand);
                            await _dataServices.DeleteData<Stand>(Id);
                        }
                    }
                   
                }
                else
                {
                    await _dataServices.DeleteDataBulk<Stand>(Ids);
                }
                // Assign customers to the next potential stand.
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
                // Get the stands
                // Get the next potential stand
                var stands = await GetStands();

                var standsWithProductMatch = stands.Where(s => s.Product.Id == Id).ToList();
                await _dataServices.DeleteDataBulk<Stand>(standsWithProductMatch.Select(i=>i.Id).ToList());
                // Assign customers to the next potential stand.
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
                return stands.Where(s => s.Product.Id == Id).ToList();
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
                // A customer is added to a queue on a stand
                var stand = await GetStand(standDTO.Id);
                if (stand == null)
                    return;

                // modify the Duration for the stand
                stand.Duration = standDTO.Duration;
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

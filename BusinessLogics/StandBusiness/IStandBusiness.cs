using BusinessLogics.DTO;
using DataAcess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogics.StandBusiness
{
    public interface IStandBusiness
    {
        Task<Stand> GetStand(string Id);
        Task<List<Stand>> GetStands();
        Task<List<Stand>> GetStandsProductId(string Id);

        Task<Stand> AddStand(StandDTO standDTO);

        Task UpdateStand(StandDTO standDTO);
        Task UpdateStandBulk(List<StandDTO> standDTO);
        //Task UpdateStandProductId(List<StandDTO> standDTO);

        Task DeleteStand(string Id);
        Task DeleteStandBulk(List<string> Ids);
        Task DeleteStandProductId(string Id);
    }
}

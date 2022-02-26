using BusinessLogics.DTO;
using DataAcess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogics.MallBusiness
{
    public interface IMallBusiness
    {
        Mall GetMallOpenedStatus();
        Mall GetMallOpenCloseDuration();
        Task<Stand> GetStand(string Id);
        Task<List<Stand>> GetStands();
        Task<Stand> AddStand(StandDTO standDTO);
        Task UpdateStand(StandDTO standDTO);
        Task DeleteStand(string Id);
    }
}

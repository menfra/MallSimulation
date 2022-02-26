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
        Task<Mall> SetMallOpenedStatus();
        Mall GetMallOpenCloseDuration();
        Task<Mall> SetMallCapacity();
        Task<Stand> GetStand(string Id);
        Task<List<Stand>> GetStands();
        Task<Stand> AddStand(Stand stand);
        Task<Stand> UpdateStand(string Id, Stand stand);
        Task DeleteStand(string Id);
    }
}

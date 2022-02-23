using DataAcess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandService.StandBusinessLayer
{
    interface IStandBusiness
    {

        Task<Stand> GetStand(string Id);
        Task<List<Stand>> GetStands();
        Task<Stand> AddStand(Stand stand);
        Task<Stand> UpdateStand(string Id, Stand stand);
        Task DeleteStand(string Id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.DataServices
{
    public class PostgresDataServices : IDataServices
    {
        public Task AddData<T>(T tdata)
        {
            throw new NotImplementedException();
        }

        public Task DeleteData<T>(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllData<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetDataByID<T>(string Id)
        {
            throw new NotImplementedException();
        }

        public Task UpSertData<T>(string Id, T tdata)
        {
            throw new NotImplementedException();
        }
    }
}

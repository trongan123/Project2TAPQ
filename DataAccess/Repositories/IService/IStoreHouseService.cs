using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IStoreHouseService
    {
        List<StoreHouse> getAll();
        StoreHouse FindStoreHouseById(string id);
        void AddStoreHouse(StoreHouse a);
        StoreHouse FindStoreHouseByIdAcc(string idacc);

        void UpdateStoreHouse(StoreHouse a);

        void DeleteStoreHouse(StoreHouse a);

    }
}

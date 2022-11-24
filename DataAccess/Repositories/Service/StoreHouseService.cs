using BusinessObjects.Models;
using DataAccess.DAO;
using DataAccess.Repositories.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Service
{
    public class StoreHouseService : IStoreHouseService
    {
        public void AddStoreHouse(StoreHouse a) => StoreHouseDAO.AddStoreHouse(a);

        public void DeleteStoreHouse(StoreHouse a) => StoreHouseDAO.DeleteStoreHouse(a);

        public StoreHouse FindStoreHouseById(string id) => StoreHouseDAO.FindStoreHouseById(id);

        public StoreHouse FindStoreHouseByIdAcc(string idacc) => StoreHouseDAO.FindStoreHouseByIdAcc(idacc);

        public List<StoreHouse> getAll() => StoreHouseDAO.getAll();

        public void UpdateStoreHouse(StoreHouse a) => StoreHouseDAO.UpdateStoreHouse(a);
    }
}

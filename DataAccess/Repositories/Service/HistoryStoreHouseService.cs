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
    public class HistoryStoreHouseService : IHistoryStoreHouseService
    {
        public void AddHistoryStoreHouse(HistoryStoreHouse a) => HistoryStoreHouseDAO.AddHistoryStoreHouse(a);

        public void DeleteHistoryStoreHouse(HistoryStoreHouse a) => HistoryStoreHouseDAO.DeleteHistoryStoreHouse(a);

        public HistoryStoreHouse FindHistoryStoreHouseById(string id) => HistoryStoreHouseDAO.FindHistoryStoreHouseById(id);

        public List<HistoryStoreHouse> getAll(string ids) => HistoryStoreHouseDAO.getAll(ids);

        public void UpdateHistoryStoreHouse(HistoryStoreHouse a) => HistoryStoreHouseDAO.UpdateHistoryStoreHouse(a);
    }
}

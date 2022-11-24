using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IHistoryStoreHouseService
    {
        List<HistoryStoreHouse> getAll(string ids);
        HistoryStoreHouse FindHistoryStoreHouseById(string id);
        void AddHistoryStoreHouse(HistoryStoreHouse a);

        void UpdateHistoryStoreHouse(HistoryStoreHouse a);

        void DeleteHistoryStoreHouse(HistoryStoreHouse a);

    }
}

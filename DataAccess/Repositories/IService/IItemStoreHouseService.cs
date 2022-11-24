using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IItemStoreHouseService
    {
        List<ItemStoreHouse> getAll(string ids);
        ItemStoreHouse FindItemStoreHouseById(string id);
        void AddItemStoreHouse(ItemStoreHouse a);

        void UpdateItemStoreHouse(ItemStoreHouse a);

        void DeleteItemStoreHouse(ItemStoreHouse a);

    }
}

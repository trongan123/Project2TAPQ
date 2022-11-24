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
    public class ItemStoreHouseService : IItemStoreHouseService
    {
        public void AddItemStoreHouse(ItemStoreHouse a) => ItemStoreHouseDAO.AddItemStoreHouse(a);

        public void DeleteItemStoreHouse(ItemStoreHouse a) => ItemStoreHouseDAO.DeleteItemStoreHouse(a);

        public ItemStoreHouse FindItemStoreHouseById(string id) => ItemStoreHouseDAO.FindItemStoreHouseById(id);

        public List<ItemStoreHouse> getAll(string ids) => ItemStoreHouseDAO.getAll(ids);

        public void UpdateItemStoreHouse(ItemStoreHouse a) => ItemStoreHouseDAO.UpdateItemStoreHouse(a);
    }
}

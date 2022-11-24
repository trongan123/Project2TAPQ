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
    public class ItemCategoryService : IItemCategoryService
    {
        public void AddItemCategory(ItemCategory a) => ItemCategoryDAO.AddItemCategory(a);

        public void DeleteItemCategory(ItemCategory a) => ItemCategoryDAO.DeleteItemCategory(a);

        public ItemCategory FindItemCategoryById(string id) => ItemCategoryDAO.FindItemCategoryById(id);

        public List<ItemCategory> getAll() => ItemCategoryDAO.getAll();

        public void UpdateItemCategory(ItemCategory a) => ItemCategoryDAO.UpdateItemCategory(a);
    }
}

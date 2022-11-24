using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IItemCategoryService
    {
        List<ItemCategory> getAll();
        ItemCategory FindItemCategoryById(string id);
        void AddItemCategory(ItemCategory a);

        void UpdateItemCategory(ItemCategory a);

        void DeleteItemCategory(ItemCategory a);

    }
}

using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ItemCategoryDAO
    {
        public static List<ItemCategory> getAll()
        {

            List<ItemCategory> list = new List<ItemCategory>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.ItemCategories.Where(a => a.Status != 0).ToList();
            }

            return list;
        }



        public static ItemCategory FindItemCategoryById(string id)
        {
            ItemCategory a = new ItemCategory();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.ItemCategories.SingleOrDefault(x => x.IdItemCategory.Equals(id));
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }
        public static string GetIDCuoi()
        {
            List<ItemCategory> lists;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    lists = context.ItemCategories.Select((ItemCategory i) => i).ToList();
                    if (lists.Count <= 0)
                    {
                        return "IC00000001";
                    }
                    string iDCuoi = lists.Last().IdItemCategory;
                    return $"IC{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddItemCategory(ItemCategory a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdItemCategory = GetIDCuoi();
                    context.ItemCategories.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateItemCategory(ItemCategory a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<ItemCategory>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteItemCategory(ItemCategory a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.ItemCategories.SingleOrDefault(
                        x => x.IdItemCategory == a.IdItemCategory);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<ItemCategory>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}

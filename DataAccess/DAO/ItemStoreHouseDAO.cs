using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ItemStoreHouseDAO
    {
        public static List<ItemStoreHouse> getAll(string ids)
        {

            List<ItemStoreHouse> list = new List<ItemStoreHouse>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.ItemStoreHouses.Where(a=> a.IdSHouse.Equals(ids)).ToList();
                foreach (var a in list)
                {
                    a.IdItemCategoryNavigation = ItemCategoryDAO.FindItemCategoryById(a.IdItemCategory);
                    a.IdSHouseNavigation = StoreHouseDAO.FindStoreHouseById(a.IdSHouse);

                }
            }

            return list;
        }



        public static ItemStoreHouse FindItemStoreHouseById(string id)
        {
            ItemStoreHouse a = new ItemStoreHouse();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.ItemStoreHouses.SingleOrDefault(x => x.IdItemStoreHouse.Equals(id));
                    if (a != null)
                    {
                        a.IdItemCategoryNavigation = ItemCategoryDAO.FindItemCategoryById(a.IdItemCategory);
                        a.IdSHouseNavigation = StoreHouseDAO.FindStoreHouseById(a.IdSHouse);

                    }
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
            List<ItemStoreHouse> lists;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    lists = context.ItemStoreHouses.Select((ItemStoreHouse i) => i).ToList();
                    if (lists.Count <= 0)
                    {
                        return "ISH0000001";
                    }
                    string iDCuoi = lists.Last().IdItemCategory;
                    return $"ISH{int.Parse(iDCuoi.Substring(3)) + 1:000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void AddItemStoreHouse(ItemStoreHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdItemStoreHouse = GetIDCuoi();
                    context.ItemStoreHouses.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateItemStoreHouse(ItemStoreHouse a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<ItemStoreHouse>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteItemStoreHouse(ItemStoreHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.ItemStoreHouses.SingleOrDefault(
                        x => x.IdItemStoreHouse == a.IdItemStoreHouse);
                    if (p1 != null)
                    {
                        
                        context.Remove(p1);
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

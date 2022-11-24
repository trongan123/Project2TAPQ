using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class HistoryStoreHouseDAO
    {
        public static List<HistoryStoreHouse> getAll(string ids)
        {

            List<HistoryStoreHouse> list = new List<HistoryStoreHouse>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.HistoryStoreHouses.Where(a => a.Status != 0 && a.IdSHouse.Equals(ids)).ToList();
                foreach (var a in list)
                {
                    a.IdItemCategoryNavigation = ItemCategoryDAO.FindItemCategoryById(a.IdItemCategory);
                    a.IdSHouseNavigation = StoreHouseDAO.FindStoreHouseById(a.IdSHouse);
                    a.IdStaffNavigation = AccountDAO.FindAccountById(a.IdStaff);
                }
            }

            return list;
        }



        public static HistoryStoreHouse FindHistoryStoreHouseById(string id)
        {
            HistoryStoreHouse a = new HistoryStoreHouse();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.HistoryStoreHouses.SingleOrDefault(x => x.IdHistoryStoreHouse.Equals(id));
                    if (a != null)
                    {
                        a.IdItemCategoryNavigation = ItemCategoryDAO.FindItemCategoryById(a.IdItemCategory);
                        a.IdSHouseNavigation = StoreHouseDAO.FindStoreHouseById(a.IdSHouse);
                        a.IdStaffNavigation = AccountDAO.FindAccountById(a.IdStaff);
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
            List<HistoryStoreHouse> lists;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    lists = context.HistoryStoreHouses.Select((HistoryStoreHouse i) => i).ToList();
                    if (lists.Count <= 0)
                    {
                        return "HSH0000001";
                    }
                    string iDCuoi = lists.Last().IdHistoryStoreHouse;
                    return $"HSH{int.Parse(iDCuoi.Substring(3)) + 1:000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void AddHistoryStoreHouse(HistoryStoreHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdHistoryStoreHouse = GetIDCuoi();
                    context.HistoryStoreHouses.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateHistoryStoreHouse(HistoryStoreHouse a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<HistoryStoreHouse>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteHistoryStoreHouse(HistoryStoreHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.HistoryStoreHouses.SingleOrDefault(
                        x => x.IdHistoryStoreHouse == a.IdHistoryStoreHouse);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<HistoryStoreHouse>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

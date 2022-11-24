using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class StoreHouseDAO
    {
        public static List<StoreHouse> getAll()
        {

            List<StoreHouse> list = new List<StoreHouse>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.StoreHouses.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);
                }
            }

            return list;
        }



        public static StoreHouse FindStoreHouseByIdAcc(string idacc)
        {
            StoreHouse a = new StoreHouse();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.StoreHouses.SingleOrDefault(x => x.IdUser.Equals(idacc));
                    if (a != null)
                    {
                        a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }
        public static StoreHouse FindStoreHouseById(string id)
        {
            StoreHouse a = new StoreHouse();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.StoreHouses.SingleOrDefault(x => x.IdSHouse.Equals(id));
                    if (a != null)
                    {
                        a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);
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
            List<StoreHouse> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.StoreHouses.Select((StoreHouse i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "SH00000001";
                    }
                    string iDCuoi = accounts.Last().IdSHouse;
                    return $"SH{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddStoreHouse(StoreHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdSHouse = GetIDCuoi();
                    context.StoreHouses.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateStoreHouse(StoreHouse a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<StoreHouse>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteStoreHouse(StoreHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.StoreHouses.SingleOrDefault(
                        x => x.IdSHouse == a.IdSHouse);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<StoreHouse>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

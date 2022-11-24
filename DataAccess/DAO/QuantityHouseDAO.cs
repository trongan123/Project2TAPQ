using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class QuantityHouseDAO
    {
        public static List<QuantityHouse> getAll()
        {

            List<QuantityHouse> list = new List<QuantityHouse>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.QuantityHouses.ToList();
                foreach (var a in list)
                {
                    a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);
                }
            }

            return list;
        }



        public static QuantityHouse FindQuantityHouseById(string id)
        {
            QuantityHouse a = new QuantityHouse();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.QuantityHouses.SingleOrDefault(x => x.IdQuantity.Equals(id));
                    if (a != null)
                    {
                        a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);
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
            List<QuantityHouse> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.QuantityHouses.Select((QuantityHouse i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "QH00000001";
                    }
                    string iDCuoi = accounts.Last().IdQuantity;
                    return $"QH{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddQuantityHouse(QuantityHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdQuantity = GetIDCuoi();
                    context.QuantityHouses.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateQuantityHouse(QuantityHouse a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<QuantityHouse>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteQuantityHouse(QuantityHouse a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.QuantityHouses.SingleOrDefault(
                        x => x.IdQuantity == a.IdQuantity);
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

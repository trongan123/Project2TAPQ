using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProvinceDAO
    {
        public static List<Province> getAll()
        {

            List<Province> list = new List<Province>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Provinces.Where(a => a.Status != 0).ToList();
            }

            return list;
        }



        public static Province FindProvinceById(string id)
        {
            Province a = new Province();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Provinces.SingleOrDefault(x => x.IdProvince.Equals(id));

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
            List<Province> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.Provinces.Select((Province i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "PR00000001";
                    }
                    string iDCuoi = accounts.Last().IdProvince;
                    return $"PR{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void AddProvince(Province a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdProvince = GetIDCuoi();
                    context.Provinces.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateProvince(Province a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<Province>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteProvince(Province a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Provinces.SingleOrDefault(
                        x => x.IdProvince == a.IdProvince);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<Province>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

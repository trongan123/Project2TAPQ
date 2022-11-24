using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DistrictDAO
    {
        public static List<District> getAll()
        {

            List<District> list = new List<District>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Districts.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdProvinceNavigation = ProvinceDAO.FindProvinceById(a.IdProvince);
                }
            }

            return list;
        }
         public static string GetIDCuoi()
        {
            List<District> lists;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    lists = context.Districts.Select((District i) => i).ToList();
                    if (lists.Count <= 0)
                    {
                        return "D000000001";
                    }
                    string iDCuoi = lists.Last().IdDistrict;
                    return $"D{int.Parse(iDCuoi.Substring(1)) + 1:00000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public static District FindDistrictById(string id)
        {
            District a = new District();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Districts.SingleOrDefault(x => x.IdDistrict.Equals(id));
                    if (a != null)
                    {
                        a.IdProvinceNavigation = ProvinceDAO.FindProvinceById(a.IdProvince);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }


        public static void AddDistrict(District a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdDistrict = GetIDCuoi();
                    context.Districts.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateDistrict(District a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<District>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteDistrict(District a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Districts.SingleOrDefault(
                        x => x.IdDistrict == a.IdDistrict);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<District>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

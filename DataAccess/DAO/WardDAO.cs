using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class WardDAO
    {
        public static List<Ward> getAll()
        {

            List<Ward> list = new List<Ward>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Wards.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdDistrictNavigation = DistrictDAO.FindDistrictById(a.IdDistrict);
                }
            }

            return list;
        }



        public static Ward FindWardById(string id)
        {
            Ward a = new Ward();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Wards.SingleOrDefault(x => x.IdWard.Equals(id));
                    if (a != null)
                    {
                        a.IdDistrictNavigation = DistrictDAO.FindDistrictById(a.IdDistrict);
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
            List<Ward> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.Wards.Select((Ward i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "W000000001";
                    }
                    string iDCuoi = accounts.Last().IdWard;
                    return $"W{int.Parse(iDCuoi.Substring(1)) + 1:00000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void AddWard(Ward a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdWard = GetIDCuoi();
                    context.Wards.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateWard(Ward a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<Ward>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteWard(Ward a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Wards.SingleOrDefault(
                        x => x.IdWard == a.IdWard);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<Ward>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

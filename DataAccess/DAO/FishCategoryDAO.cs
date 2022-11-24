using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class FishCategoryDAO
    {
        public static List<FishCategory> getAll()
        {

            List<FishCategory> list = new List<FishCategory>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.FishCategories.Where(a => a.Status != 0).ToList();

            }

            return list;
        }
        public static List<FishCategory> getAllbyStatus(int status)
        {

            List<FishCategory> list = new List<FishCategory>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.FishCategories.Where(a => a.Status == status).ToList();

            }

            return list;
        }



        public static FishCategory FindFishCategoryById(string id)
        {
            FishCategory a = new FishCategory();
            try
            {
                using (var context = new _2TAPQDBContext())
                {

                    a = context.FishCategories.SingleOrDefault(x => x.IdFcategory.Equals(id));

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
            List<FishCategory> list;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    list = context.FishCategories.Select((FishCategory i) => i).ToList();
                    if (list.Count <= 0)
                    {
                        return "FC00000001";
                    }
                    string iDCuoi = list.Last().IdFcategory;
                    return $"FC{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddFishCategory(FishCategory a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdFcategory = GetIDCuoi();
                    context.FishCategories.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateFishCategory(FishCategory a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<FishCategory>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void ConfirmFishCategory(FishCategory a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.FishCategories.SingleOrDefault(
                        x => x.IdFcategory == a.IdFcategory);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<FishCategory>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteFishCategory(FishCategory a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.FishCategories.SingleOrDefault(
                        x => x.IdFcategory == a.IdFcategory);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<FishCategory>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PondDAO
    {
        public static List<Pond> getAllList()
        {

            List<Pond> list = new List<Pond>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Ponds.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);
                    a.IdFcategoryNavigation = FishCategoryDAO.FindFishCategoryById(a.IdFcategory);
                }
            }

            return list;
        }
        public static List<Pond> getAll(string idacc)
        {

            List<Pond> list = new List<Pond>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Ponds.Where(a => a.Status != 0 && a.IdAcc.Equals(idacc)).ToList();
                foreach (var a in list)
                {
                    a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);
                    a.IdFcategoryNavigation = FishCategoryDAO.FindFishCategoryById(a.IdFcategory);
                }
            }

            return list;
        }

        public static List<Pond> getAllForCoo(string idRoom)
        {
            List<Member> member = MemberDAO.getAllMember(idRoom);
              List<Pond> list = new List<Pond>();
            foreach(Member m in member)
            {
                var listp = getAll(m.IdUser);
                list.AddRange(listp);
            }
            return list;
        }




        public static Pond FindPondById(string id)
        {
            Pond a = new Pond();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Ponds.SingleOrDefault(x => x.IdPond.Equals(id));
                    if (a != null)
                    {
                        a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);
                        a.IdFcategoryNavigation = FishCategoryDAO.FindFishCategoryById(a.IdFcategory);
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
            List<Pond> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.Ponds.Select((Pond i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "P000000001";
                    }
                    string iDCuoi = accounts.Last().IdPond;
                    return $"P{int.Parse(iDCuoi.Substring(1)) + 1:00000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddPond(Pond a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdPond = GetIDCuoi();
                    context.Ponds.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdatePond(Pond a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<Pond>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeletePond(Pond a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Ponds.SingleOrDefault(
                        x => x.IdPond == a.IdPond);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<Pond>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

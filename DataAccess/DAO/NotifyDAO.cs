using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class NotifyDAO
    {
        public static List<Notify> getAllList()
        {

            List<Notify> list = new List<Notify>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Notifies.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);
                    
                }
            }

            return list;
        }

        public static List<Notify> getAll(string idacc)
        {

            List<Notify> list = new List<Notify>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Notifies.Where(a => a.Status != 0 && a.IdAcc.Equals(idacc)).ToList();
                foreach (var a in list)
                {
                    a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);
                    
                }
            }

            return list;
        }

        public static List<Notify> getAllByType(string Type)
        {

            List<Notify> list = new List<Notify>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Notifies.Where(a => a.Status != 0 && a.Type.Equals(Type)).ToList();
                foreach (var a in list)
                {
                    a.IdAccNavigation = AccountDAO.FindAccountById(a.IdAcc);

                }
            }

            return list;
        }

        public static List<Notify> getAllForCoo(string idRoom)
        {
            List<Member> member = MemberDAO.getAllMember(idRoom);
            List<Notify> list = new List<Notify>();
            foreach (Member m in member)
            {
                var listp = getAll(m.IdUser);
                list.AddRange(listp);
            }
            return list;
        }




        public static Notify FindNotifyById(string id)
        {
            Notify a = new Notify();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Notifies.SingleOrDefault(x => x.IdNotify.Equals(id));
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
        public static string GetIDCuoi(int i = 1)
        {
            List<Notify> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.Notifies.Select((Notify i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "N000000001";
                    }
                    string iDCuoi = accounts.Last().IdNotify;
                    return $"N{int.Parse(iDCuoi.Substring(1)) + i:00000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddNotify(Notify a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdNotify = GetIDCuoi();
                    context.Notifies.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void AddNotifyCoop(string idcoop,Notify a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdNotify = GetIDCuoi();
                    context.Notifies.Add(a);
                    a.IdNotify = GetIDCuoi();
                    a.IdAcc = idcoop;
                    context.Notifies.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateNotify(Notify a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<Notify>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void Readed(List<Notify> a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    foreach (var item in a)
                    {
                        item.Status = 1;
                        context.Entry<Notify>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteNotify(Notify a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Notifies.SingleOrDefault(
                        x => x.IdNotify == a.IdNotify);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<Notify>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

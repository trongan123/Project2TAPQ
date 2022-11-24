using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoleStaffDAO
    {
        public static List<RoleStaff> getAll()
        {

            List<RoleStaff> list = new List<RoleStaff>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.RoleStaffs.Where(a => a.Status != 0).ToList();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        item.IdRoleNavigation = RoleDAO.FindRoleById(item.IdRole);
                    }
                }
            }

            return list;
        }

        public static List<RoleStaff> getStaff(string idfarm)
        {

            List<RoleStaff> list = new List<RoleStaff>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.RoleStaffs.Where(a => a.Status != 0 && a.IdAcc.Equals(idfarm)).ToList();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        item.IdRoleNavigation = RoleDAO.FindRoleById(item.IdRole);
                    }
                }
            }

            return list;
        }



        public static RoleStaff FindRoleStaffById(string id)
        {
            RoleStaff a = new RoleStaff();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.RoleStaffs.SingleOrDefault(x => x.IdRoleStaff.Equals(id));
                    if (a != null)
                    {
                        a.IdRoleNavigation = RoleDAO.FindRoleById(a.IdRole);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }

        public static string Getid(string con)
        {
            List<RoleStaff> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.RoleStaffs.Select((RoleStaff i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "RS00000001";
                    }
                    string iDCuoi = accounts.Last().IdRoleStaff;
                    return $"RS{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static string GetIDCuoi()
        {
            List<RoleStaff> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.RoleStaffs.Select((RoleStaff i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "RS00000001";
                    }
                    string iDCuoi = accounts.Last().IdRoleStaff;
                    return $"RS{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void AddRoleStaff(RoleStaff a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdRoleStaff = GetIDCuoi();
                    context.RoleStaffs.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateRoleStaff(RoleStaff a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<RoleStaff>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteRoleStaff(RoleStaff a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.RoleStaffs.SingleOrDefault(
                        x => x.IdRoleStaff == a.IdRoleStaff);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<RoleStaff>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

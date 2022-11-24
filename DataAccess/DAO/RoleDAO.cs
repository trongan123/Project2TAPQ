using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    internal class RoleDAO
    {
        public static List<Role> getAll()
        {

            List<Role> list = new List<Role>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Roles.Where(a => a.Status != 0).ToList();
            }

            return list;
        }



        public static Role FindRoleById(string id)
        {
            Role a = new Role();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Roles.SingleOrDefault(x => x.IdRole.Equals(id));

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
            List<Role> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.Roles.Select((Role i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "RL00000001";
                    }
                    string iDCuoi = accounts.Last().IdRole;
                    return $"RL{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void AddRole(Role a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdRole = GetIDCuoi();
                    context.Roles.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateRole(Role a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<Role>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteRole(Role a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Roles.SingleOrDefault(
                        x => x.IdRole == a.IdRole);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<Role>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

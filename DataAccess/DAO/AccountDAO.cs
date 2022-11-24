using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        public static List<Account> getAll()
        {

            List<Account> list = new List<Account>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Accounts.Where(a => a.Status != 0).ToList();
                foreach (var item in list)
                {
                    if (item.IdRoleStaff != null)
                    {
                        item.IdRoleStaffNavigation = RoleStaffDAO.FindRoleStaffById(item.IdRoleStaff);
                    }
                    item.IdWardNavigation = WardDAO.FindWardById(item.IdWard);
                }
            }

            return list;
        }


        public static List<Account> getAllAccountByStatus(int st)
        {

            List<Account> list = new List<Account>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Accounts.Where(a => a.Status == st).ToList();
                foreach (var item in list)
                {
                    if (item.IdRoleStaff != null)
                    {
                        item.IdRoleStaffNavigation = RoleStaffDAO.FindRoleStaffById(item.IdRoleStaff);
                    }
                    item.IdWardNavigation = WardDAO.FindWardById(item.IdWard);
                }
            }

            return list;
        }

        public static List<Account> getAllAccountStaffFarm(string IdFarm)
        {
            var staff = RoleStaffDAO.getStaff(IdFarm);



            List<Account> list = new List<Account>();
            List<Account> data = new List<Account>();


            using (var context = new _2TAPQDBContext())
            {
                list = context.Accounts.Where(a => a.Status != 0).ToList();
                if (staff != null)
                {
                  
                    foreach (var item in list)
                    {
                        item.IdWardNavigation = WardDAO.FindWardById(item.IdWard);
                        if (item.IdRoleStaff != null)
                        {
                            item.IdRoleStaffNavigation = RoleStaffDAO.FindRoleStaffById(item.IdRoleStaff);
                            if (item.IdRoleStaffNavigation.IdAcc.Equals(IdFarm))
                            {
                               data.Add(item);
                            }
                        }

                    }
                  
                }
            }

            return data;
        }

        public static List<Account> getAllAccountByRole(int ro)
        {

            List<Account> list = new List<Account>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Accounts.Where(a => a.Role == ro && a.Status != 0).ToList();
                foreach (var item in list)
                {
                    if (item.IdRoleStaff != null)
                    {
                        item.IdRoleStaffNavigation = RoleStaffDAO.FindRoleStaffById(item.IdRoleStaff);
                    }
                    item.IdWardNavigation = WardDAO.FindWardById(item.IdWard);
                }
            }

            return list;
        }

        public static void UnBlockAccount(Account a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Accounts.SingleOrDefault(
                       x => x.IdAcc == a.IdAcc);
                    if (p1 != null)
                    {
                        p1.Status = 1;
                        context.Entry<Account>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static Account FindAccountById(string id)
        {
            Account a = new Account();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Accounts.SingleOrDefault(x => x.IdAcc.Equals(id));
                    if (a != null)
                    {
                        if (a.IdRoleStaff != null)
                        {
                            a.IdRoleStaffNavigation = RoleStaffDAO.FindRoleStaffById(a.IdRoleStaff);
                        }
                        a.IdWardNavigation = WardDAO.FindWardById(a.IdWard);
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
            List<Account> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.Accounts.Select((Account i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "A000000001";
                    }
                    string iDCuoi = accounts.Last().IdAcc;
                    return $"A{int.Parse(iDCuoi.Substring(1)) + 1:00000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static string MD5Password(string pass)
        {
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


        public static Account Login(string user, string password)
        {
            Account a = new Account();


            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Accounts.SingleOrDefault(x => (x.Username.Equals(user) && x.Password.Equals(MD5Password(password))) || (x.Email.Equals(user) && x.Password.Equals(MD5Password(password))));
                    if (a != null)
                    {
                        if (a.IdRoleStaff != null)
                        {
                            a.IdRoleStaffNavigation = RoleStaffDAO.FindRoleStaffById(a.IdRoleStaff);
                        }
                        a.IdWardNavigation = WardDAO.FindWardById(a.IdWard);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }
        public static List<Account> SearchAccountByPhone(String phone)
        {
            List<Account> list = new List<Account>();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    list = context.Accounts.Where(x => x.Phone.Contains(phone)).ToList();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }
        public static void AddAccount(Account a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.Password = MD5Password(a.Password);
                    a.IdAcc = GetIDCuoi();
                    context.Accounts.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public static void UpdateAccount(Account a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<Account>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteAccount(Account a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Accounts.SingleOrDefault(
                        x => x.IdAcc == a.IdAcc);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<Account>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

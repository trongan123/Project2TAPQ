using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ReceiptsPaymentDAO
    {
        public static List<ReceiptsPayment> getAll()
        {

            List<ReceiptsPayment> list = new List<ReceiptsPayment>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.ReceiptsPayments.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);
                }
            }

            return list;
        }


        public static List<ReceiptsPayment> getallbyStatus(string id, int status)
        {

            List<ReceiptsPayment> list = new List<ReceiptsPayment>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.ReceiptsPayments.Where(a => a.Status == status && a.IdUser.Equals(id)).ToList();
                foreach (var a in list)
                {
                    a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);
                }
            }

            return list;
        }
        public static ReceiptsPayment FindReceiptsPaymentByIdAcc(string idacc)
        {
            ReceiptsPayment a = new ReceiptsPayment();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.ReceiptsPayments.SingleOrDefault(x => x.IdUser.Equals(idacc));
                    if (a != null)
                    {
                        a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }

        public static ReceiptsPayment FindReceiptsPaymentById(string id)
        {
            ReceiptsPayment a = new ReceiptsPayment();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.ReceiptsPayments.SingleOrDefault(x => x.IdInvoice.Equals(id));
                    if (a != null)
                    {
                        a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);
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
            List<ReceiptsPayment> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.ReceiptsPayments.Select((ReceiptsPayment i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "RP00000001";
                    }
                    string iDCuoi = accounts.Last().IdInvoice;
                    return $"RP{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public static void AddReceiptsPayment(ReceiptsPayment a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdInvoice = GetIDCuoi();
                    context.ReceiptsPayments.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateReceiptsPayment(ReceiptsPayment a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<ReceiptsPayment>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteReceiptsPayment(ReceiptsPayment a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.ReceiptsPayments.SingleOrDefault(
                        x => x.IdInvoice == a.IdInvoice);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<ReceiptsPayment>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

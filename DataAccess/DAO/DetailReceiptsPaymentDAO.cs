using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DetailReceiptsPaymentDAO
    {
        public static List<DetailReceiptsPayment> getAll(string id, int status)
        {

            List<DetailReceiptsPayment> list = new List<DetailReceiptsPayment>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.DetailReceiptsPayments.Where(a => a.IdInvoice.Equals(id) && a.Status == status).ToList();
                foreach (var a in list)
                {
                    a.IdInvoiceNavigation = ReceiptsPaymentDAO.FindReceiptsPaymentById(a.IdInvoice);
                }
            }

            return list;
        }



        public static DetailReceiptsPayment FindDetailReceiptsPaymentById(string id)
        {
            DetailReceiptsPayment a = new DetailReceiptsPayment();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.DetailReceiptsPayments.SingleOrDefault(x => x.IdDetailReceiptsPayments.Equals(id));
                    if (a != null)
                    {
                        a.IdInvoiceNavigation = ReceiptsPaymentDAO.FindReceiptsPaymentById(a.IdInvoice);
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
            List<DetailReceiptsPayment> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.DetailReceiptsPayments.Select((DetailReceiptsPayment i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "DRP0000001";
                    }
                    string iDCuoi = accounts.Last().IdDetailReceiptsPayments;
                    return $"DRP{int.Parse(iDCuoi.Substring(3)) + 1:000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddDetailReceiptsPayment(DetailReceiptsPayment a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdDetailReceiptsPayments = GetIDCuoi();
                    context.DetailReceiptsPayments.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void UpdateDetailReceiptsPayment(DetailReceiptsPayment a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<DetailReceiptsPayment>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteDetailReceiptsPayment(DetailReceiptsPayment a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.DetailReceiptsPayments.SingleOrDefault(
                        x => x.IdDetailReceiptsPayments == a.IdDetailReceiptsPayments);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<DetailReceiptsPayment>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

using BusinessObjects.Models;
using DataAccess.DAO;
using DataAccess.Repositories.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Service
{
    public class DetailReceiptsPaymentService : IDetailReceiptsPaymentService
    {

        public void AddDetailReceiptsPayment(DetailReceiptsPayment a) => DetailReceiptsPaymentDAO.AddDetailReceiptsPayment(a);

        public void DeleteDetailReceiptsPayment(DetailReceiptsPayment a) => DetailReceiptsPaymentDAO.DeleteDetailReceiptsPayment(a);

        public DetailReceiptsPayment FindDetailReceiptsPaymentById(string id) => DetailReceiptsPaymentDAO.FindDetailReceiptsPaymentById(id);

        public List<DetailReceiptsPayment> getAll(string id, int status) => DetailReceiptsPaymentDAO.getAll(id,status);

        public void UpdateDetailReceiptsPayment(DetailReceiptsPayment a) => DetailReceiptsPaymentDAO.UpdateDetailReceiptsPayment(a);
    }
}

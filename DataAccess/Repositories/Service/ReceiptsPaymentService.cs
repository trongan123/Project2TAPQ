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
    public class ReceiptsPaymentService : IReceiptsPaymentService
    {
        public void AddReceiptsPayment(ReceiptsPayment a) => ReceiptsPaymentDAO.AddReceiptsPayment(a);

        public void DeleteReceiptsPayment(ReceiptsPayment a) => ReceiptsPaymentDAO.DeleteReceiptsPayment(a);

        public ReceiptsPayment FindReceiptsPaymentById(string id) => ReceiptsPaymentDAO.FindReceiptsPaymentById(id);
        public ReceiptsPayment FindReceiptsPaymentByIdAcc(string idacc) => ReceiptsPaymentDAO.FindReceiptsPaymentByIdAcc(idacc);
        public List<ReceiptsPayment> getAll() => ReceiptsPaymentDAO.getAll();
        public List<ReceiptsPayment> getallbyStatus(string id, int status) => ReceiptsPaymentDAO.getallbyStatus(id,status);

        public void UpdateReceiptsPayment(ReceiptsPayment a) => ReceiptsPaymentDAO.UpdateReceiptsPayment(a);
    }
}

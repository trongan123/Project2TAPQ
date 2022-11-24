using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IReceiptsPaymentService
    {
        List<ReceiptsPayment> getAll();

        List<ReceiptsPayment> getallbyStatus(string id, int status);

        ReceiptsPayment FindReceiptsPaymentByIdAcc(string idacc);

        ReceiptsPayment FindReceiptsPaymentById(string id);
        void AddReceiptsPayment(ReceiptsPayment a);

        void UpdateReceiptsPayment(ReceiptsPayment a);

        void DeleteReceiptsPayment(ReceiptsPayment a);

    }
}

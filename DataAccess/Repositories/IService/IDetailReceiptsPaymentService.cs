using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IDetailReceiptsPaymentService
    {
        List<DetailReceiptsPayment> getAll(string id, int status);
        DetailReceiptsPayment FindDetailReceiptsPaymentById(string id);
        void AddDetailReceiptsPayment(DetailReceiptsPayment a);

        void UpdateDetailReceiptsPayment(DetailReceiptsPayment a);

        void DeleteDetailReceiptsPayment(DetailReceiptsPayment a);

    }
}

using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface INotifyService
    {
        List<Notify> getAll(string idacc);
        List<Notify> getAllList();

        List<Notify> getAllByType(string Type);

        List<Notify> getAllForCoo(string idRoom);

        Notify FindNotifyById(string id);
        void AddNotify(Notify a);
        void AddNotifyCoop(string idcoop, Notify a);
        void UpdateNotify(Notify a);
        void Readed(List<Notify> a);

        void DeleteNotify(Notify a);

    }
}

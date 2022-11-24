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
    public class NotifyService : INotifyService
    {
        public void AddNotify(Notify a) => NotifyDAO.AddNotify(a);
        public void AddNotifyCoop(string idcoop, Notify a) => NotifyDAO.AddNotifyCoop(idcoop,a);

        public void DeleteNotify(Notify a) => NotifyDAO.DeleteNotify(a);

        public Notify FindNotifyById(string id) => NotifyDAO.FindNotifyById(id);

        public List<Notify> getAll(string idacc) => NotifyDAO.getAll(idacc);
        public List<Notify> getAllByType(string Type) => NotifyDAO.getAllByType(Type);
        public List<Notify> getAllList() => NotifyDAO.getAllList();
        public void UpdateNotify(Notify a) => NotifyDAO.UpdateNotify(a);
        public void Readed(List<Notify> a) => NotifyDAO.Readed(a);

        public List<Notify> getAllForCoo(string idRoom) => NotifyDAO.getAllForCoo(idRoom);
    }
}

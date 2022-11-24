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
    public class WardService : IWardService
    {
        public void AddWard(Ward a) => WardDAO.AddWard(a);

        public void DeleteWard(Ward a) => WardDAO.DeleteWard(a);

        public Ward FindWardById(string id) => WardDAO.FindWardById(id);

        public List<Ward> getAll() => WardDAO.getAll();

        public void UpdateWard(Ward a) => WardDAO.UpdateWard(a);
    }
}

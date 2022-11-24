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
    public class PondService : IPondService
    {
        public void AddPond(Pond a) => PondDAO.AddPond(a);

        public void DeletePond(Pond a) => PondDAO.DeletePond(a);

        public Pond FindPondById(string id) => PondDAO.FindPondById(id);

        public List<Pond> getAll(string idacc) => PondDAO.getAll(idacc);
        public List<Pond> getAllList() => PondDAO.getAllList();
        public void UpdatePond(Pond a) => PondDAO.UpdatePond(a);

        public List<Pond> getAllForCoo(string idRoom) => PondDAO.getAllForCoo(idRoom);
    }
}

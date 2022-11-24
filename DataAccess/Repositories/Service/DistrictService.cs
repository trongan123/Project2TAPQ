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
    public class DistrictService : IDistrictService
    {
        public void AddDistrict(District a) => DistrictDAO.AddDistrict(a);

        public void DeleteDistrict(District a) => DistrictDAO.DeleteDistrict(a);

        public District FindDistrictById(string id) => DistrictDAO.FindDistrictById(id);

        public List<District> getAll() => DistrictDAO.getAll();

        public void UpdateDistrict(District a) => DistrictDAO.UpdateDistrict(a);
    }
}

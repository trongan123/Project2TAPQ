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
    public class ProvinceService : IProvinceService
    {
        public void AddProvince(Province a) => ProvinceDAO.AddProvince(a);

        public void DeleteProvince(Province a) => ProvinceDAO.DeleteProvince(a);

        public Province FindProvinceById(string id)=> ProvinceDAO.FindProvinceById(id);

        public List<Province> getAll() => ProvinceDAO.getAll();

        public void UpdateProvince(Province a) => ProvinceDAO.UpdateProvince(a);
    }
}

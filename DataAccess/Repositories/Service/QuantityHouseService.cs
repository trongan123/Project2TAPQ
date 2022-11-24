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
    public class QuantityHouseService : IQuantityHouseService
    {
        public void AddQuantityHouse(QuantityHouse a) => QuantityHouseDAO.AddQuantityHouse(a);

        public void DeleteQuantityHouse(QuantityHouse a) => QuantityHouseDAO.DeleteQuantityHouse(a);

        public QuantityHouse FindQuantityHouseById(string id) => QuantityHouseDAO.FindQuantityHouseById(id);

        public List<QuantityHouse> getAll() => QuantityHouseDAO.getAll();

        public void UpdateQuantityHouse(QuantityHouse a) => QuantityHouseDAO.UpdateQuantityHouse(a);
    }
}

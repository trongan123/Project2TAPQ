using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IQuantityHouseService
    {
        List<QuantityHouse> getAll();
        QuantityHouse FindQuantityHouseById(string id);
        void AddQuantityHouse(QuantityHouse a);

        void UpdateQuantityHouse(QuantityHouse a);

        void DeleteQuantityHouse(QuantityHouse a);

    }
}

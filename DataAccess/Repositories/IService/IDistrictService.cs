using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IDistrictService
    {
        List<District> getAll();
        District FindDistrictById(string id);
        void AddDistrict(District a);

        void UpdateDistrict(District a);

        void DeleteDistrict(District a);

    }
}

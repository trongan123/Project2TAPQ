using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IProvinceService
    {
        List<Province> getAll();
        Province FindProvinceById(string id);
        void AddProvince(Province a);

        void UpdateProvince(Province a);

        void DeleteProvince(Province a);


    }
}

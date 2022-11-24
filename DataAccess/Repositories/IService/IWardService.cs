using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IWardService
    {
        List<Ward> getAll();
        Ward FindWardById(string id);
        void AddWard(Ward a);

        void UpdateWard(Ward a);

        void DeleteWard(Ward a);

    }
}

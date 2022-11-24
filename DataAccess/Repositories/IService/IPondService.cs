using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IPondService
    {
        List<Pond> getAll(string idacc);
 List<Pond> getAllList();

        List<Pond> getAllForCoo(string idRoom);

        Pond FindPondById(string id);
        void AddPond(Pond a);

        void UpdatePond(Pond a);

        void DeletePond(Pond a);

    }
}

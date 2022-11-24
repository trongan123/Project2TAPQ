using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IFishCategoryService
    {
        List<FishCategory> getAll();
        List<FishCategory> getAllbyStatus(int status);
        FishCategory FindFishCategoryById(string id);
        void AddFishCategory(FishCategory a);

        void UpdateFishCategory(FishCategory a);

        void DeleteFishCategory(FishCategory a);

        void ConfirmFishCategory(FishCategory a);

    }
}

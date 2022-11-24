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
    public class FishCategoryService : IFishCategoryService
    {
        public void AddFishCategory(FishCategory a) => FishCategoryDAO.AddFishCategory(a);

        public void DeleteFishCategory(FishCategory a) => FishCategoryDAO.DeleteFishCategory(a);
        public void ConfirmFishCategory(FishCategory a) => FishCategoryDAO.ConfirmFishCategory(a);

        public FishCategory FindFishCategoryById(string id) => FishCategoryDAO.FindFishCategoryById(id);

        public List<FishCategory> getAll() => FishCategoryDAO.getAll();
        public List<FishCategory> getAllbyStatus(int status) => FishCategoryDAO.getAllbyStatus(status);
        public void UpdateFishCategory(FishCategory a) => FishCategoryDAO.UpdateFishCategory(a);
    }
}

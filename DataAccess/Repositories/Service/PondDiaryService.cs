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
    public class PondDiaryService : IPondDiaryService
    {
        public void AddPondDiary(PondDiary a) => PondDiaryDAO.AddPondDiary(a);

        public void DeletePondDiary(PondDiary a) => PondDiaryDAO.DeletePondDiary(a);

        public PondDiary FindPondDiaryById(string id) => PondDiaryDAO.FindPondDiaryById(id);

        public List<PondDiary> getAll(string idPond) => PondDiaryDAO.getAll(idPond);

        public void UpdatePondDiary(PondDiary a) => PondDiaryDAO.UpdatePondDiary(a);
    }
}

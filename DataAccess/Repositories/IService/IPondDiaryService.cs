using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IPondDiaryService
    {
        List<PondDiary> getAll(string idPood);
        PondDiary FindPondDiaryById(string id);
        void AddPondDiary(PondDiary a);

        void UpdatePondDiary(PondDiary a);

        void DeletePondDiary(PondDiary a);

    }
}

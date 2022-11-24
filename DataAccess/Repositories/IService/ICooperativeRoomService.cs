using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface ICooperativeRoomService
    {
        List<CooperativeRoom> getAll();
        CooperativeRoom FindCooperativeRoomById(string id);

        CooperativeRoom GetCooperativeRoomByAccount(string idacc);
        CooperativeRoom FindCooperativeRoomByCode(string code);
        void AddCooperativeRoom(CooperativeRoom a);

        void UpdateCooperativeRoom(CooperativeRoom a);

        void DeleteCooperativeRoom(CooperativeRoom a);

    }
}

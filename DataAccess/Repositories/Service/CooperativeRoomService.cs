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
    public class CooperativeRoomService : ICooperativeRoomService
    {
        public void AddCooperativeRoom(CooperativeRoom a) => CooperativeRoomDAO.AddCooperativeRoom(a);

        public void DeleteCooperativeRoom(CooperativeRoom a) => CooperativeRoomDAO.DeleteCooperativeRoom(a);

        public CooperativeRoom FindCooperativeRoomById(string id) => CooperativeRoomDAO.FindCooperativeRoomById(id);

        public CooperativeRoom GetCooperativeRoomByAccount(string idacc) => CooperativeRoomDAO.GetCooperativeRoomByAccount(idacc);
        public CooperativeRoom FindCooperativeRoomByCode(string code) => CooperativeRoomDAO.FindCooperativeRoomByCode(code);
        public List<CooperativeRoom> getAll() => CooperativeRoomDAO.getAll();

        public void UpdateCooperativeRoom(CooperativeRoom a) => CooperativeRoomDAO.UpdateCooperativeRoom(a);
    }
}

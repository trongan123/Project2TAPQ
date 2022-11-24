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
    public class MemberService : IMemberService
    {
        public void AddMember(Member a) => MemberDAO.AddMember(a);

        public void DeleteMember(Member a) => MemberDAO.DeleteMember(a);

        public List<Member> getAllByStatus(int st) => MemberDAO.getAllByStatus(st);

        public Member FindMemberById(string id) => MemberDAO.FindMemberById(id);
        public Member FindMemberByIdacc(string idacc) => MemberDAO.FindMemberByIdAcc(idacc);

        public List<Member> getAllMember(string idRoom) => MemberDAO.getAllMember(idRoom);

        public List<Member> getAll() => MemberDAO.getAll();

        public void UpdateMember(Member a) => MemberDAO.UpdateMember(a);

        public void ConfirmMember(Member a) => MemberDAO.ConfirmMember(a);
    }
}

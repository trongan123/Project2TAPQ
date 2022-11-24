using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IMemberService
    {
        List<Member> getAll();
        Member FindMemberById(string id);
        Member FindMemberByIdacc(string idacc);
        List<Member> getAllByStatus(int st);
        void ConfirmMember(Member a);
        void AddMember(Member a);
        List<Member> getAllMember(string idRoom);

        void UpdateMember(Member a);

        void DeleteMember(Member a);

    }
}

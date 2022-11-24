using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        public static List<Member> getAll()
        {

            List<Member> list = new List<Member>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Members.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdRoomNavigation = CooperativeRoomDAO.FindCooperativeRoomById(a.IdRoom);
                    a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);

                }
            }

            return list;
        }
        public static List<Member> getAllByStatus(int st)
        {

            List<Member> list = new List<Member>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Members.Where(a => a.Status == st).ToList();
                foreach (var a in list)
                {
                    a.IdRoomNavigation = CooperativeRoomDAO.FindCooperativeRoomById(a.IdRoom);
                    a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);

                }
            }

            return list;
        }

        public static List<Member> getAllMember(string idRoom)
        {

            List<Member> list = new List<Member>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.Members.Where(a => a.Status != 0 && a.IdRoom.Equals(idRoom)).ToList();
                foreach (var a in list)
                {
                    a.IdRoomNavigation = CooperativeRoomDAO.FindCooperativeRoomById(a.IdRoom);
                    a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);

                }
            }

            return list;
        }

        


        public static Member FindMemberById(string id)
        {
            Member a = new Member();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Members.SingleOrDefault(x => x.IdMember.Equals(id));
                    if (a != null)
                    {
                        a.IdRoomNavigation = CooperativeRoomDAO.FindCooperativeRoomById(a.IdRoom);
                        a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }

        public static Member FindMemberByIdAcc(string idacc)
        {
            Member a = new Member();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.Members.SingleOrDefault(x => x.IdUser.Equals(idacc));
                    if (a != null)
                    {
                        a.IdRoomNavigation = CooperativeRoomDAO.FindCooperativeRoomById(a.IdRoom);
                        a.IdUserNavigation = AccountDAO.FindAccountById(a.IdUser);

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }

        public static string GetIDCuoi()
        {
            List<Member> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.Members.Select((Member i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "M000000001";
                    }
                    string iDCuoi = accounts.Last().IdMember;
                    return $"M{int.Parse(iDCuoi.Substring(1)) + 1:00000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddMember(Member a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {

                    a.IdMember = GetIDCuoi();
                    a.Status = 2;
                    context.Members.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateMember(Member a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<Member>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void ConfirmMember(Member a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.Status = 1;
                    context.Entry<Member>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteMember(Member a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.Members.SingleOrDefault(
                        x => x.IdMember == a.IdMember);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<Member>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}

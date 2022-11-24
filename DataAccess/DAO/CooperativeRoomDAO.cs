using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CooperativeRoomDAO
    {
        public static List<CooperativeRoom> getAll()
        {

            List<CooperativeRoom> list = new List<CooperativeRoom>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.CooperativeRooms.Where(a => a.Status != 0).ToList();
                foreach (var a in list)
                {
                    a.IdCooNavigation = AccountDAO.FindAccountById(a.IdCoo);
                    if(a.IdCooNavigation.Status == 0)
                    {
                        list.Remove(a);
                        
                    }
                    if (list.Count == 0)
                    {
                        break;
                    }
                }
                
            }

            return list;
        }



        public static CooperativeRoom FindCooperativeRoomById(string id)
        {
            CooperativeRoom a = new CooperativeRoom();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.CooperativeRooms.SingleOrDefault(x => x.IdRoom.Equals(id));
                    if (a != null)
                    {
                        a.IdCooNavigation = AccountDAO.FindAccountById(a.IdCoo);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }

        public static CooperativeRoom FindCooperativeRoomByCode(string code)
        {
            CooperativeRoom a = new CooperativeRoom();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.CooperativeRooms.SingleOrDefault(x => x.JoinCode.Equals(code) && x.Status == 1);
                    if (a != null)
                    {
                        a.IdCooNavigation = AccountDAO.FindAccountById(a.IdCoo);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return a;
        }

        public static CooperativeRoom GetCooperativeRoomByAccount(string idacc)
        {
            CooperativeRoom a = new CooperativeRoom();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.CooperativeRooms.SingleOrDefault(x => x.IdCoo.Equals(idacc) && x.Status != 0);
                    if (a != null)
                    {
                        a.IdCooNavigation = AccountDAO.FindAccountById(a.IdCoo);
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
            List<CooperativeRoom> CooperativeRooms;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    CooperativeRooms = context.CooperativeRooms.Select((CooperativeRoom i) => i).ToList();
                    if (CooperativeRooms.Count <= 0)
                    {
                        return "R000000001";
                    }
                    string iDCuoi = CooperativeRooms.Last().IdRoom;
                    return $"R{int.Parse(iDCuoi.Substring(1)) + 1:00000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static string getJoinCode()
        {
            string Joincode = "";
            Random r = new Random();
            var cooRoom = getAll();
            bool check = false;
            do
            {
                check = false;
                for (int i = 0; i < 9; i++)
                {
                    Joincode += r.Next(0, 9).ToString();
                }
                foreach (var room in cooRoom)
                {
                    if (room.JoinCode.Equals(Joincode))
                    {
                        check = true;
                    }
                }
            }
            while (check);
            return Joincode;
        }

        public static void AddCooperativeRoom(CooperativeRoom a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.JoinCode = getJoinCode();
                    a.IdRoom = GetIDCuoi();
                    context.CooperativeRooms.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdateCooperativeRoom(CooperativeRoom a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<CooperativeRoom>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeleteCooperativeRoom(CooperativeRoom a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.CooperativeRooms.SingleOrDefault(
                        x => x.IdRoom == a.IdRoom);
                    if (p1 != null)
                    {
                        p1.Status = 0;
                        context.Entry<CooperativeRoom>(p1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

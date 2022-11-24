using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PondDiaryDAO
    {
        public static List<PondDiary> getAll(string idPood)
        {

            List<PondDiary> list = new List<PondDiary>();
            using (var context = new _2TAPQDBContext())
            {
                list = context.PondDiaries.Where(a => a.IdPond.Equals(idPood)).ToList();
                foreach (var a in list)
                {
                    a.IdPondNavigation = PondDAO.FindPondById(a.IdPond);
                }
            }

            return list;
        }



        public static PondDiary FindPondDiaryById(string id)
        {
            PondDiary a = new PondDiary();
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a = context.PondDiaries.SingleOrDefault(x => x.IdDiary.Equals(id));
                    if (a != null)
                    {
                        a.IdPondNavigation = PondDAO.FindPondById(a.IdPond);
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
            List<PondDiary> accounts;

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    accounts = context.PondDiaries.Select((PondDiary i) => i).ToList();
                    if (accounts.Count <= 0)
                    {
                        return "PD00000001";
                    }
                    string iDCuoi = accounts.Last().IdDiary;
                    return $"PD{int.Parse(iDCuoi.Substring(2)) + 1:0000000#}";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static void AddPondDiary(PondDiary a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    a.IdDiary = GetIDCuoi();
                    context.PondDiaries.Add(a);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void UpdatePondDiary(PondDiary a)
        {

            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    context.Entry<PondDiary>(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static void DeletePondDiary(PondDiary a)
        {
            try
            {
                using (var context = new _2TAPQDBContext())
                {
                    var p1 = context.PondDiaries.SingleOrDefault(
                        x => x.IdDiary == a.IdDiary);
                    if (p1 != null)
                    {
                        context.Remove(p1);
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

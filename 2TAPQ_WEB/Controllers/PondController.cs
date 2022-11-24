using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers
{
    public class PondController : Controller
    {
        private readonly HttpClient client = null;
        private string PondAPiUrl = "";
        private string PondDiaryAPIUrl = "";
        private string FishCategoryAPiUrl = "";

        notification notify = new notification();
        VietNamChar vnc = new VietNamChar();
        AccountGet acc = new AccountGet();
        public PondController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PondAPiUrl = "https://localhost:7291/api/Pond";
            FishCategoryAPiUrl = "https://localhost:7291/api/FishCategory";
            PondDiaryAPIUrl = "https://localhost:7291/api/PondDiary";

        }
        public async Task<List<FishCategory>> GetFishCategorys()
        {
            HttpResponseMessage response = await client.GetAsync(FishCategoryAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<FishCategory> listFishCategorys = JsonSerializer.Deserialize<List<FishCategory>>(strDate, options);
            return listFishCategorys;
        }

        public async Task<List<Pond>> GetPonds(string idacc)
        {
            HttpResponseMessage response = await client.GetAsync(PondAPiUrl + "/idacc?idacc=" + idacc);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Pond> listPonds = JsonSerializer.Deserialize<List<Pond>>(strDate, options);

            return listPonds;
        }
        public async Task<List<Pond>> GetlistPond()
        {
            HttpResponseMessage response = await client.GetAsync(PondAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Pond> listPonds = JsonSerializer.Deserialize<List<Pond>>(strDate, options);

            return listPonds;
        }

        public async Task<Pond> GetlistPondbyid(string id)
        {
            HttpResponseMessage response = await client.GetAsync(PondAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Pond Ponds = JsonSerializer.Deserialize<Pond>(strDate, options);

            return Ponds;
        }

        public async Task<List<PondDiary>> GetlistPondDiary(string idPond)
        {
            HttpResponseMessage response = await client.GetAsync(PondDiaryAPIUrl + "/idPond?idPond=" + idPond);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<PondDiary> listPonds = JsonSerializer.Deserialize<List<PondDiary>>(strDate, options);

            return listPonds;
        }

        /// <summary>
        /// End get methor
        /// </summary>


        public async Task<IActionResult> EditPondDialy(string idDialy)
        {
            await getNotify();

            HttpResponseMessage response = await client.GetAsync(PondDiaryAPIUrl + "/id?id=" + idDialy);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            PondDiary data = JsonSerializer.Deserialize<PondDiary>(strDate, options);

            ViewBag.idPond = data.IdPond;
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPondDialy([Bind("IdDiary,IdPond,Sanility,Ph,Temperature,WaterLevel,FishStatus,Date")] PondDiary pond)
        {

            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(PondDiaryAPIUrl + "/id?id=" + pond.IdDiary, pond);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("Pond", "Pond");
            }

            ViewBag.idPond = pond.IdPond;
            return View("EditPondDialy");
        }
        public static bool CompareDateTimes(DateTime firstDate, DateTime secondDate)
        {
            return firstDate.Day == secondDate.Day && firstDate.Month == secondDate.Month && firstDate.Year == secondDate.Year;
        }

        public async Task<IActionResult> ReportDialy(string idPond)
        {
            await getNotify();

            var list = await GetlistPondDiary(idPond);
            DateTime now = DateTime.Now;
            PondDiary dialy = null;
            foreach (var a in list)
            {

                if (CompareDateTimes(Convert.ToDateTime(a.Date), now))
                {
                    dialy = a;
                    break;
                }
            }
            if (dialy != null)
            {
                return RedirectToAction("EditPondDialy", new { idDialy = dialy.IdDiary });
            }


            ViewBag.idPond = idPond;
            return View();
        }



        public async Task<IActionResult> HistoryDaily(string idPond, int pg = 1)
        {
            await getNotify();

            ViewBag.idPond = idPond;
            var list = await GetlistPondDiary(idPond);

            const int pageSize = 8;
            if (pg < 1)
                pg = 1;
            int recsCount = list.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();

            return View(data);
        }
        public async Task<IActionResult> Detail(string idPond)
        {
            await getNotify();

            ViewBag.idPond = idPond;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportDialy([Bind("IdDiary,IdPond,Sanility,Ph,Temperature,WaterLevel,FishStatus,Date")] PondDiary pond)
        {

            if (ModelState.IsValid)
            {
                DateTime join = DateTime.Now;

                pond.Date = join;
                HttpResponseMessage response1 = await client.PostAsJsonAsync(PondDiaryAPIUrl, pond);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("Pond", "Pond");
            }

            ViewBag.idPond = pond.IdPond;
            return View("AddPondDialy");
        }

        public async Task<int[]> getPond()
        {
            int[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            List<Pond> list = await GetlistPond();
            foreach (Pond p in list)
            {
                int start = Convert.ToDateTime(p.StartDay).Month;
                int end = Convert.ToDateTime(p.EndDay).Month;
                if (end < start)
                {
                    end = 12;
                }
                start--;
                for (int i = start; i < end; i++)
                {
                    result[i]++;
                }

            }
            result[12] = list.Count + 1;

            return result;
        }

        public async Task<IActionResult> AddPond()
        {
            await getNotify();

            var idacc = HttpContext.Session.GetString("IdFarm");
            ViewBag.Idacc = idacc;
            ViewBag.FishCategory = await GetFishCategorys();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPond([Bind("IdPond,IdAcc,IdFcategory,Name,PondArea,Image,Session,QuantityOfFingerlings,QuanlityOfEnd,StartDay,EndDay,Status")] Pond pond)
        {
            var idacc = HttpContext.Session.GetString("IdFarm");
            if (ModelState.IsValid)
            {
                List<Pond> listPondts = await GetPonds(pond.IdAcc);
                List<FishCategory> fishCategory = await GetFishCategorys();

                DateTime join = DateTime.Now;
                FishCategory f = fishCategory.FirstOrDefault(f => f.IdFcategory.Equals(pond.IdFcategory));
                if (listPondts.FirstOrDefault(a => a.Name.Equals(pond.Name)) == null)
                {
                    pond.EndDay = join.AddDays(f.HarvestTime);
                    pond.StartDay = join;

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(PondAPiUrl, pond);
                    response1.EnsureSuccessStatusCode();


                    Member a = await acc.GetMemberByID(idacc);
                    if (a != null)
                    {
                        notify.addnotifiFarm("Pond", pond.IdAcc, a.IdRoomNavigation.IdCoo);
                    }

                    notify.addnotifiFarm("Pond", pond.IdAcc);

                    return RedirectToAction("Pond", "Pond");
                }
            }
            ViewBag.Idacc = idacc;
            ViewBag.FishCategory = await GetFishCategorys();
            return View("AddPondAdmin");
        }


        public async Task<IActionResult> Pond(string? sea, int pg = 1)
        {
            await getNotify();


            var idusr = HttpContext.Session.GetString("IdFarm");
            List<Pond> listponds = await GetPonds(idusr);
            ViewBag.idacc = idusr;

            if (sea != null)
            {
                listponds = listponds.Where(a => vnc.LocDau(a.Name).ToLower().Contains(vnc.LocDau(sea).ToLower())).ToList();
                ViewBag.Search = sea;
            }

            const int pagesize = 6;
            if (pg < 1)
                pg = 1;
            int recscount = listponds.Count();
            var pager = new Pager(recscount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var data = listponds.Skip(recskip).Take(pager.PageSize).ToList();

            ViewBag.pager = pager;
            return View(data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchPond(string search)
        {
            return RedirectToAction("Pond", new { sea = search });
        }


        public async Task<IActionResult> Detailpond(string idPond)
        {
            await getNotify();
            Pond p = await GetlistPondbyid(idPond);
            return View(p);
        }

        public async Task<IActionResult> DeletePond(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     PondAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();
            return RedirectToAction("Pond", "Pond");
        }

        public async Task<string> getNotify()
        {
            var id = HttpContext.Session.GetString("IdFarm");
            var idusr = HttpContext.Session.GetString("IdUser");
            List<Notify> not = await notify.GetNotifyfarm(id);
            var newnot = not.Where(a => a.Status == 2).Count();
            not = not.OrderByDescending(s => s.IdNotify).ToList();
            ViewBag.notifiAll = not;
            ViewBag.notifi = not.Take(4).ToList();
            ViewBag.notifiNum = newnot;
            Account a = await acc.GetAccountByID(idusr);
            ViewBag.User = a;
            return "";
        }

        public async void readed()
        {
            var idusr = HttpContext.Session.GetString("IdFarm");
            List<Notify> not = await notify.GetNotifyfarm(idusr);
            var newnot = not.Where(a => a.Status == 2).ToList();
            if (newnot.Count > 0)
            {
                await notify.readed(newnot);
            }
        }



    }
}

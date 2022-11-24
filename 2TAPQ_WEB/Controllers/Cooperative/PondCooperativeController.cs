using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers.Cooperative
{
    public class PondCooperativeController : Controller
    {
        private readonly HttpClient client = null;
        private string PondAPiUrl = "";
        private string FishCategoryAPiUrl = "";

        notification notify = new notification();


        public PondCooperativeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PondAPiUrl = "https://localhost:7291/api/Pond";
            FishCategoryAPiUrl = "https://localhost:7291/api/FishCategory";

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
        public async Task<Pond> GetPond(string id)
        {
            HttpResponseMessage response = await client.GetAsync(PondAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Pond listPonds = JsonSerializer.Deserialize<Pond>(strDate, options);

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


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> EditPondCooperative(string id)
        {
            await getNotify();

            Pond p = await GetPond(id);

            ViewBag.Idacc = p.IdAcc;
            ViewBag.FishCategory = await GetFishCategorys();
            return View(p);
        }
        public async Task<IActionResult> AddPondCooperative(string idacc)
        {
            await getNotify();
            ViewBag.Idacc = idacc;
            ViewBag.FishCategory = await GetFishCategorys();
            return View();
        }

        public async Task<IActionResult> ChartPondCooperative()
        {
            await getNotify();
            return View();
        }

        public async Task<int[]> getPond()
        {
            await getNotify();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPond([Bind("IdPond,IdAcc,IdFcategory,Name,PondArea,Image,Session,QuantityOfFingerlings,QuanlityOfEnd,StartDay,EndDay,Status")] Pond pond)
        {
            await getNotify();
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(PondAPiUrl + "/id?id=" + pond.IdPond, pond);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("PondCooperative", new { idacc = pond.IdAcc });
            }
            ViewBag.Idacc = pond.IdAcc;
            ViewBag.FishCategory = await GetFishCategorys();
            return View("EditPondCooperative");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPond([Bind("IdPond,IdAcc,IdFcategory,Name,PondArea,Image,Session,QuantityOfFingerlings,QuanlityOfEnd,StartDay,EndDay,Status")] Pond pond)
        {
            await getNotify();
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

                    notify.addnotifiFarm("Pond", pond.IdAcc);

                    return RedirectToAction("PondCooperative", new { idacc = pond.IdAcc });
                }
            }
            ViewBag.Idacc = pond.IdAcc;
            ViewBag.FishCategory = await GetFishCategorys();
            return View("AddPondCooperative");
        }
        public async Task<IActionResult> PondCooperative(string idacc, string? sea, int pg = 1)
        {
            await getNotify();
            List<Pond> listPonds = await GetPonds(idacc);
            ViewBag.Idacc = idacc;

            if (sea != null)
            {
                listPonds = listPonds.Where(a => a.Name.ToLower().Contains(sea.ToLower())).ToList();
                ViewBag.Search = sea;
            }

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = listPonds.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listPonds.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchPond(string idacc, string search)
        {
           
            return RedirectToAction("PondCooperative", new { idacc = idacc, sea = search });
        }


        public async Task<IActionResult> DeletePondCooperative(string id, string idacc)
        {
            await getNotify();

            HttpResponseMessage response1 = await client.DeleteAsync(
                     PondAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();
            return RedirectToAction("PondCooperative", new { idacc = idacc });
        }

        public async Task<string> getNotify()
        {
            var idusr = HttpContext.Session.GetString("IdCoop");
            List<Notify> not = await notify.GetNotifyfarm(idusr);
            var newnot = not.Where(a => a.Status == 2).Count();
            not = not.OrderByDescending(s => s.IdNotify).ToList();
            ViewBag.notifiAll = not;
            ViewBag.notifi = not.Take(4).ToList();
            ViewBag.notifiNum = newnot;
            return "";
        }
    }
}


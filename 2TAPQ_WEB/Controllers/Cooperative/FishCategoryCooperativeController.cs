using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers.Cooperative
{
    public class FishCategoryCooperativeController : Controller
    {
        private readonly HttpClient client = null;
        private string FishCategoryAPiUrl = "";

        notification notify = new notification();
        VietNamChar vnc = new VietNamChar();
        public FishCategoryCooperativeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            FishCategoryAPiUrl = "https://localhost:7291/api/FishCategory";

        }


        public async Task<List<FishCategory>> GetFishCategorys(int status)
        {
            HttpResponseMessage response = await client.GetAsync(FishCategoryAPiUrl + "/status?status=" + status);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<FishCategory> listFishCategorys = JsonSerializer.Deserialize<List<FishCategory>>(strDate, options);
            return listFishCategorys;
        }
        public async Task<List<FishCategory>> GetFishCategoryAll()
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



        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> AddFishCategoryCooperative()
        {
            await getNotify();
            return View();
        }

        public async Task<IActionResult> EditFishCategory(string id)
        {
            await getNotify();

            HttpResponseMessage response = await client.GetAsync(FishCategoryAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            FishCategory data = JsonSerializer.Deserialize<FishCategory>(strDate, options);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFishCategoryCooperative([Bind("IdFcategory,CategoryName,Image,HarvestTime,Description,Status")] FishCategory fishCategory)
        {
            await getNotify();

            if (ModelState.IsValid)
            {
                List<FishCategory> listfishCategoryts = await GetFishCategoryAll();
                string img = "/images/";
                if (listfishCategoryts.FirstOrDefault(a => a.CategoryName.Equals(fishCategory.CategoryName)) == null)
                {
                    fishCategory.Image = img + fishCategory.Image;
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(FishCategoryAPiUrl, fishCategory);
                    response1.EnsureSuccessStatusCode();


                    return RedirectToAction("FishCategoryCooperative");
                }
            }

            return View("AddFishCategoryCooperative");
        }



        public async Task<IActionResult> FishCategoryCooperative(string? sea, int pg = 1)
        {
            await getNotify();

            List<FishCategory> listFishCategorys = await GetFishCategorys(1);
            if (sea != null)
            {
                listFishCategorys = listFishCategorys.Where(a => vnc.LocDau(a.CategoryName).ToLower().Contains(vnc.LocDau(sea).ToLower())).ToList();
                ViewBag.Search = sea;
            }
            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = listFishCategorys.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listFishCategorys.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Searchfish(string search)
        {
            return RedirectToAction("FishCategoryCooperative", new { sea = search });
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

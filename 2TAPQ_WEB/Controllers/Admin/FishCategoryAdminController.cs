using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers.Admin
{
    public class FishCategoryAdminController : Controller
    {
        private readonly HttpClient client = null;
        private string FishCategoryAPiUrl = "";

        notification notify = new notification();
        VietNamChar vnc = new VietNamChar();
        public FishCategoryAdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            FishCategoryAPiUrl = "https://localhost:7291/api/FishCategory";


        }


        public async Task<List<FishCategory>> GetFishCategorys(int status)
        {
            HttpResponseMessage response = await client.GetAsync(FishCategoryAPiUrl+ "/status?status="+ status);
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

        public async Task<IActionResult> AddFishCategory()
        {
            return View();
        }

        public async Task<IActionResult> EditFishCategory(string id)
        {
            
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
        public async Task<IActionResult> AddFishCategory([Bind("IdFcategory,CategoryName,Image,HarvestTime,Description,Status")] FishCategory fishCategory)
        {

            if (ModelState.IsValid)
            {
                List<FishCategory> listfishCategoryts = await GetFishCategoryAll();
          
                if (listfishCategoryts.FirstOrDefault(a => a.CategoryName.Equals(fishCategory.CategoryName)) == null)
                {
                   
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(FishCategoryAPiUrl, fishCategory);
                    response1.EnsureSuccessStatusCode();


                    return RedirectToAction("FishCategoryAdmin");
                }
            }

            return View("AddFishCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFishCategory([Bind("IdFcategory,CategoryName,Image,HarvestTime,Description,Status")] FishCategory fishCategory)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(FishCategoryAPiUrl + "/id?id=" + fishCategory.IdFcategory, fishCategory);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("FishCategoryAdmin");
            }
        
            ViewBag.regis = 1;
            return View("EditFishCategory");
        }

        public async Task<IActionResult> FishCategoryAdmin(string? sea, int pg = 1)
        {
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

        public async Task<IActionResult> ConfirmFishAdmin(string? sea, int pg = 1)
        {
            List<FishCategory> listFishCategorys = await GetFishCategorys(2);

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
        public IActionResult SearchConfirmFish(string search)
        {
            return RedirectToAction("ConfirmFishAdmin", new { sea = search });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchFish(string search)
        {
            return RedirectToAction("FishCategoryAdmin", new { sea = search });
        }
        

        public async Task<IActionResult> ConfirmFishCategory(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     FishCategoryAPiUrl + "/idf?idf=" + id);
            response1.EnsureSuccessStatusCode();
            return RedirectToAction("ConfirmFishAdmin");
        }
        public async Task<IActionResult> Deletefish(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     FishCategoryAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();
            return RedirectToAction("FishCategoryAdmin");
        }
    }
}

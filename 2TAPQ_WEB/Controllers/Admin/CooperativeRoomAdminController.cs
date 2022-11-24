using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers.Admin
{
    public class CooperativeRoomAdminController : Controller
    {
        private readonly HttpClient client = null;
        private string CooperativeRoomAPiUrl = "";
        private string AccountAPiUrl = "";
        private string WardAPiUrl = "";
        private string DistrictAPiUrl = "";
        private string ProvinceAPiUrl = "";

        notification notify = new notification();

        public CooperativeRoomAdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CooperativeRoomAPiUrl = "https://localhost:7291/api/CooperativeRoom";
            AccountAPiUrl = "https://localhost:7291/api/Account";
            WardAPiUrl = "https://localhost:7291/api/Ward";
            DistrictAPiUrl = "https://localhost:7291/api/District";
            ProvinceAPiUrl = "https://localhost:7291/api/Province";


        }

        public async Task<List<Account>> GetAccounts()
        {
            HttpResponseMessage response = await client.GetAsync(AccountAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Account> listAccounts = JsonSerializer.Deserialize<List<Account>>(strDate, options);
            return listAccounts;
        }

        public async Task<List<Ward>> GetWards()
        {
            HttpResponseMessage response = await client.GetAsync(WardAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Ward> listWards = JsonSerializer.Deserialize<List<Ward>>(strDate, options);
            return listWards;
        }

        public async Task<List<District>> GetDistricts()
        {
            HttpResponseMessage response = await client.GetAsync(DistrictAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<District> listDistricts = JsonSerializer.Deserialize<List<District>>(strDate, options);
            return listDistricts;
        }

        public async Task<List<Province>> GetProvinces()
        {
            HttpResponseMessage response = await client.GetAsync(ProvinceAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Province> listProvinces = JsonSerializer.Deserialize<List<Province>>(strDate, options);
            return listProvinces;
        }

        public async Task<List<CooperativeRoom>> GetCooperativeRooms()
        {
            HttpResponseMessage response = await client.GetAsync(CooperativeRoomAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<CooperativeRoom> listCooperativeRooms = JsonSerializer.Deserialize<List<CooperativeRoom>>(strDate, options);
            return listCooperativeRooms;
        }
       


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CooperativeAdmin(string? sea, int pg = 1)
        {
            List<CooperativeRoom> listCooperativeRooms = await GetCooperativeRooms();

            if (sea != null)
            {
                listCooperativeRooms = listCooperativeRooms.Where(a => a.IdCooNavigation.Phone.Contains(sea)).ToList();
                ViewBag.Search = sea;
            }

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = listCooperativeRooms.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listCooperativeRooms.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchCoop(string search)
        {
            return RedirectToAction("CooperativeAdmin", new { sea = search });
        }

        public async Task<IActionResult> CreateCooperativeAdmin(string? err)
        {
            ViewBag.Ward = await GetWards();
            ViewBag.District = await GetDistricts();
            ViewBag.Province = await GetProvinces();

            if (err != null)
            {
                ViewBag.error = err;
            }
            return View();
        }

        public async Task<IActionResult> EditCooperativeAdmin(string id)
        {
            HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Account account = JsonSerializer.Deserialize<Account>(strDate, options);
            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCooperative([Bind("IdAcc,Username,Password,Fullname,Email,Phone,Birthday,Address,Role,IdRoleStaff,IdWard,DateJoin,Image,Status")] Account account)
        {

            if (ModelState.IsValid)
            {
                List<Account> listAccounts = await GetAccounts();
                DateTime? bd = account.Birthday;
                DateTime join = DateTime.Now;
                account.Birthday = bd;
                account.DateJoin = join;
                if (listAccounts.FirstOrDefault(a => a.Email.Equals(account.Email)) == null)
                {
                   

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(AccountAPiUrl, account);
                    response1.EnsureSuccessStatusCode();

                    response1 = await client.GetAsync(AccountAPiUrl + "/" + account.Email + "/" + account.Password);
                    string strDate = await response1.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    account = JsonSerializer.Deserialize<Account>(strDate, options);

                    CooperativeRoom cooperativeRoom = new CooperativeRoom
                    {
                        IdRoom = "R000000001",
                        IdCoo = account.IdAcc,
                        JoinCode = "000000000",
                        PondArea = 0,
                        Status = 1

                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync(CooperativeRoomAPiUrl, cooperativeRoom);
                    response.EnsureSuccessStatusCode();

                    return RedirectToAction("CooperativeAdmin");
                }
                ViewBag.emailExist = false;
            }
            ViewBag.Ward = await GetWards();
            ViewBag.District = await GetDistricts();
            ViewBag.Province = await GetProvinces();
            return View("CreateCooperativeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCooperative([Bind("IdAcc,Username,Password,Fullname,Email,Phone,Birthday,Address,Role,IdRoleStaff,IdWard,DateJoin,Image,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(AccountAPiUrl + "/id?id=" + account.IdAcc, account);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("CooperativeAdmin");
            }
     
            return View("EditCooperativeAdmin");
        }
    }
}

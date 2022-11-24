using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers.Admin
{
    public class AccountAdminController : Controller
    {
        private readonly HttpClient client = null;

        //URL API
        private string AccountAPiUrl = "";
        private string WardAPiUrl = "";
        private string DistrictAPiUrl = "";
        private string ProvinceAPiUrl = "";

        notification notify = new notification();
        AccountGet acc = new AccountGet();

        const int farm = 2;
        public AccountAdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
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

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> FarmerAdmin(string? sea, int pg = 1)
        {
            ViewBag.note = "10";
            List<Account> listAccounts = await acc.getAllAccountByRole(2);

            if (sea != null)
            {
                listAccounts = listAccounts.Where(a => a.Phone.Contains(sea)).ToList();
                ViewBag.Search = sea;
            }

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;

            int recsCount = listAccounts.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listAccounts.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }


        public async Task<IActionResult> HistoryDelete(string? sea, int pg = 1)
        {
            List<Account> listAccounts = await acc.getAllAccountByStatus(0);

            if (sea != null)
            {
                listAccounts = listAccounts.Where(a => a.Phone.Contains(sea)).ToList();
                ViewBag.Search = sea;
            }

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;
            int recsCount = listAccounts.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listAccounts.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchFarm(string search)
        {
            return RedirectToAction("FarmerAdmin", new { sea = search });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchBlock(string search)
        {
            return RedirectToAction("HistoryDelete", new { sea = search });
        }
        
        public async Task<IActionResult> UnBlockAccount(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(AccountAPiUrl + "/idBlock?id=" + id);
            response1.EnsureSuccessStatusCode();
            return RedirectToAction("HistoryDelete");

        }

        public async Task<IActionResult> DeleteAccount(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     AccountAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();
            return RedirectToAction("FarmerAdmin");
        }

        public async Task<IActionResult> CreateFarmerAdmin(string? err)
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

        public async Task<IActionResult> EditAccountAdmin(string id)
        {
            ViewBag.Ward = await GetWards();
            ViewBag.District = await GetDistricts();
            ViewBag.Province = await GetProvinces();

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
        public async Task<IActionResult> CreateFarmer([Bind("IdAcc,Username,Password,Fullname,Email,Phone,Birthday,Address,Role,IdRoleStaff,IdWard,DateJoin,Image,Status")] Account account)
        {

            if (ModelState.IsValid)
            {
                List<Account> listAccounts = await acc.GetAccounts();
                DateTime? bd = account.Birthday;
                DateTime join = DateTime.Now;
                account.Birthday = bd;
                account.DateJoin = join;
                if (listAccounts.FirstOrDefault(a => a.Email.Equals(account.Email)) == null)
                {
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(AccountAPiUrl, account);
                    response1.EnsureSuccessStatusCode();

                    HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/" + account.Email + "/" + account.Password);
                    string strDate = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    account = JsonSerializer.Deserialize<Account>(strDate, options);

                    notify.addnotifiFarm("Account", account.IdAcc);

                    HttpContext.Session.SetString("IdUser", account.IdAcc);

                    return RedirectToAction("FarmerAdmin");
                }
                ViewBag.emailExist = false;
            }
            ViewBag.Ward = await GetWards();
            ViewBag.District = await GetDistricts();
            ViewBag.Province = await GetProvinces();
            return View("CreateFarmerAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFarmer([Bind("IdAcc,Username,Password,Fullname,Email,Phone,Birthday,Address,Role,IdRoleStaff,IdWard,DateJoin,Image,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(AccountAPiUrl + "/id?id=" + account.IdAcc, account);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("FarmerAdmin");
            }
            ViewBag.Ward = await GetWards();
            ViewBag.District = await GetDistricts();
            ViewBag.Province = await GetProvinces();
            ViewBag.regis = 1;
            return View("EditFarmerAdmin");
        }
    }
}

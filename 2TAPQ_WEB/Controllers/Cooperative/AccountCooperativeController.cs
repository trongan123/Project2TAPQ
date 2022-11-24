using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers.Cooperative
{
    public class AccountCooperativeController : Controller
    {
        private readonly HttpClient client = null;
        private string CooperativeRoomAPiUrl = "";
        private string PondAPiUrl = "";
        private string FishCategoryAPiUrl = "";
        private string AccountAPiUrl = "";
        private string RoleStaffAPiUrl = "";

        notification notify = new notification();
        AccountGet acc = new AccountGet();
        public AccountCooperativeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CooperativeRoomAPiUrl = "https://localhost:7291/api/CooperativeRoom";
            PondAPiUrl = "https://localhost:7291/api/Pond";
            FishCategoryAPiUrl = "https://localhost:7291/api/FishCategory";
            AccountAPiUrl = "https://localhost:7291/api/Account";
            RoleStaffAPiUrl = "https://localhost:7291/api/RoleStaff";
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

        public async Task<CooperativeRoom> GetCooperativeRoom(string id)
        {

            HttpResponseMessage response = await client.GetAsync(CooperativeRoomAPiUrl+"/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            CooperativeRoom listCooperativeRooms = JsonSerializer.Deserialize<CooperativeRoom>(strDate, options);
            return listCooperativeRooms;
        }

        public async Task<IActionResult> EditAccountCoop(string id)
        {
            await getNotify();

            CooperativeRoom co = await GetCooperativeRoom("R000000001");
            ViewBag.JoinCode = co.JoinCode;

            HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Account account = JsonSerializer.Deserialize<Account>(strDate, options);
            return View(account);
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddAccountCooperative()
        {
            await getNotify();
            return View();
        }

        public async Task<IActionResult> EditCooperativeRoom(string id)
        {
            await getNotify();

            HttpResponseMessage response = await client.GetAsync(CooperativeRoomAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            CooperativeRoom data = JsonSerializer.Deserialize<CooperativeRoom>(strDate, options);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccountCooperative([Bind("IdFcategory,CategoryName,Image,HarvestTime,Description,Status")] CooperativeRoom CooperativeRoom)
        {
            await getNotify();

            if (ModelState.IsValid)
            {
                List<CooperativeRoom> listCooperativeRoomts = await GetCooperativeRooms();


                HttpResponseMessage response1 = await client.PostAsJsonAsync(CooperativeRoomAPiUrl, CooperativeRoom);
                response1.EnsureSuccessStatusCode();


                return RedirectToAction("AccountCooperative");

            }

            return View("AddAccountCooperative");
        }
        public async Task<List<Pond>> GetPonds(string idacc)
        {
            HttpResponseMessage response = await client.GetAsync(PondAPiUrl + "/idRoom?idRoom=" + idacc);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Pond> listPonds = JsonSerializer.Deserialize<List<Pond>>(strDate, options);

            return listPonds;
        }
        public async Task<int[]> getPond()
        {
            HttpContext.Session.SetString("IdRoom", "R000000001");
            var idusr = HttpContext.Session.GetString("IdRoom");
            int[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            List<Pond> list = await GetPonds(idusr);
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



        public async Task<IActionResult> AccountCooperative(string id)
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdRoom");
            List<Pond> list = await GetPonds(idusr);


            CooperativeRoom co = await GetCooperativeRoom(idusr);
            ViewBag.JoinCode = co.JoinCode;

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
        public async Task<IActionResult> EditAccountCooperative([Bind("IdAcc,Username,Password,Fullname,Email,Phone,Birthday,Address,Role,IdRoleStaff,IdWard,DateJoin,Image,Status")] Account account)
        {
            await getNotify();


            if (ModelState.IsValid)
            {
                HttpResponseMessage response1 = await client.PutAsJsonAsync(AccountAPiUrl + "/id?id=" + account.IdAcc, account);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("AccountCooperative");
            }

            HttpResponseMessage response = await client.GetAsync(AccountAPiUrl + "/id?id=" + account.IdAcc);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Account account1 = JsonSerializer.Deserialize<Account>(strDate, options);

            account.IdWardNavigation = account1.IdWardNavigation;

            return View("AccountCooperative",account);
        }

        public async Task<IActionResult> StaffCooperative(string? sea, int pg = 1)
        {
            await getNotify();
            var idusr = HttpContext.Session.GetString("IdCoop");
            if (idusr == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Account> list = await acc.getAllAccountStaffFarm(idusr);

            if (sea != null)
            {
                list = list.Where(a => a.Phone.Contains(sea)).ToList();
                ViewBag.Search = sea;
            }

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;

            int recsCount = list.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = list.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchStaff(string search)
        {
            return RedirectToAction("StaffCooperative", new { sea = search });
        }
        public async Task<IActionResult> CreateStaff(string? err)
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdCoop");
            var farm = await acc.GetAccountByID(idusr);


            ViewBag.Farm = farm;
            if (err != null)
            {
                ViewBag.error = err;
            }
            return View();
        }
        public async Task<IActionResult> EditStaff(string id)
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdCoop");
            var farm = await acc.GetAccountByID(id);

            ViewBag.Farm = farm;

            return View(farm);
        }

        public RoleStaff GetStaff(string idRole, decimal salary)
        {
            var idusr = HttpContext.Session.GetString("IdCoop");
            var r = new RoleStaff
            {
                IdRoleStaff = "RS00000001",
                IdRole = idRole,
                IdAcc = idusr,
                Salary = salary,
                Status = 1
            };
            return r;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStaff([Bind("IdAcc,Username,Password,Fullname,Email,Phone,Birthday,Address,Role,IdRoleStaff,IdWard,DateJoin,Image,Status")] Account account, string salary, string idRole)
        {
            await getNotify();

            if (ModelState.IsValid)
            {
                List<Account> listAccounts = await acc.GetAccounts();
                DateTime? bd = account.Birthday;
                DateTime join = DateTime.Now;
                account.Birthday = bd;
                account.DateJoin = join;
                if (listAccounts.FirstOrDefault(a => a.Email.Equals(account.Email)) == null)
                {
                    decimal s = Convert.ToDecimal(salary);
                    var r = GetStaff(idRole, s);

                    string idstaff = await acc.getidStaff();
                    account.IdRoleStaff = idstaff;

                    HttpResponseMessage response1 = await client.PostAsJsonAsync(RoleStaffAPiUrl, r);
                    response1.EnsureSuccessStatusCode();

                    response1 = await client.PostAsJsonAsync(AccountAPiUrl, account);
                    response1.EnsureSuccessStatusCode();

                    return RedirectToAction("StaffCooperative", "AccountCooperative");
                }
            }

            return View("CreateStaff");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStaff([Bind("IdAcc,Username,Password,Fullname,Email,Phone,Birthday,Address,Role,IdRoleStaff,IdWard,DateJoin,Image,Status")] Account account, string salary, string idRole)
        {
            await getNotify();

            if (ModelState.IsValid)
            {


                decimal s = Convert.ToDecimal(salary);

                Account ac = await acc.GetAccountByID(account.IdAcc);

                RoleStaff role = ac.IdRoleStaffNavigation;
                role.Salary = s;
                role.IdRole = idRole;

                HttpResponseMessage response1 = await client.PutAsJsonAsync(RoleStaffAPiUrl + "/id?id=" + account.IdRoleStaff, role);
                response1.EnsureSuccessStatusCode();

                response1 = await client.PutAsJsonAsync(AccountAPiUrl + "/id?id=" + account.IdAcc, account);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("StaffCooperative", "AccountCooperative");

            }

            return View("EditStaff");

        }
        public async Task<IActionResult> DeleteAccount(string id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(
                     AccountAPiUrl + "/id?id=" + id);
            response1.EnsureSuccessStatusCode();
            return RedirectToAction("StaffCooperative", "AccountCooperative");
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

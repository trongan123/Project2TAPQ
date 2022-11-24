using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers.Cooperative
{
    public class CooperativeRoomCooperativeController : Controller
    {
        private readonly HttpClient client = null;
        private string CooperativeRoomAPiUrl = "";
        private string PondAPiUrl = "";
        private string FishCategoryAPiUrl = "";

        notification notify = new notification();

        public CooperativeRoomCooperativeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CooperativeRoomAPiUrl = "https://localhost:7291/api/CooperativeRoom";
            PondAPiUrl = "https://localhost:7291/api/Pond";
            FishCategoryAPiUrl = "https://localhost:7291/api/FishCategory";
        }

        public async Task<List<CooperativeRoom>> GetCooperativeRooms()
        {
            HttpContext.Session.SetString("IdRoom", "R000000001");
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
            HttpResponseMessage response = await client.GetAsync(CooperativeRoomAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            CooperativeRoom room = JsonSerializer.Deserialize<CooperativeRoom>(strDate, options);
            return room;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddCooperativeRoomCooperative()
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
        public async Task<IActionResult> AddCooperativeRoomCooperative([Bind("IdFcategory,CategoryName,Image,HarvestTime,Description,Status")] CooperativeRoom CooperativeRoom)
        {
            await getNotify();
            if (ModelState.IsValid)
            {
                List<CooperativeRoom> listCooperativeRoomts = await GetCooperativeRooms();

                HttpResponseMessage response1 = await client.PostAsJsonAsync(CooperativeRoomAPiUrl, CooperativeRoom);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("CooperativeRoomCooperative");

            }

            return View("AddCooperativeRoomCooperative");
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



        public async Task<IActionResult> CooperativeRoomCooperative()
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdRoom");
            var room = await GetCooperativeRoom("R000000001");

            ViewBag.IdAcc = room.IdCoo;

            return View();

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

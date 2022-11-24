using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers
{
    public class CooperativeRoomController : Controller
    {
        private readonly HttpClient client = null;
        private string CooperativeRoomAPiUrl = "";
        notification notify = new notification();
        AccountGet acc = new AccountGet();
        public CooperativeRoomController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CooperativeRoomAPiUrl = "https://localhost:7291/api/CooperativeRoom";


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


        public async Task<IActionResult> CooperativeAdmin()
        {
            List<CooperativeRoom> listCooperativeRooms = await GetCooperativeRooms();
            return View(listCooperativeRooms);
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
    }
}

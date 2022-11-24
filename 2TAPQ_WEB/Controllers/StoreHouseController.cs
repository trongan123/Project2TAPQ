using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers
{
    public class StoreHouseController : Controller
    {

        private readonly HttpClient client = null;
        private string StoreHouseAPiUrl = "";
        private string PondDiaryAPIUrl = "";
        private string FishCategoryAPiUrl = "";
        private string ItemStoreHouseAPIUrl = "";
        private string HistoryStoreHouseAPIUrl = "";
        private string ItemCategoryAPIUrl = "";

        notification notify = new notification();
        VietNamChar vnc = new VietNamChar();
        AccountGet acc = new AccountGet();
        public StoreHouseController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoreHouseAPiUrl = "https://localhost:7291/api/StoreHouse";
            FishCategoryAPiUrl = "https://localhost:7291/api/FishCategory";
            PondDiaryAPIUrl = "https://localhost:7291/api/PondDiary";
            ItemStoreHouseAPIUrl = "https://localhost:7291/api/ItemStoreHouse";
            HistoryStoreHouseAPIUrl = "https://localhost:7291/api/HistoryStoreHouse";
            ItemCategoryAPIUrl = "https://localhost:7291/api/ItemCategory";

        }
        public async Task<List<StoreHouse>> GetStoreHouses()
        {
            HttpResponseMessage response = await client.GetAsync(StoreHouseAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<StoreHouse> lists = JsonSerializer.Deserialize<List<StoreHouse>>(strDate, options);
            return lists;
        }
        public async Task<List<ItemCategory>> GetItemCategory()
        {
            HttpResponseMessage response = await client.GetAsync(ItemCategoryAPIUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ItemCategory> lists = JsonSerializer.Deserialize<List<ItemCategory>>(strDate, options);
            return lists;
        }
        public async Task<List<ItemStoreHouse>> GetItemStoreHouses(string id)
        {
            HttpResponseMessage response = await client.GetAsync(ItemStoreHouseAPIUrl + "/ids?ids=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ItemStoreHouse> lists = JsonSerializer.Deserialize<List<ItemStoreHouse>>(strDate, options);
            return lists;
        }
        public async Task<ItemStoreHouse> GetItemStoreHousebyid(string id)
        {
            HttpResponseMessage response = await client.GetAsync(ItemStoreHouseAPIUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            ItemStoreHouse data = JsonSerializer.Deserialize<ItemStoreHouse>(strDate, options);
            return data;
        }
        public async Task<List<HistoryStoreHouse>> GetHistoryStoreHouses(string id)
        {
            HttpResponseMessage response = await client.GetAsync(HistoryStoreHouseAPIUrl + "/ids?ids=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<HistoryStoreHouse> lists = JsonSerializer.Deserialize<List<HistoryStoreHouse>>(strDate, options);
            return lists;
        }

        public async Task<StoreHouse> GetStoreHouse(string id)
        {
            HttpResponseMessage response = await client.GetAsync(StoreHouseAPiUrl + "/idacc?idacc=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            StoreHouse data = JsonSerializer.Deserialize<StoreHouse>(strDate, options);
            return data;
        }

        /// <summary>
        /// End Get
        /// </summary>

        public async Task<IActionResult> StoreHouse(string? sea, int pg = 1)
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdFarm");

            StoreHouse store = await GetStoreHouse(idusr);


            List<ItemStoreHouse> list;

            if (store != null)
            {
                list = await GetItemStoreHouses(store.IdSHouse);
                if (sea != null)
                {
                    list = list.Where(a => vnc.LocDau(a.Name).ToLower().Contains(vnc.LocDau(sea).ToLower())).ToList();
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
            return View();
        }

        public async Task<IActionResult> AddItem()
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdFarm");
            StoreHouse store = await GetStoreHouse(idusr);

            ViewBag.Store = store;
            ViewBag.ItemCategory = await GetItemCategory();
            return View();
        }

        public async Task<IActionResult> EditItem(string id)
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdFarm");
            var item = await GetItemStoreHousebyid(id);

            ViewBag.ItemCategory = await GetItemCategory();
            return View(item);
        }
        public HistoryStoreHouse GetHistory(ItemStoreHouse itemStore, int status)
        {
            var idusr = HttpContext.Session.GetString("IdUser");
            string note = "";
            switch (status)
            {
                case 1:
                    note = "Đã thêm Sản phẩm trong kho";
                    break;
                case 2:
                    note = "Đã sữa Sản phẩm trong kho";
                    break;
                case 3:
                    note = "Đã xóa Sản phẩm trong kho";
                    break;
            }
            HistoryStoreHouse historyStoreHouse = new HistoryStoreHouse
            {
                IdHistoryStoreHouse = "HSH0000001",
                IdStaff = idusr,
                IdSHouse = itemStore.IdSHouse,
                Name = itemStore.Name,
                Quanlity = itemStore.Quanlity,
                IdItemCategory = itemStore.IdItemCategory,
                Note = note,
                Date = DateTime.Now,
                Status = status
            };
            return historyStoreHouse;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem([Bind("IdItemStoreHouse,IdSHouse,Name,Quanlity,IdItemCategory,Note")] ItemStoreHouse itemStore)
        {
            await getNotify();

            if (ModelState.IsValid)
            {
                List<ItemStoreHouse> list = await GetItemStoreHouses(itemStore.IdSHouse);
                ItemStoreHouse item = list.FirstOrDefault(a => vnc.LocDau(a.Name).ToLower().Contains(vnc.LocDau(itemStore.Name).ToLower()));
                if (item != null)
                {

                    item.Quanlity = item.Quanlity + itemStore.Quanlity;
                    HttpResponseMessage response1 = await client.PutAsJsonAsync(ItemStoreHouseAPIUrl + "/id?id=" + item.IdItemStoreHouse, item);
                    response1.EnsureSuccessStatusCode();

                    HistoryStoreHouse historyStoreHouse = GetHistory(itemStore, 1);

                    response1 = await client.PostAsJsonAsync(HistoryStoreHouseAPIUrl, historyStoreHouse);
                    response1.EnsureSuccessStatusCode();

                    return RedirectToAction("StoreHouse", "StoreHouse");
                }
                else
                {
                    HttpResponseMessage response1 = await client.PostAsJsonAsync(ItemStoreHouseAPIUrl, itemStore);
                    response1.EnsureSuccessStatusCode();

                    HistoryStoreHouse historyStoreHouse = GetHistory(itemStore, 1);

                    response1 = await client.PostAsJsonAsync(HistoryStoreHouseAPIUrl, historyStoreHouse);
                    response1.EnsureSuccessStatusCode();

                    return RedirectToAction("StoreHouse", "StoreHouse");
                }
            }
            var idusr = HttpContext.Session.GetString("IdFarm");
            StoreHouse store = await GetStoreHouse(idusr);

            ViewBag.Store = store;
            ViewBag.ItemCategory = await GetItemCategory();
            return View("AddItem", itemStore);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem([Bind("IdItemStoreHouse,IdSHouse,Name,Quanlity,IdItemCategory,Note")] ItemStoreHouse itemStore)
        {
            await getNotify();

            if (ModelState.IsValid)
            {

                HttpResponseMessage response1 = await client.PutAsJsonAsync(ItemStoreHouseAPIUrl + "/id?id=" + itemStore.IdItemStoreHouse, itemStore);
                response1.EnsureSuccessStatusCode();

                HistoryStoreHouse historyStoreHouse = GetHistory(itemStore, 2);

                response1 = await client.PostAsJsonAsync(HistoryStoreHouseAPIUrl, historyStoreHouse);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("StoreHouse", "StoreHouse");

            }
            var idusr = HttpContext.Session.GetString("IdFarm");
            StoreHouse store = await GetStoreHouse(idusr);

            ViewBag.ItemCategory = await GetItemCategory();
            return View("EditItem", itemStore);

        }


        public async Task<IActionResult> HistoryStoreHouse(string? sea, int pg = 1)
        {
            var idusr = HttpContext.Session.GetString("IdFarm");
            StoreHouse store = await GetStoreHouse(idusr);


            List<HistoryStoreHouse> list;

            if (store != null)
            {
                list = await GetHistoryStoreHouses(store.IdSHouse);
                if (sea != null)
                {
                    list = list.Where(a => vnc.LocDau(a.Name).ToLower().Contains(vnc.LocDau(sea).ToLower())).ToList();
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchItem(string search)
        {
            return RedirectToAction("StoreHouse", new { sea = search });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchHistory(string search)
        {
            return RedirectToAction("HistoryStoreHouse", new { sea = search });
        }

        public async Task<IActionResult> DeleteItem(string id)
        {
            ItemStoreHouse itemStore = await GetItemStoreHousebyid(id);
            if (itemStore != null)
            {
                HttpResponseMessage response1 = await client.DeleteAsync(
                         ItemStoreHouseAPIUrl + "/id?id=" + id);
                response1.EnsureSuccessStatusCode();

                HistoryStoreHouse historyStoreHouse = GetHistory(itemStore, 3);

                response1 = await client.PostAsJsonAsync(HistoryStoreHouseAPIUrl, historyStoreHouse);
                response1.EnsureSuccessStatusCode();
            }
            return RedirectToAction("StoreHouse", "StoreHouse");
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

using _2TAPQ_WEB.Models;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Controllers
{
    public class ReceiptsPaymentController : Controller
    {
        private readonly HttpClient client = null;
        private string ReceiptsPaymentAPiUrl = "";
        private string DetailReceiptsPaymentAPiUrl = "";
        private string PondDiaryAPIUrl = "";
        private string AccountAPiUrl = "";

        notification notify = new notification();
        AccountGet acc = new AccountGet();
        VietNamChar vnc = new VietNamChar();
        public ReceiptsPaymentController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AccountAPiUrl = "https://localhost:7291/api/Account";
            ReceiptsPaymentAPiUrl = "https://localhost:7291/api/ReceiptsPayment";
            DetailReceiptsPaymentAPiUrl = "https://localhost:7291/api/DetailReceiptsPayment";
        }
        public async Task<List<ReceiptsPayment>> GetReceiptsPayments()
        {
            HttpResponseMessage response = await client.GetAsync(ReceiptsPaymentAPiUrl);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ReceiptsPayment> lists = JsonSerializer.Deserialize<List<ReceiptsPayment>>(strDate, options);
            return lists;
        }

        public async Task<ReceiptsPayment> GetReceiptsPayment(string id)
        {
            HttpResponseMessage response = await client.GetAsync(ReceiptsPaymentAPiUrl + "/idacc?idacc=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            ReceiptsPayment data = JsonSerializer.Deserialize<ReceiptsPayment>(strDate, options);
            return data;
        }

        //status
        public async Task<List<DetailReceiptsPayment>> GetDetail(string id, int status)
        {
            HttpResponseMessage response = await client.GetAsync(DetailReceiptsPaymentAPiUrl + "/" + id + "/" + status);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<DetailReceiptsPayment> lists = JsonSerializer.Deserialize<List<DetailReceiptsPayment>>(strDate, options);
            return lists;
        }

        public async Task<DetailReceiptsPayment> GetDetailReceiptsPaymentbyId(string id)
        {
            HttpResponseMessage response = await client.GetAsync(DetailReceiptsPaymentAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            DetailReceiptsPayment data = JsonSerializer.Deserialize<DetailReceiptsPayment>(strDate, options);
            return data;
        }


        public async Task<IActionResult> Receipts(string? sea, int pg = 1)
        {
            await getNotify();


            var idusr = HttpContext.Session.GetString("IdFarm");

            ReceiptsPayment receipts = await GetReceiptsPayment(idusr);

            List<DetailReceiptsPayment> list;

            if (receipts != null)
            {
                list = await GetDetail(receipts.IdInvoice, 1);
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

        public async Task<IActionResult> Payment(string? sea, int pg = 1)
        {
            await getNotify();


            var idusr = HttpContext.Session.GetString("IdFarm");

            ReceiptsPayment receipts = await GetReceiptsPayment(idusr);

            List<DetailReceiptsPayment> list;

            if (receipts != null)
            {
                list = await GetDetail(receipts.IdInvoice, 2);
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
        public IActionResult SearchReceipts(string search)
        {
            return RedirectToAction("Receipts", new { sea = search });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchPayment(string search)
        {
            return RedirectToAction("Payment", new { sea = search });
        }

        public async Task<IActionResult> AddReceipts()
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdFarm");
            ReceiptsPayment receipts = await GetReceiptsPayment(idusr);
            var id = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(id);

            ViewBag.receipts = receipts;
            ViewBag.namestaff = staff.Fullname;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReceipts([Bind("IdDetailReceiptsPayments,IdInvoice,NameStaff,Quanlity,Price,Name,Description,Status,Date")] DetailReceiptsPayment Receipts)
        {
            await getNotify();

            if (ModelState.IsValid)
            {

                Receipts.Date = DateTime.Now;

                HttpResponseMessage response1 = await client.PostAsJsonAsync(DetailReceiptsPaymentAPiUrl, Receipts);
                response1.EnsureSuccessStatusCode();

                var p = await TotalPR();

                response1 = await client.PutAsJsonAsync(ReceiptsPaymentAPiUrl + "/id?id=" + Receipts.IdInvoice, p);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("Receipts", "ReceiptsPayment");

            }
            var idusr = HttpContext.Session.GetString("IdFarm");
            ReceiptsPayment receipts = await GetReceiptsPayment(idusr);
            var id = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(id);

            ViewBag.receipts = receipts;
            ViewBag.namestaff = staff.Fullname;
            return View("AddReceipts", Receipts);

        }
        public async Task<IActionResult> AddPayment()
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdFarm");
            ReceiptsPayment receipts = await GetReceiptsPayment(idusr);
            var id = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(id);

            ViewBag.receipts = receipts;
            ViewBag.namestaff = staff.Fullname;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPayment([Bind("IdDetailReceiptsPayments,IdInvoice,NameStaff,Quanlity,Price,Name,Description,Status,Date")] DetailReceiptsPayment Payment)
        {
            await getNotify();

            if (ModelState.IsValid)
            {

                Payment.Date = DateTime.Now;


                HttpResponseMessage response1 = await client.PostAsJsonAsync(DetailReceiptsPaymentAPiUrl, Payment);
                response1.EnsureSuccessStatusCode();

                var p = await TotalPR();

                response1 = await client.PutAsJsonAsync(ReceiptsPaymentAPiUrl + "/id?id=" + Payment.IdInvoice, p);
                response1.EnsureSuccessStatusCode();
                return RedirectToAction("Payment", "ReceiptsPayment");

            }
            var idusr = HttpContext.Session.GetString("IdFarm");
            ReceiptsPayment receipts = await GetReceiptsPayment(idusr);
            var id = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(id);

            ViewBag.receipts = receipts;
            ViewBag.namestaff = staff.Fullname;
            return View("AddPayment", Payment);

        }

        public async Task<IActionResult> EditPayment(string id)
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdFarm");

            var item = await GetDetailReceiptsPaymentbyId(id);
            var idstaff = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(idstaff);

            ViewBag.namestaff = staff.Fullname;


            return View(item);
        }
        public async Task<IActionResult> EditReceipts(string id)
        {
            await getNotify();

            var idusr = HttpContext.Session.GetString("IdFarm");

            var item = await GetDetailReceiptsPaymentbyId(id);
            var idstaff = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(idstaff);

            ViewBag.namestaff = staff.Fullname;


            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPayment([Bind("IdDetailReceiptsPayments,IdInvoice,NameStaff,Quanlity,Price,Name,Description,Status,Date")] DetailReceiptsPayment Payment)
        {
            await getNotify();

            if (ModelState.IsValid)
            {

                HttpResponseMessage response1 = await client.PutAsJsonAsync(DetailReceiptsPaymentAPiUrl + "/id?id=" + Payment.IdDetailReceiptsPayments, Payment);
                response1.EnsureSuccessStatusCode();

                var p = await TotalPR();

                response1 = await client.PutAsJsonAsync(ReceiptsPaymentAPiUrl + "/id?id=" + Payment.IdInvoice, p);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("Payment", "ReceiptsPayment");

            }

            var idstaff = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(idstaff);

            ViewBag.namestaff = staff.Fullname;

            return View("EditPayment", Payment);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReceipts([Bind("IdDetailReceiptsPayments,IdInvoice,NameStaff,Quanlity,Price,Name,Description,Status,Date")] DetailReceiptsPayment Payment)
        {
            await getNotify();

            if (ModelState.IsValid)
            {

                HttpResponseMessage response1 = await client.PutAsJsonAsync(DetailReceiptsPaymentAPiUrl + "/id?id=" + Payment.IdDetailReceiptsPayments, Payment);
                response1.EnsureSuccessStatusCode();

                var p = await TotalPR();

                response1 = await client.PutAsJsonAsync(ReceiptsPaymentAPiUrl + "/id?id=" + Payment.IdInvoice, p);
                response1.EnsureSuccessStatusCode();

                return RedirectToAction("Receipts", "ReceiptsPayment");

            }
        
            var idstaff = HttpContext.Session.GetString("IdUser");
            Account staff = await acc.GetAccountByID(idstaff);

            ViewBag.namestaff = staff.Fullname;

            return View("EditReceipts", Payment);

        }

        public async Task<IActionResult> DeleteReceipts(string id)
        {
            DetailReceiptsPayment item = await GetDetailReceiptsPaymentbyId(id);
            if (item != null)
            {
                HttpResponseMessage response1 = await client.DeleteAsync(
                         DetailReceiptsPaymentAPiUrl + "/id?id=" + id);
                response1.EnsureSuccessStatusCode();

                var p = await TotalPR();

                response1 = await client.PutAsJsonAsync(ReceiptsPaymentAPiUrl + "/id?id=" + p.IdInvoice, p);
                response1.EnsureSuccessStatusCode();

                if (item.Status == 1)
                {
                    return RedirectToAction("Receipts", "ReceiptsPayment");
                }
                else if (item.Status == 2)
                {
                    return RedirectToAction("Payment", "ReceiptsPayment");

                }
            }
            return RedirectToAction("Receipts", "ReceiptsPayment");
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
        public async Task<ReceiptsPayment> TotalPR()
        {

            var idusr = HttpContext.Session.GetString("IdFarm");

            ReceiptsPayment receipts = await GetReceiptsPayment(idusr);

            List<DetailReceiptsPayment> listR;
            List<DetailReceiptsPayment> listP;
            if (receipts != null)
            {
                double? total = 0;
                listR = await GetDetail(receipts.IdInvoice, 1);
                listP = await GetDetail(receipts.IdInvoice, 2);

                if (listR.Count > 0)
                {
                    foreach (var item in listR)
                    {
                        total += item.Quanlity * Convert.ToDouble(item.Price);
                    }
                }
                if (listP.Count > 0)
                {
                    foreach (var item in listP)
                    {
                        total -= item.Quanlity * Convert.ToDouble(item.Price);
                    }
                }
                receipts.Total = Convert.ToDecimal(total);
                return receipts;
            }
            return null;
        }
        
    }
}

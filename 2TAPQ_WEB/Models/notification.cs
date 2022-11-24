using BusinessObjects.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace _2TAPQ_WEB.Models
{
    public class notification
    {
        private readonly HttpClient client = null;

        //URL API
        private string NotifyAPiUrl = "";
        private string AccountAPiUrl = "";
        private string PondAPiUrl = "";
        public string IdNotify { get; set; }
        public string Message { get; set; }
        public string IdAcc { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

        Notify notify = new Notify();
        AccountGet acc = new AccountGet();

        public notification()
        {

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            NotifyAPiUrl = "https://localhost:7291/api/Notify";
            AccountAPiUrl = "https://localhost:7291/api/Account";
            PondAPiUrl = "https://localhost:7291/api/Pond";
        }
        public async Task<List<Notify>> GetNotifyCoop(string idRoom)
        {
            HttpResponseMessage response = await client.GetAsync(NotifyAPiUrl + "/idRoom?idRoom=" + idRoom);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Notify> not = JsonSerializer.Deserialize<List<Notify>>(strDate, options);
            return not;
        }
        public async Task<List<Notify>> GetNotifyfarm(string id)
        {
            HttpResponseMessage response = await client.GetAsync(NotifyAPiUrl + "/idacc?idacc=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Notify> not = JsonSerializer.Deserialize<List<Notify>>(strDate, options);
            return not;
        }

        public async Task<List<Notify>> getAllByType(string Type)
        {
            HttpResponseMessage response = await client.GetAsync(NotifyAPiUrl + "/Type?Type=" + Type);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Notify> not = JsonSerializer.Deserialize<List<Notify>>(strDate, options);
            return not;
        }

        public async Task<Pond> GetPond(string id)
        {
            HttpResponseMessage response = await client.GetAsync(PondAPiUrl + "/id?id=" + id);
            string strDate = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Pond pond = JsonSerializer.Deserialize<Pond>(strDate, options);
            return pond;
        }



        public async void addnotifiFarm(string Type, string Id, string IdRoom = null, string IdPond = null)
        {
            Account account = await acc.GetAccountByID(Id);
            Pond p = new Pond();
            if (IdPond != null)
            {
                p = await GetPond(IdPond);
            }

            switch (Type)
            {
                case "Pond":
                    this.Message = "Đã tạo ao mới !!!";
                    break;
                case "Account":
                    this.Message = "Đã tạo tạo tài khoản mới !!!";
                    break;
                case "Fish":
                    this.Message = "Cá tại " + p.Name + " có vấn đề !!!";
                    break;








            }

            this.Type = Type;
            this.Date = DateTime.Now;
            this.Status = 2;
            notify.IdNotify = "N000000001";
            notify.IdAcc = Id;
            notify.Status = Status;
            notify.Name = account.Fullname;
            notify.Description = Message;
            notify.Date = DateTime.Now;
            notify.Type = Type;

            if (IdRoom != null)
            {
                await addcoop(IdRoom, notify);
            }
            else
            {
                await add(notify);
            }
        }

        public async Task<string> add(Notify notify)
        {

            HttpResponseMessage response1 = await client.PostAsJsonAsync(NotifyAPiUrl, notify);
            response1.EnsureSuccessStatusCode();

            return "";
        }
        public async Task<string> addcoop(string idcoop, Notify notify)
        {

            HttpResponseMessage response1 = await client.PostAsJsonAsync(NotifyAPiUrl + "/idcoop?idcoop=" + idcoop, notify);
            response1.EnsureSuccessStatusCode();

            return "";
        }
        public async Task<string> readed(List<Notify> notifyList)
        { 
            HttpResponseMessage response1 = await client.PutAsJsonAsync(NotifyAPiUrl, notifyList);
            response1.EnsureSuccessStatusCode();
            return "";
        }
    }
}

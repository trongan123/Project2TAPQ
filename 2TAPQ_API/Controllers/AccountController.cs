using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService account)
        {
            this.accountService = account;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccount() => accountService.getAll();

        [HttpGet("st")]
        public ActionResult<IEnumerable<Account>> getAllAccountByStatus(int st) => accountService.getAllAccountByStatus(st);
       
        
        [HttpGet("IdFarm")]
        public ActionResult<IEnumerable<Account>> getAllAccountStaffFarm(string IdFarm) => accountService.getAllAccountStaffFarm(IdFarm);
        
        [HttpGet("ro")]
        public ActionResult<IEnumerable<Account>> getAllAccountByRole(int ro) => accountService.getAllAccountByRole(ro);

        [HttpGet("id")]
        public ActionResult<Account> GetAccountById(string id) => accountService.FindAccountById(id);

        [HttpDelete("idBlock")]
        public IActionResult UnBlockAccount(string id)
        {
            var aTmp = accountService.FindAccountById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            accountService.UnBlockAccount(aTmp);
            return NoContent();
        }

        [HttpGet("{email}/{pass}")]
        public ActionResult<Account> Login(string email, string pass) => accountService.Login(email, pass);

        [HttpGet("phone")]
        public ActionResult<IEnumerable<Account>> GetAccountByPhone(String phone) => accountService.SearchAccountByPhone(phone);

        [HttpPost]
        public IActionResult PortAccount(Account a)
        {
            accountService.AddAccount(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAccount(string id)
        {
            var a = accountService.FindAccountById(id);
            if (a == null)
            {
                return NotFound();
            }
            accountService.DeleteAccount(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateAccount(string id, Account a)
        {
            var aTmp = accountService.FindAccountById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            accountService.UpdateAccount(a);
            return NoContent();
        }
    }
}

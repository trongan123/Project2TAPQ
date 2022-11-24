using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleStaffController : ControllerBase
    {
        private readonly IRoleStaffService _service;

        public RoleStaffController(IRoleStaffService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoleStaff>> GetRoleStaffs() => _service.getAll();

        [HttpGet("id")]
        public ActionResult<RoleStaff> GetRoleStaffById(string id) => _service.FindRoleStaffById(id);
       
        [HttpGet("con")]
        public ActionResult<string> Getid(string con) => _service.Getid(con);


        [HttpPost]
        public IActionResult PortRoleStaff(RoleStaff a)
        {
            _service.AddRoleStaff(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteRoleStaff(string id)
        {
            var a = _service.FindRoleStaffById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeleteRoleStaff(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateRoleStaff(string id, RoleStaff a)
        {
            var aTmp = _service.FindRoleStaffById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdateRoleStaff(a);
            return NoContent();
        }
    }
}

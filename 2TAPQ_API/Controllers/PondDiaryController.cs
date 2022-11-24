using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2TAPQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PondDiaryController : ControllerBase
    {
        private readonly IPondDiaryService _service;

        public PondDiaryController(IPondDiaryService service)
        {
            this._service = service;
        }

        [HttpGet("idPond")]
        public ActionResult<IEnumerable<PondDiary>> GetPondDiarys(string idPond) => _service.getAll(idPond);

        [HttpGet("id")]
        public ActionResult<PondDiary> GetPondDiaryById(string id) => _service.FindPondDiaryById(id);



        [HttpPost]
        public IActionResult PortPondDiary(PondDiary a)
        {
            _service.AddPondDiary(a);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeletePondDiary(string id)
        {
            var a = _service.FindPondDiaryById(id);
            if (a == null)
            {
                return NotFound();
            }
            _service.DeletePondDiary(a);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdatePondDiary(string id, PondDiary a)
        {
            var aTmp = _service.FindPondDiaryById(id);
            if (aTmp == null)
            {
                return NotFound();
            }
            _service.UpdatePondDiary(a);
            return NoContent();
        }
    }
}

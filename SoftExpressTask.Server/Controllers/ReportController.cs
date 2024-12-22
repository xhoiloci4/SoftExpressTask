using Microsoft.AspNetCore.Mvc;
using SoftExpressTask.Server.Services.Interfaces;

namespace SoftExpressTask.Server.Controllers
{
    [ApiController]
    [Route("api/reports/[action]")]
    public class ReportController : Controller
    {

        public ActionRepo _actionRepo;


        public ReportController(ActionRepo repo)
        {
            _actionRepo = repo;
        }


        [HttpGet]
        public IActionResult GetAll(string? region, string? devicetype, string? application, string? timedate)
        {

            var (failure, actions) = _actionRepo.getFiltered(region, devicetype, application, timedate);
            if (failure != null)
            {

                return NotFound();

            }




            return Ok(new { actions });

        }
    }
}

using Microsoft.AspNetCore.Mvc;

using SoftExpressTask.Server.Database.Models;
using SoftExpressTask.Server.Services.Interfaces;
using SoftExpressTask.Server.Utils;



namespace SoftExpressTask.Server.Controllers
{
    [ApiController]
    [Route("api/actions/[action]")]
    public class ActionController : Controller
    {

        public ActionRepo _actionRepo;


        public ActionController(ActionRepo repo)
        {
            _actionRepo = repo;
        }


        public class ActionForm
        {
            public string Application { get; set; }
            public string DeviceType { get; set; }
            public string Region { get; set; }
            public DateTime Timedate { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Add(string token, [FromBody] ActionForm form)
        {


            var guid = AppHasher.decryptToken(token);

            var action = new Actionn
            {
                Id = new Guid(),
                Application = form.Application,
                DeviceType = form.DeviceType,
                Timedate = form.Timedate,
                Region = form.Region,
                UserId = guid,

            };


            var (failure, act) = await _actionRepo.add(action);

            if (failure != null)
            {

                return NotFound(new { error = "Something went wrong" });

            }

            return Ok(new { act });

        }



        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {

            var guid = new Guid(id);
            var (failure, user) = await _actionRepo.get(guid);

            if (failure != null)
            {

                return NotFound();

            }

            return Ok(user);

        }



        [HttpGet]
        public IActionResult GetAll(string token)
        {

            var (failure, actions) = _actionRepo.getAll();
            if (failure != null)
            {

                return NotFound();

            }

            return Ok(new { actions });

        }

    }
}

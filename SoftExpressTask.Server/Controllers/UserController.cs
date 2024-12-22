using Microsoft.AspNetCore.Mvc;
using SoftExpressTask.Server.Services.Interfaces;
using SoftExpressTask.Server.Utils;
using SoftExpressTask.Server.Database.Models;
namespace SoftExpressTask.Server.Controllers
{
    [ApiController]
    [Route("api/auth/[action]")]
    public class UserController : Controller
    {

        public UserRepo _userRepo;


        public UserController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {

            var guid = new Guid(id);
            var (failure, user) = await _userRepo.get(guid);

            if (failure != null)
            {

                return NotFound();

            }

            return Ok(user);

        }


        public class LoginModel
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {


            var (failure, user) = await _userRepo.getByUsername(model.username);

            if (failure != null)
            {
                return Unauthorized(failure.Message);
            }

            if (user == null)
            {
                return NotFound("User does not exist");
            }

            var verification = AppHasher.verifyPassword(model.password, user);

            if (!verification)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = AppHasher.GenerateJwtToken(user);

            return Ok(token);

        }


        public class SignupModel
        {
            public string name { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }

        }


        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] SignupModel model)
        {


            var (failure, user) = await _userRepo.getByUsername(model.username);



            if (user != null)
            {
                return NotFound("User with this username exists choose a different username");
            }

            var hashPassword = AppHasher.hashPassword(model.password);

            var payload = new User
            {

                Name = model.name,
                Email = model.email,
                Username = model.username,
                PasswordHash = hashPassword,
                Birthday = DateTime.UtcNow,
                Id = new Guid(),
                Role = "Admin",
                Actions = []
            };


            var saveResult = await _userRepo.add(payload);

            if (saveResult.Item1 != null)
            {

                return BadRequest(saveResult.Item1);

            }

            if (saveResult.Item2 == null)
            {

                return BadRequest(saveResult.Item2);

            }

            var token = AppHasher.GenerateJwtToken(saveResult.Item2!);

            return Ok(token);

        }
    }
}

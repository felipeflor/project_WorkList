using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkListAPI.Src.Models;
using WorkListAPI.Src.Repositories;

namespace WorkListAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUser _repository;
        #endregion

        #region Constructors
        public UserController(IUser repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        [HttpGet("email/{emailUser}")]
        public async Task<ActionResult> FindUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.FindUserByEmailAsync(emailUser);

            if (user == null) return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> NewUserAsync([FromBody] User user)
        {
            await _repository.NewUserAsync(user);

            return Created($"api/User/{user.Email}", user);
        }
        #endregion
    }
}

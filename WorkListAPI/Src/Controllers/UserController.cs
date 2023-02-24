using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkListAPI.Src.Models;
using WorkListAPI.Src.Repositories;
using WorkListAPI.Src.Services;

namespace WorkListAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUser _repository;
        private readonly IAuthentication _services;
        #endregion

        #region Constructors
        public UserController(IUser repository, IAuthentication services)
        {
            _repository = repository;
            _services = services;
        }

        #endregion

        #region Methods
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> FindUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.FindUserByEmailAsync(emailUser);

            if (user == null) return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> NewUserAsync([FromBody] User user)
        {
            try
            {
                await _services.CreateUserWithoutDuplicateAsync(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            var aux = await _repository.FindUserByEmailAsync(user.Email);
            if (aux == null) return Unauthorized(new
            {
                Message = "Invalid e-mail"
            });
            if (aux.Password != _services.EncryptedPassword(user.Password))
                return Unauthorized(new { Message = "Invalid password" });
            var token = "Bearer " + _services.GenerateToken(aux);
            return Ok(new { User = aux, Token = token });
        }

        #endregion
    }
}

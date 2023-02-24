using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkListAPI.Src.Models;
using WorkListAPI.Src.Repositories;

namespace WorkListAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Works")]
    [Produces("application/json")]
    public class WorkController : ControllerBase
    {
        #region Attributes
        private readonly IWork _repository;
        #endregion

        #region Constructors
        public WorkController(IWork repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> FindAllWorksAsync()
        {
            var list = await _repository.FindAllWorksAsync();

            if(list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("id/{idWork}")]
        [Authorize]
        public async Task<ActionResult> FindWorkByIdAsync([FromRoute] int idWork)
        {
            try
            {
                return Ok(await _repository.FindWorkByIdAsync(idWork));
            }
            catch (Exception ex)
            {
                return NotFound(new {Message = ex.Message});
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewWorkAsync([FromBody] Work work)
        {
            try
            {
                await _repository.NewWorkAsync(work);
                return Created($"api/Works", work);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateWorkAsync([FromBody] Work work)
        {
            try
            {
                await _repository.UpdateWorkAsync(work);
                return Ok(work);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("delete/{idWork}")]
        [Authorize]
        public async Task<ActionResult> DeleteWorkAsync([FromRoute] int idWork)
        {
            try
            {
                await _repository.DeleteWorkAsync(idWork);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
        #endregion
    }
}

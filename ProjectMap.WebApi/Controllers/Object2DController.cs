using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;

namespace ProjectMap.WebApi.Controllers
{
    [ApiController]
    [Route("api/object2d")]
    public class Object2DController : ControllerBase
    {
        private readonly IObject2DRepository _object2DRepository;
        private readonly ILogger<Object2DController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public Object2DController(IObject2DRepository object2DRepository, ILogger<Object2DController> logger, IAuthenticationService authenticationService)
        {
            _object2DRepository = object2DRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet("environment/{environmentId}", Name = "ReadObject2Ds")]
        public async Task<ActionResult<IEnumerable<Object2D>>> GetByEnvironmentId(Guid environmentId)
        {
            var object2Ds = await _object2DRepository.ReadByEnvironmentIdAsync(environmentId);
            return Ok(object2Ds);
        }

        [HttpGet("{object2DId}", Name = "ReadObject2D")]
        public async Task<ActionResult<Object2D>> Get(Guid object2DId)
        {
            var object2D = await _object2DRepository.ReadByIdAsync(object2DId);

            if (object2D == null)
                return NotFound();

            return Ok(object2D);
        }

        [HttpPost(Name = "CreateObject2D")]
        public async Task<ActionResult> Add(Object2D object2D)
        {
            try
            {
                if (object2D.Environment2DId == Guid.Empty)
                {
                    return BadRequest("Environment2DId is required.");
                }

                object2D.Id = Guid.NewGuid();

                var createdObject2D = await _object2DRepository.InsertAsync(object2D);
                return CreatedAtRoute("ReadObject2D", new { object2DId = createdObject2D.Id }, createdObject2D);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating object2D.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{object2DId}", Name = "UpdateObject2D")]
        public async Task<ActionResult> Update(Guid object2DId, Object2D newObject2D)
        {
            var existingObject2D = await _object2DRepository.ReadByIdAsync(object2DId);

            if (existingObject2D == null)
                return NotFound();

            newObject2D.Id = object2DId;
            await _object2DRepository.UpdateAsync(newObject2D);

            return Ok(newObject2D);
        }

        [HttpDelete("{object2DId}", Name = "DeleteObject2D")]
        public async Task<IActionResult> Delete(Guid object2DId)
        {
            var existingObject2D = await _object2DRepository.ReadByIdAsync(object2DId);

            if (existingObject2D == null)
                return NotFound();

            await _object2DRepository.DeleteAsync(object2DId);

            return Ok();
        }
    }
}



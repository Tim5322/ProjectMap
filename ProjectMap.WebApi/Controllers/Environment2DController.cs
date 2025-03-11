using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;

namespace ProjectMap.WebApi.Controllers
{
    [ApiController]
    [Route("Environment2Ds")]
    public class Environment2DController : ControllerBase
    {
        private readonly Environment2DRepository _environment2DRepository;
        private readonly ILogger<Environment2DController> _logger;

        public Environment2DController(Environment2DRepository environment2DRepository, ILogger<Environment2DController> logger)
        {
            _environment2DRepository = environment2DRepository;
            _logger = logger;
        }

        [HttpGet(Name = "ReadEnvironment2Ds")]
        public async Task<ActionResult<IEnumerable<Environment2D>>> Get()
        {
            var environment2Ds = await _environment2DRepository.ReadAllAsync();
            return Ok(environment2Ds);
        }

        [HttpGet("{environment2DId}", Name = "ReadEnvironment2D")]
        public async Task<ActionResult<Environment2D>> Get(Guid environment2DId)
        {
            var environment2D = await _environment2DRepository.ReadByIdAsync(environment2DId);

            if (environment2D == null)
                return NotFound();

            return Ok(environment2D);
        }

        [HttpPost(Name = "CreateEnvironment2D")]
        public async Task<ActionResult> Add(Environment2D environment2D)
        {
            environment2D.Id = Guid.NewGuid();

            var createdEnvironment2D = await _environment2DRepository.InsertAsync(environment2D);
            return CreatedAtRoute("ReadEnvironment2D", new { environment2DId = createdEnvironment2D.Id }, createdEnvironment2D);
        }

        [HttpPut("{environment2DId}", Name = "UpdateEnvironment2D")]
        public async Task<ActionResult> Update(Guid environment2DId, Environment2D newEnvironment2D)
        {
            var existingEnvironment2D = await _environment2DRepository.ReadByIdAsync(environment2DId);

            if (existingEnvironment2D == null)
                return NotFound();

            newEnvironment2D.Id = environment2DId;
            await _environment2DRepository.UpdateAsync(newEnvironment2D);

            return Ok(newEnvironment2D);
        }

        [HttpDelete("{environment2DId}", Name = "DeleteEnvironment2D")]
        public async Task<IActionResult> Delete(Guid environment2DId)
        {
            var existingEnvironment2D = await _environment2DRepository.ReadByIdAsync(environment2DId);

            if (existingEnvironment2D == null)
                return NotFound();

            await _environment2DRepository.DeleteAsync(environment2DId);

            return Ok();
        }
    }
}

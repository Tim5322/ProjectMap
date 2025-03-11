using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;
using System;

namespace ProjectMap.WebApi.Controllers;

[ApiController]
[Route("Object2Ds")]
public class Object2DController : ControllerBase
{
    private readonly Object2DRepository _object2DRepository;
    private readonly ILogger<Object2DController> _logger;

    public Object2DController(Object2DRepository object2DRepository, ILogger<Object2DController> logger)
    {
        _object2DRepository = object2DRepository;
        _logger = logger;
    }

    [HttpGet("{environment2DId}", Name = "ReadObject2Ds")]
    public async Task<ActionResult<IEnumerable<Object2D>>> Get(Guid environment2DId)
    {
        var object2Ds = await _object2DRepository.ReadByEnvironmentIdAsync(environment2DId);
        return Ok(object2Ds);
    }

    [HttpPost(Name = "CreateObject2D")]
    public async Task<ActionResult> Add(Object2D object2D)
    {
        object2D.Id = Guid.NewGuid();

        var createdObject2D = await _object2DRepository.InsertAsync(object2D);
        return CreatedAtRoute("ReadObject2D", new { object2DId = createdObject2D.Id }, createdObject2D);
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

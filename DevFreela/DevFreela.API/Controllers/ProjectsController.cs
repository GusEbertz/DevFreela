using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DevFreela.Application.Services;
using MediatR;
using DevFreela.Application.Querys.GetAllProjects;
using DevFreela.Application.Querys.GetProjectById;
using DevFreela.Application.Comands.InsertProject;
using DevFreela.Application.Comands.UpdateProject;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DevFreela.Application.Comands.DeleteProject;
using DevFreela.Application.Comands.InsertComment;
using DevFreela.Application.Comands.StartProject;
using DevFreela.Application.Comands.CompleteProject;

namespace DevFreela.API.Controllers
{
  [ApiController]
  [Route("api/projects")]
  public class ProjectsController : ControllerBase
  {
    private readonly IProjectService _service;
    private readonly IMediator _mediator;
       public ProjectsController(IProjectService service, IMediator mediator)
        {
          _service = service;
          _mediator = mediator;
        }
    

    //GET api/projects?search=crm
    [HttpGet]
    public async Task<IActionResult> Get(string search = "")
    {
     // var result = _service.GetAll();
      var query = new GetAllProjectsQuery();
      var result = await _mediator.Send(query);
      return Ok(result);
    }

    //api/projects/599
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var query = new GetProjectByIdQuery(id);
      var result = await _mediator.Send(query);
      
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InsertProjectCommand command)
    {
      var result = await _mediator.Send(command);
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return CreatedAtAction(nameof(GetById), new {id=result.Data}, command);
    }

    // api/projects/2
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
    {
      var result = await _mediator.Send(command);
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return NoContent();
    }

    // api/projects/3
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var result = await _mediator.Send(new DeleteProjectCommand(id));
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return NoContent();
    }

    //api/projects/1/comments POST
    [HttpPost("{id}/comments")]
    public async Task<IActionResult> PostComment(int id, InsertCommentCommand command)
    {
      var result = await _mediator.Send(command);
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return NoContent();
    }

    //api/projects/1/start PUT
    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start(int id)
    {

      var result = await _mediator.Send(new StartProjectCommand(id));
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return NoContent();
    }

    //api/projects/1/finish PUT
    [HttpPut("{id}/finish")]
    public async Task<IActionResult> Finish(int id)
    {
      var result = await _mediator.Send(new CompleteProjectCommand(id));
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return NoContent();
    }
        
  }
}

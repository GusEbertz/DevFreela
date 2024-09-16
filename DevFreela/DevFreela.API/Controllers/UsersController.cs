using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{

  [Route("api/users")]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
      _service = service;
    }
    //api/users/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var result = _service.GetById(id);
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return Ok(result);
    }


    //api/users
    [HttpPost]
    public IActionResult Post([FromBody] CreateUserModel createUserModel) 
    {
      var result = _service.Insert(createUserModel);
      return CreatedAtAction(nameof(GetById), new { id = result.Data }, createUserModel);
    }

    //api/users/1/skills
    [HttpPost("{id}/skills")]
    public IActionResult PostSkills([FromBody] int id, CreateSkillModel createSkill)
    {
      var result = _service.InsertSkill(id, createSkill);
      if (!result.IsSuccess)
      {
        return BadRequest(result.Message);
      }
      return NoContent();

    }

    //api/users/1/login
    [HttpPut("{id}/login")]
    public IActionResult Login(int id, [FromBody] LoginModel loginModel)
    {
      return NoContent();
    }

    //api/users/1/profile-picture
    [HttpPut("{id}/profile-picture")]
    public IActionResult PostProfilePicture(IFormFile file)
    {
      var description = $"File: {file.FileName}, Size: {file.Length}";
      //Aqui podemos processar a imagem
      return Ok(description);
    }

  }
}

using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace DevFreela.API.Controllers
{

  [Route("api/users")]
  public class UsersController : ControllerBase
  {
    private readonly DevFreelaDBContext _db;
    public UsersController(DevFreelaDBContext db) 
    { 
      _db = db;
    }
    //api/users/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = _db.Users
        .Include(u => u.Skills)
        .ThenInclude(u => u.Skill)
        .SingleOrDefault(u => u.Id == id);

      if(user is null)
      {
        return NotFound();
      }
      var model = UserViewModel.FromEntity(user);
      return Ok();
    }


    //api/users
    [HttpPost]
    public IActionResult Post([FromBody] CreateUserModel createUserModel) 
    {
      var user = new User(createUserModel.FullName, createUserModel.Email, createUserModel.BirthDate, createUserModel.Password);
      _db.Users.Add(user);
      _db.SaveChanges();

      return CreatedAtAction(nameof(GetById), new {id=1}, createUserModel);
    }

    //api/users/1/skills
    [HttpPost("{id}/skills")]
    public IActionResult PostSkills([FromBody] int id, UserSkillsModel userSkillsModel)
    {
      var userSkills = userSkillsModel.SkillIds.Select(s => new UserSkill(id, s)).ToList();
      _db.UserSkills.AddRange(userSkills);
      _db.SaveChanges();
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

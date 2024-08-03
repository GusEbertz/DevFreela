using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace DevFreela.API.Controllers
{

  [Route("api/skills")]
  [ApiController]
  public class SkillsController : ControllerBase
  {
        private readonly DevFreelaDBContext _db;
    public SkillsController(DevFreelaDBContext db)
    {
      _db = db;
    }


        [HttpGet]
    //GET api/skills
    public IActionResult GetAll()
    {
      var skills = _db.Skills.ToList();
      return Ok(skills);
    }

    [HttpPost]
    //Post api/skills
    public IActionResult Post(CreateSkillModel  model)
    {
      var skill = new Skill(model.Title, model.Description);
      _db.Skills.Add(skill);
      _db.SaveChanges();

      return NoContent();
    }

   
  }
}

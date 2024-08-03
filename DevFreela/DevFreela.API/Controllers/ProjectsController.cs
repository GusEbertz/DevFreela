using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
  [ApiController]
  [Route("api/projects")]
  public class ProjectsController : ControllerBase
  {
       private readonly DevFreelaDBContext _db;
       public ProjectsController(DevFreelaDBContext db)
        {
           _db = db;
        }
    

    //GET api/projects?search=crm
    [HttpGet]
    public IActionResult Get(string search = "")
    {
      var project = _db.Projects
        .Include(p => p.Client)
        .Include(p => p.Freelancer)
        .Where(p => !p.IsDeleted).ToList();

      var model = project.Select(ProjectItemViewModel.FromEntity).ToList();
      return Ok(model);
    }

    //api/projects/599
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var project = _db.Projects
        .Include(p => p.Client)
        .Include(p => p.Freelancer)
        .Include(p => p.Comments)
        .SingleOrDefault(p => p.Id == id);
       

      var model = ProjectViewModel.FromEntity(project);

      return Ok(model);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateProjectModel createProject)
    {
     var project = createProject.ToEntity();
      _db.Projects.Add(project);
      _db.SaveChanges();
      return CreatedAtAction(nameof(GetById), new {id=createProject.Id}, createProject);
    }

    // api/projects/2
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProject)
    {
      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if(project == null)
      {
        return NotFound();
      }
      
      project.Update(updateProject.Title, updateProject.Description, updateProject.TotalCost);

      _db.Projects.Update(project);
      _db.SaveChanges();
      return NoContent();
    }

    // api/projects/3
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if (project == null)
      {
        return NotFound();
      }

      project.SetAsDeleted();
      _db.Projects.Update(project);
      _db.SaveChanges();
      // Buscar, se não existir/ retorna NOtFound
      //Remover
      return NoContent();
    }

    //api/projects/1/comments POST
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentModel createComment)
    {
      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if (project == null)
      {
        return NotFound();
      }
      var comment = new ProjectComment(createComment.Comment, createComment.IdProject, createComment.IdUser); 

      _db.ProjectComments.Add(comment);
      _db.SaveChanges();

      return NoContent();
      //return BadRequest()
      //return CreatedAtAction(nameof(GetById), new { id = createComment.Id }, createComment);
    }

    //api/projects/1/start PUT
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {

      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if (project == null)
      {
        return NotFound();
      }
      project.Start();
      _db.Projects.Update(project);
      _db.SaveChanges();

      return NoContent();
    }

    //api/projects/1/finish PUT
    [HttpPut("{id}/finish")]
    public IActionResult Finish(int id)
    {
      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if (project == null)
      {
        return NotFound();
      }

      project.Complete();
      _db.Projects.Update(project);
      _db.SaveChanges();

      return NoContent();
    }
        
  }
}

using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
  public class ProjectService : IProjectService
  {
    private readonly DevFreelaDBContext _db;
    public ProjectService(DevFreelaDBContext db)
    {
      _db = db;
    }

    public ResultViewModel Complete(int id)
    {
      var project = _db.Projects.SingleOrDefault(p => p.Id == id);
      if(project is null)
      {
        return ResultViewModel.Error("Projeto nao existe.");
      }
      project.Complete();
      _db.Projects.Update(project);
      _db.SaveChanges();
      return ResultViewModel.Success();
    }

    public ResultViewModel Delete(int id)
    {
      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if (project is null)
      {
        return ResultViewModel.Error("ProjetoNaoEncontrado");
      }

      project.SetAsDeleted();
      _db.Projects.Update(project);
      _db.SaveChanges();
      // Buscar, se não existir/ retorna NOtFound
      //Remover
      return ResultViewModel.Success();
    }

    public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "")
    {
      var project = _db.Projects
        .Include(p => p.Client)
        .Include(p => p.Freelancer)
        .Where(p => !p.IsDeleted).ToList();

      var model = project.Select(ProjectItemViewModel.FromEntity).ToList();
      return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }

    public ResultViewModel<ProjectViewModel> GetById(int id)
    {
      var project = _db.Projects
        .Include(p => p.Client)
        .Include(p => p.Freelancer)
        .Include(p => p.Comments)
        .SingleOrDefault(p => p.Id == id);

      if(project == null)
      {
        return ResultViewModel<ProjectViewModel>.Error("Projeto não Existe");
      }

      var model = ProjectViewModel.FromEntity(project);

      return ResultViewModel<ProjectViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateProjectModel model)
    {
      var project = model.ToEntity();
      _db.Projects.Add(project);
      _db.SaveChanges();
      return ResultViewModel<int>.Success(project.Id);
    }

    public ResultViewModel InsertComment(int id, CreateCommentModel model)
    {
      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if (project == null)
      {
        return ResultViewModel.Error("Projeto nao encontrado");
      }
      var comment = new ProjectComment(model.Comment, model.IdProject, model.IdUser);

      _db.ProjectComments.Add(comment);
      _db.SaveChanges();

      return ResultViewModel.Success();
    }

    public ResultViewModel Start(int id)
    {
      var project = _db.Projects
        .AsNoTracking().SingleOrDefault(p => p.Id == id);
      if (project == null)
      {
        return ResultViewModel.Error("Projeto nao encontrado");
      }
      project.Start();
      _db.Projects.Update(project);
      _db.SaveChanges();

      return ResultViewModel.Success();
    }

    public ResultViewModel Update(UpdateProjectModel model)
    {
      var project = _db.Projects
         .AsNoTracking().SingleOrDefault(p => p.Id == model.Id);
      if (project == null)
      {
        return ResultViewModel.Error("Projeto Não Existe");
      }

      project.Update(model.Title, model.Description, model.TotalCost);

      _db.Projects.Update(project);
      _db.SaveChanges();
      return ResultViewModel.Success();
    }

    ResultViewModel<List<ProjectViewModel>> IProjectService.GetAll(string search)
    {
      throw new NotImplementedException();
    }
  }
}

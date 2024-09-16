using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Querys.GetAllProjects
{
  public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
  {
    private readonly DevFreelaDBContext _db;
    public GetAllProjectsHandler(DevFreelaDBContext db)
    {
      _db = db;
    }
    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
      var project = await _db.Projects
        .Include(p => p.Client)
        .Include(p => p.Freelancer)
        .Where(p => !p.IsDeleted).ToListAsync();

      var model = project.Select(ProjectItemViewModel.FromEntity).ToList();
      return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }
  }
}

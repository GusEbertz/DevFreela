using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Querys.GetProjectById
{
  public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
  {
    public GetProjectByIdQuery(int id)
    {
      Id = id;
    }

    public int Id { get; set; }
    }

  public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
  {
    private readonly DevFreelaDBContext _db;
    public GetProjectByIdHandler(DevFreelaDBContext db)
    {
      _db = db;
    }
    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
      var project = await _db.Projects
        .Include(p => p.Client)
        .Include(p => p.Freelancer)
        .Include(p => p.Comments)
        .SingleOrDefaultAsync(p => p.Id == request.Id);

      if (project == null)
      {
        return ResultViewModel<ProjectViewModel>.Error("Projeto não Existe");
      }

      var model = ProjectViewModel.FromEntity(project);

      return ResultViewModel<ProjectViewModel>.Success(model);
    }
  }
}

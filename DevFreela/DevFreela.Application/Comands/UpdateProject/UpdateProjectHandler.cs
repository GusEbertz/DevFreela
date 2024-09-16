using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.UpdateProject
{
  public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
  {
    private readonly DevFreelaDBContext _db;
    public UpdateProjectHandler(DevFreelaDBContext db)
    {
      _db = db;
    }

    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
      var project = await _db.Projects
         .AsNoTracking().SingleOrDefaultAsync(p => p.Id == request.Id);
      if (project == null)
      {
        return ResultViewModel.Error("Projeto Não Existe");
      }

      project.Update(request.Title, request.Description, request.TotalCost);

      _db.Projects.Update(project);
      await _db.SaveChangesAsync();
      return ResultViewModel.Success();
    }
  }
}

using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.StartProject
{
  public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
  {
    private readonly DevFreelaDBContext _db;
    public StartProjectHandler(DevFreelaDBContext db)
    {
      _db = db;
    }

    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
      var project = await _db.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
      if (project is null)
      {
        return ResultViewModel.Error("Projeto nao existe.");
      }
      project.Start();
      _db.Projects.Update(project);
      await _db.SaveChangesAsync();
      return ResultViewModel.Success();
    }
  }
}

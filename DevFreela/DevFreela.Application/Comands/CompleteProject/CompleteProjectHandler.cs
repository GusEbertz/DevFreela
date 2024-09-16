using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.CompleteProject
{
  public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
  {

    private readonly DevFreelaDBContext _db;
    public CompleteProjectHandler(DevFreelaDBContext db)
    {
      _db = db;
    }
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
      var project = await _db.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
      if (project is null)
      {
        return ResultViewModel.Error("Projeto nao existe.");
      }
      project.Complete();
      _db.Projects.Update(project);
      await _db.SaveChangesAsync();
      return ResultViewModel.Success();
    }
  }
}

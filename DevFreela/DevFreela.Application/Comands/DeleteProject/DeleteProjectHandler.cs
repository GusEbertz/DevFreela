using DevFreela.Application.Comands.CompleteProject;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.DeleteProject
{
  public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
  {
    private readonly DevFreelaDBContext _db;
    public DeleteProjectHandler(DevFreelaDBContext db)
    {
      _db = db;
    }


    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
      var project = await _db.Projects
        .AsNoTracking().SingleOrDefaultAsync(p => p.Id == request.Id);
      if (project is null)
      {
        return ResultViewModel.Error("ProjetoNaoEncontrado");
      }

      project.SetAsDeleted();
      _db.Projects.Update(project);
      await _db.SaveChangesAsync();
      // Buscar, se não existir/ retorna NOtFound
      //Remover
      return ResultViewModel.Success();
    }
  }
}

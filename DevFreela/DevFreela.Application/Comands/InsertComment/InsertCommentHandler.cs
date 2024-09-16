using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.InsertComment
{
  public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
  {
    private readonly DevFreelaDBContext _db;
    public InsertCommentHandler(DevFreelaDBContext db)
    {
      _db = db;
    }
    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
      var project = await _db.Projects
        .AsNoTracking().SingleOrDefaultAsync(p => p.Id == request.IdProject);
      if (project == null)
      {
        return ResultViewModel.Error("Projeto nao encontrado");
      }
      var comment = new ProjectComment(request.Comment, request.IdProject, request.IdUser);

      await _db.ProjectComments.AddAsync(comment);
      await _db.SaveChangesAsync();

      return ResultViewModel.Success();
    }
  }
}

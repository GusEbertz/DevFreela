using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.InsertProject
{
  public class ValidateInsertProjectCommandBehavior :
    IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
  {

    private readonly DevFreelaDBContext _db;
    public ValidateInsertProjectCommandBehavior(DevFreelaDBContext db)
    {
      _db = db;
    }
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
    {
      var clientExists = _db.Users.Any(u => u.Id == request.IdClient);
      var freelanceExists = _db.Users.Any(u => u.Id == request.IdFreelance);
      if(!clientExists || !freelanceExists ) 
      {
        return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos.");
      }

      return await next();
    }
  }
}

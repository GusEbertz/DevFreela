using DevFreela.Application.Models;
using DevFreela.Application.Notifications;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.InsertProject
{
  public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
  {
    private readonly DevFreelaDBContext _db;
    private readonly IMediator _mediator;
    public InsertProjectHandler(DevFreelaDBContext db, IMediator mediator)
    {
      _db = db;
      _mediator = mediator;
    }
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
      var project = request.ToEntity();
      await _db.Projects.AddAsync(project);
      await _db.SaveChangesAsync();
      var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
      await _mediator.Publish(projectCreated);
      return ResultViewModel<int>.Success(project.Id);
    }
  }
}

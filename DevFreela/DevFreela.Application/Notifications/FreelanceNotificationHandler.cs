using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Notifications
{
  internal class FreelanceNotificationHandler : INotificationHandler<ProjectCreatedNotification>
  {
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
      Console.WriteLine($"Notificando os Freelancers sobre o projeto{notification.Title}");
      return Task.CompletedTask;
    }
  }
}

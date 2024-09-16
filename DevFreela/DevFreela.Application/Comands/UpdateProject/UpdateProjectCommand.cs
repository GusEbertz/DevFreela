using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.UpdateProject
{
  public class UpdateProjectCommand : IRequest<ResultViewModel>
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TotalCost { get; set; }
  }
}

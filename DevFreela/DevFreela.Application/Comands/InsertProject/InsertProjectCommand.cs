using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.InsertProject
{
  public class InsertProjectCommand : IRequest<ResultViewModel<int>>
  {
    public int Id { get; set; }
    public int IdClient { get; set; }
    public int IdFreelance { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TotalCost { get; set; }
    public Project ToEntity()
      => new(Title, Description, IdClient, IdFreelance, TotalCost);
  }
}

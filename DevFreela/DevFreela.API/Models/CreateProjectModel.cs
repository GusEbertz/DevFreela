using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
  public class CreateProjectModel
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

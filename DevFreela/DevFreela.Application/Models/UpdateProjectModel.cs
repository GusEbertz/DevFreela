namespace DevFreela.Application.Models
{
  public class UpdateProjectModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description {  get; set; }

    public int TotalCost { get; set; }
  }
}

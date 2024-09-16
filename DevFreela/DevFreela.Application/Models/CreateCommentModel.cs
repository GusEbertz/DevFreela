namespace DevFreela.Application.Models
{
  public class CreateCommentModel
  {
        public string Comment { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }
  }
}

namespace DevFreela.API.Entities
{
  public class ProjectComment : BaseEntity
  {
    public ProjectComment():base() { }

        public ProjectComment(string content, int idProject, int idUser)
        {
            Content = content;
            IdProjeto = idProject;
            IdUser = idUser;
        }

        public int IdProjeto { get; private set; }
        public string Content { get; private set; }
        public int IdUser { get; set; }
        public Project Project { get; private set; }
        public User User { get; private set; }
  }
}

namespace DevFreela.Core.Entities
{
  public class Skill : BaseEntity
  {
    public Skill(string title, string description) :base() 
    {
      Title = title;
      Description = description;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Rating { get; private set; }

        public List<UserSkill> UserSkills { get; private set; }

    }
}

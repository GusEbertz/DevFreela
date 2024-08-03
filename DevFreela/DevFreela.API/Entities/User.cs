using DevFreela.API.Enums;
using System.ComponentModel.Design;

namespace DevFreela.API.Entities
{
  public class User : BaseEntity
  {
        public User(string fullName, string email, DateTime birthDate, string password)
      : base()
        {
             FullName = fullName;
             Email = email;
             BirthDate = birthDate;
             Password = password;
             Active = true;

             Skills = [];
             OwnedProjects = [];
             FreelanceProjects = [];
             Comments = [];
        }
        public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime BirthDate { get; private set; }
    
    public bool Active { get; private set; }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? LastModifiedDate { get; private set;}

    public List<UserSkill> Skills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelanceProjects { get; private set; }
    public List<ProjectComment> Comments { get; private set; }
  }

}

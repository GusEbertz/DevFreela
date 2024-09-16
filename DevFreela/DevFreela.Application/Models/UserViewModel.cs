using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
  public class UserViewModel
  {
    public UserViewModel(string fullName, string email, bool active, List<string> skills)
    {
      FullName = fullName;
      Email = email;
      Active = active;
      Skills = skills;
    }

    public string FullName { get;  set; }
    public string Email { get;  set; }
   
    public bool Active { get;  set; }
  
    public List<string> Skills { get;  set; }

    public static UserViewModel FromEntity(User user)
    {
      var skills = user.Skills.Select(u => u.Skill.Description).ToList();
      return new UserViewModel(user.FullName, user.Email, user.Active, skills);
    }
  }
}

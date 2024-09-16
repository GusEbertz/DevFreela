using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
  public class UserSkillsModel
  {
    public UserSkillsModel(int[] skillIds, int id)
    {
      SkillIds = skillIds;
      Id = id;
    }

    public int[] SkillIds { get; set; }
    public int Id { get; set;}

    public static UserSkillsModel FromEntity(UserSkill entity)
     => new(entity.Skill, entity.IdUser);
  } 
}

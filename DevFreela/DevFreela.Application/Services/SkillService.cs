using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
  public class SkillService : ISkillService
  {
    private readonly DevFreelaDBContext _db;
    public SkillService(DevFreelaDBContext db)
    {
      _db = db;
    }
    public ResultViewModel<UserSkillsModel> GetAll(string search = "")
    {
      var skills = _db.Skills.ToList();
      var model = skills.Select(UserSkillsModel.FromEntity).ToList();
      return ResultViewModel<UserSkillsModel>.Success(model);
    }

  }
}

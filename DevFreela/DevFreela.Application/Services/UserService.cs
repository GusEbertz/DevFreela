using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
  internal class UserService : IUserService
  {

    private readonly DevFreelaDBContext _db;
    public UserService(DevFreelaDBContext db)
    {
      _db = db;
    }
    public ResultViewModel<List<UserViewModel>> GetAll(string search = "")
    {
      var user = _db.Users;       

      var model = user.Select(UserViewModel.FromEntity).ToList();
      return ResultViewModel<List<UserViewModel>>.Success(model);
    }

    public ResultViewModel<UserViewModel> GetById(int id)
    {
      var user = _db.Users
        .Include(u => u.Skills)        
        .SingleOrDefault(u => u.Id == id);

      if (user is null)
      {
        ResultViewModel<UserViewModel>.Error("Usuario não Existe");
      }
      var model = UserViewModel.FromEntity(user);
      return ResultViewModel<UserViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateUserModel model)
    {
      var user = model.ToEntity();
      _db.Users.Add(user);
      _db.SaveChanges();
      return ResultViewModel<int>.Success(user.Id);
    }

    public ResultViewModel InsertSkill(int id, UserSkillsModel model)
    {
      var userSkill = model.SkillIds.Select(x => new UserSkill(id, x)).ToList();

      _db.UserSkills.AddRange(userSkill);
      _db.SaveChanges();

      return ResultViewModel.Success();
    }
  }
}

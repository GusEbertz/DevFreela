using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
  public interface IProjectService
  {
    ResultViewModel<List<ProjectViewModel>> GetAll(string search = "");
    ResultViewModel<ProjectViewModel> GetById(int id);
    ResultViewModel<int> Insert(CreateProjectModel model);
    ResultViewModel Update(UpdateProjectModel model);
    ResultViewModel Delete(int id);
    ResultViewModel Start(int id);
    ResultViewModel Complete(int id);
    ResultViewModel InsertComment(int id, CreateCommentModel model);
  }
}

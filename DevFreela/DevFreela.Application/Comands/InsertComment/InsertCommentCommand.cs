﻿using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Comands.InsertComment
{
  public class InsertCommentCommand : IRequest<ResultViewModel>
  {
    public string Comment { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }
  }
}

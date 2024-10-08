﻿using DevFreela.Core.Enums;
using System.Runtime.CompilerServices;

namespace DevFreela.Core.Entities
{
  public class Project : BaseEntity
  {
    protected Project() { }
    public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
    :base()
    {
      Title = title;
      Description = description;
      IdClient = idClient;
      IdFreelancer = idFreelancer;
      TotalCost = totalCost;
      Status = ProjectStatusEnum.Created;
      Comments = [];
    }

    public string Title { get; private set; }
    public string Description { get; private set; }

    public int IdClient { get; private set; }
    public int IdFreelancer { get; private set; }
    public User Client { get; private set; }
    public User Freelancer { get; private set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

    public decimal TotalCost { get; set; }

    public ProjectStatusEnum Status { get; set; }

    public List<ProjectComment> Comments { get; private set;}

    public void Cancel()
    {
      if(Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Suspended) 
      {
        Status = ProjectStatusEnum.Cancelled;
      }
    }

    public void Start()
    {
      if (Status == ProjectStatusEnum.Created)
      {
        Status = ProjectStatusEnum.InProgress;
        StartedAt = DateTime.Now;
      }
    }

    public void Complete()
    {
      if (Status == ProjectStatusEnum.PendentPayment || Status == ProjectStatusEnum.Completed)
      {
        Status = ProjectStatusEnum.Completed;
        FinishedAt = DateTime.Now;
      }
    }

    public void SetPendentPayment()
    {
      if (Status == ProjectStatusEnum.InProgress)
      {
        Status = ProjectStatusEnum.PendentPayment;
      }
    }

    public void Update(string title, string description, decimal totalCost)
    {
      Title = title;
      Description = description;
      TotalCost = totalCost;
    }
  }
}

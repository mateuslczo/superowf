using System;
using TaskList._01___Application.ViewModels.Enums;
using TaskList._01___Domain;

namespace TaskListWeb.ViewModelResults
{
    public class TasksViewModelResult : BaseEntity
    {
        public string Title { get; set; }

        public EnTypeStatusViewModel Status { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? ConclusionDate { get; set; }
    }
}

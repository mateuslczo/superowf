using System;
using TaskList._01___Domain;
using TaskList._01___Domain.Entities.Enums;

namespace TaskListWeb.ViewModelResults
{
    public class TasksViewModelResult
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public EnTypeStatus Status { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? ConclusionDate { get; set; }
    }
}

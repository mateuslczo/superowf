using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList._01___Application.ViewModels
{

    public class TasksViewModel
    {

        public string Title { get; set; }

        public EnTypeStatusViewModel Status { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? ConclusionDate { get; set; }
    }
}
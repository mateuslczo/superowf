using MediatR;
using System;
using TaskList._01___Domain.Entities.Enums;

namespace TaskList.Notifications
{
    public class TaskActionNotification : INotification
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? ConclusionDate { get; set; }

        public ActionNotifications Action { get; set; }
    }
}

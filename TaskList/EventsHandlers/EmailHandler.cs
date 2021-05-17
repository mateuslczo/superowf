using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskList.Notifications;

namespace TaskList.EventsHandlers
{
    public class EmailHandler : INotificationHandler<TaskActionNotification>
    {
        public Task Handle(TaskActionNotification notification, CancellationToken cancellationToken)
        {

            return Task.Run(() =>
            {
                Console.WriteLine("A tarefa {0} {1} foi {2} com sucesso", notification.Title, notification.Description, notification.Action.ToString().ToLower());
            });
        }
    }
}

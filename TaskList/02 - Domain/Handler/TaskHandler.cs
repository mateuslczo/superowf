using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskList._01___Domain;
using TaskList._02___Domain.Command;
using TaskList._03___Infra.Repositories;
using TaskList.Notifications;

namespace TaskList._02___Domain.Handler
{
    public class TaskHandler : IRequestHandler<TaskCreateCommand, string>, IRequestHandler<TaskDeleteCommand, string>, IRequestHandler<TaskUpdateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly ITaskRepository _taskRepository;

        public TaskHandler(IMediator mediator, ITaskRepository taskRepository)
        {
            _mediator = mediator;
            _taskRepository = taskRepository;
        }

        public async Task<string> Handle(TaskCreateCommand request, CancellationToken cancellationToken)
        {
            var task = new Tasks();

            task.Title = request.Title;
            task.Description = request.Description;
            task.CreateDate = request.CreateDate;
            task.EditDate = request.EditDate;
            task.ConclusionDate = request.ConclusionDate;

            await _taskRepository.Save(task);

            await _mediator.Publish(new TaskActionNotification
            {
                Title = request.Title,
                Description = request.Description,
                CreateDate = request.CreateDate,
                EditDate = request.EditDate,
                ConclusionDate = request.ConclusionDate,
                Action = ActionNotifications.Created
            });
            ;

            return await Task.FromResult("Tarefa atualizada com sucesso");

        }

        public async Task<string> Handle(TaskDeleteCommand request, CancellationToken cancellationToken)
        {

            var task =  await _taskRepository.Get(request.Id);

            if (task == null)
                return await Task.FromResult("Tarefa não encontrada");

            _taskRepository.Remove(task);

            await _mediator.Publish(new TaskActionNotification
            {
                Title = task.Title,
                Description = task.Description,
                CreateDate = task.CreateDate,
                EditDate = task.EditDate,
                ConclusionDate = task.ConclusionDate
            });

            return await Task.FromResult("Tarefa excluida");


        }

        public async Task<string> Handle(TaskUpdateCommand request, CancellationToken cancellationToken)
        {

            Tasks task =  null; 
            //await _taskRepository.Get(request.Id);

            if (task == null)
                return await Task.FromResult("Tarefa não encontrada");

            task.Title = request.Title;
            task.Description = request.Description;
            task.CreateDate = request.CreateDate;
            task.EditDate = request.EditDate;
            task.ConclusionDate = request.ConclusionDate;

            _taskRepository.Update(task);

            await _mediator.Publish(new TaskActionNotification
            {
                Title = request.Title,
                Description = request.Description,
                CreateDate = request.CreateDate,
                EditDate = request.EditDate,
                ConclusionDate = request.ConclusionDate,
            });

            return await Task.FromResult("Tarefa criada com sucesso");
        }
    }
}

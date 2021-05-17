using MediatR;

namespace TaskList._02___Domain.Command
{
    public class TaskDeleteCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}

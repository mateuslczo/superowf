using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskList._01___Domain;

namespace TaskList._03___Infra.Repositories
{

    public class TaskRepository : RepositoryBase<Tasks>, ITaskRepository
    {

        protected readonly DataContext context;

        public TaskRepository(DataContext _dataContext) : base(_dataContext)
        {
            context = _dataContext;
        }


        public async Task<Tasks> GetTaskByNameAsync(string title)
        {
            var task = await context.Tasks.Where(t => t.Title.StartsWith(title)).FirstOrDefaultAsync();
            return task;
        }

        public async Task<ICollection<Tasks>> GetAllTaskAsync()
        {
            var taskList = await context.Tasks.AsNoTracking().ToListAsync();
            return taskList;
        }

     
    }
}
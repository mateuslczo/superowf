using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskList._01___Domain;
using TaskList._03___Infra.Repositories.DatabaseContext;

namespace TaskList._03___Infra.Repositories
{

    public class TaskRepository : RepositoryBase<Tasks>, ITaskRepository
    {

        protected readonly DataContext context;

        public TaskRepository(DataContext _dataContext) : base(_dataContext)
        {
            context = _dataContext;
        }


        public async Task<ICollection<Tasks>> GetTaskByTitleAsync(string title)
        {
            var task = await context.Tasks.Where(t => t.Title.StartsWith(title)).ToListAsync();
            return task;
        }

        public async Task<ICollection<Tasks>> GetAllTaskAsync()
        {
            var taskList = await context.Tasks.AsNoTracking().ToListAsync();
            return taskList;
        }

        public async Task<bool> ValidateUniqueTasks(string title) 
        {

            var _task = await GetTaskByTitleAsync(title);

            if (_task.Count > 0)
                return false;

            return true;

        }


    }
}
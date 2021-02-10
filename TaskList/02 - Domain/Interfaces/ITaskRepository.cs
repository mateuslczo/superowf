using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList._01___Domain;
using TaskList._01___Domain.Entities.Enums;
using TaskList._01___Domain.Interfaces;

namespace TaskList._03___Infra.Repositories
{
    public interface ITaskRepository : IRepositoryBase<Tasks>
    {

        /// <summary>
        /// Lista de tarefas por nome
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Task</returns>
        Task<ICollection<Tasks>> GetTaskByTitleAsync(string name);

        /// <summary>
        /// Lista de todaas as tarefas
        /// </summary>
        /// <returns>ICollection<Task></returns>
        Task<ICollection<Tasks>> GetAllTaskAsync();

        /// <summary>
        /// Verifica se a task já existe
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<bool> ValidateUniqueTasksAsync(string title);

        /// <summary>
        /// Atualizar status da tarefa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tpStatus"></param>
        /// <returns></returns>
        Task UpdateStatusTasks(Tasks _task, int tpStatus);


    }
}

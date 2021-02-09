﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList._01___Domain;
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
        Task<Tasks> GetTaskByNameAsync(string name);

        /// <summary>
        /// Lista de todaas as tarefas
        /// </summary>
        /// <returns>ICollection<Task></returns>
        Task<ICollection<Tasks>> GetAllTaskAsync();


    }
}
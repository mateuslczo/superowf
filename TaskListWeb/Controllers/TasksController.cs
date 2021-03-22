using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList._01___Application.ViewModels;
using TaskList._01___Domain;
using TaskList._01___Domain.Entities.Enums;
using TaskList._01___Domain.Interfaces;
using TaskList._03___Infra.Repositories;


namespace TaskListWeb.Controllers
{

    /// <summary>
    /// Endpoints
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal server error</response>
    [EnableCors]
    [ApiController]
    [Route("v1/tasks")]
    public class TasksController : ControllerBase
    {


        private readonly ITaskRepository tasksRepository;
        private readonly IMapper mapper;
        private readonly IDataTransaction dataTransaction;

        /// <summary>
        /// Método cosntrutor
        /// </summary>
        /// <param name="_tasksRepository"></param>
        /// <param name="_mapper"></param>
        public TasksController([FromServices] ITaskRepository _tasksRepository, [FromServices] IMapper _mapper, IDataTransaction _dataTransaction)
        {
            tasksRepository = _tasksRepository;
            mapper = _mapper;
            dataTransaction = _dataTransaction;
        }

        /// <summary>
        /// Listar tarefas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TasksViewModelResult>>> AllTasks()
        {
            try
            {

                var _tasks = await tasksRepository.GetAllTaskAsync();

                if (_tasks.Count == 0)
                    return NotFound();

                var viewTasks = mapper.Map<List<TasksViewModelResult>>(_tasks);

                string jsonData = JsonConvert.SerializeObject(viewTasks);

                return Ok(jsonData);


            } catch (Exception error)
            {

                throw new Exception(error.Message.ToString());

            }

        }

        /// <summary>
        /// Tarefas por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<TasksViewModelResult>> TasksById(long id)
        {
            try
            {
                var _tasks = await tasksRepository.Get(id);

                if (_tasks == null)
                    return NotFound();

                var viewTasks = mapper.Map<TasksViewModelResult>(_tasks);

                string jsonData = JsonConvert.SerializeObject(viewTasks);

                return Ok(jsonData);


            } catch (Exception error)
            {

                throw new Exception(error.Message.ToString());

            }
        }

        /// <summary>
        /// Buscar tarefa por titulo
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{title}")]
        public async Task<ActionResult<List<TasksViewModelResult>>> TasksByTitle(string title)
        {
            try
            {
                var _tasks = await tasksRepository.GetTaskByTitleAsync(title.ToLower());

                if (_tasks.Count == 0)
                    return NotFound();

                var viewTasks = mapper.Map<List<TasksViewModelResult>>(_tasks);

                string jsonData = JsonConvert.SerializeObject(viewTasks);

                return Ok(jsonData);

            } catch (Exception error)
            {

                throw new Exception(error.Message.ToString());

            }

        }


        /// <summary>
        /// Inserir tarefa
        /// </summary>
        /// <param name="_tasks"></param>
        /// <returns></returns>
        //[Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<TasksViewModel>> Post([FromBody] TasksViewModel _tasks)
        {


            if (_tasks == null)
                return BadRequest(new { error = "Não foi possível abrir tarefa, verifique!" });

            try
            {
                object result;
                ;

                if (!tasksRepository.ValidateUniqueTasksAsync(_tasks.Title).Result)
                {
                    result = new
                    {
                        message = "Tarefa já existe"
                    };

                    return Ok(result);
                }

                var _tasksSent = mapper.Map<Tasks>(_tasks);

                _tasksSent.ChangeDateForStatus(EnTypeStatus.Open);

                await tasksRepository.Save(_tasksSent);

                dataTransaction.Commit();

                result = new
                {
                    message = "Tarefa criada"
                };

                return Ok(result);

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                throw new Exception(error.Message.ToString());

            }
        }


        /// <summary>
        /// Ataulizar tarefa
        /// </summary>
        /// <param name="_tasks"></param>
        /// <returns></returns>
        //[Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult<TasksViewModel>> Put([FromBody] TasksViewModel _tasks)
        {

            if (_tasks == null)
                return BadRequest(new { error = "Não foi possível atualizar tarefa, verifique!" });

            try
            {
                var entityToUpd = await tasksRepository.Get(_tasks.Id);

                entityToUpd.Status = _tasks.Status;
                entityToUpd.Description = _tasks.Description;
                entityToUpd.Title = _tasks.Title;

                var _tasksSent = mapper.Map<Tasks>(entityToUpd);

                tasksRepository.Update(_tasksSent);

                dataTransaction.Commit();

                var result = new
                {
                    message = "Tarefa atualizada"
                };

                return Ok(result);

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                throw new Exception(error.Message.ToString());

            }
        }

        /// <summary>
        /// Ataulizar status da tarefa 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:long}/{status:int}")]
        public async Task<ActionResult<TasksViewModel>> PutStatus(long id, int status)

        {

            try
            {
                var _task = mapper.Map<Tasks>(await tasksRepository.Get(id));

                if (_task == null)
                    return NotFound();

                await tasksRepository.UpdateStatusTasks(_task, status);

                dataTransaction.Commit();

                var result = new
                {
                    message = "Status atualizado"
                };


                return Ok(result);

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                throw new Exception(error.Message.ToString());

            }
        }


        /// <summary>
        /// Remover tarefa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {

            var _tasks = mapper.Map<Tasks>(await tasksRepository.Get(id));
            //var _tasks = await tasksRepository.Get(id);

            if (_tasks == null)
                return NotFound();

            try {

                tasksRepository.Remove(_tasks);
                dataTransaction.Commit();

                return Ok(new { Success = "Tarefa excluida" });

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                throw new Exception(error.Message.ToString());

            }

        }
    }
}

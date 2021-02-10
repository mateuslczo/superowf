using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    //[Authorize("Bearer")] desabilitado para facilitar os testes
    [Route("v1/tasks")]
    public class TasksController : ControllerBase
    {

        /// <summary>
        /// Listar tarefas
        /// </summary>
        /// <param name="tasksRepository"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<TasksViewModelResult>>> AllTasks([FromServices] ITaskRepository tasksRepository,
                                                                 [FromServices] IMapper mapper)
        {
            try
            {

                var _tasks = await tasksRepository.GetAllTaskAsync();

                if (_tasks.Count == 0)
                    return NotFound();

                var viewTasks = mapper.Map<List<TasksViewModelResult>>(_tasks);

                return Ok(viewTasks);


            } catch (Exception error)
            {

                return BadRequest(new { error = error.Message.ToString() });

            }

        }

        /// <summary>
        /// Tarefas por id
        /// </summary>
        /// <param name="tasksRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<TasksViewModelResult>> TasksById([FromServices] ITaskRepository tasksRepository,
                                                                  [FromServices] IMapper mapper,
                                                                  long id)
        {
            try
            {
                var _tasks = await tasksRepository.Get(id);

                if (_tasks == null)
                    return NotFound();

                var viewTasks = mapper.Map<TasksViewModelResult>(_tasks);

                return Ok(viewTasks);

            } catch (Exception error)
            {

                return BadRequest(new { error = error.Message.ToString() });

            }
        }

        /// <summary>
        /// Buscar tarefa por titulo
        /// </summary>
        /// <param name="tasksRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{title}")]
        public async Task<ActionResult<List<TasksViewModelResult>>> TasksByTitle([FromServices] ITaskRepository tasksRepository,
                                                                          [FromServices] IMapper mapper,
                                                                          string title)
        {
            try
            {
                var _tasks = await tasksRepository.GetTaskByTitleAsync(title.ToLower());

                if (_tasks.Count == 0)
                    return NotFound();

                var viewTasks = mapper.Map<List<TasksViewModelResult>>(_tasks);
                return Ok(viewTasks);

            } catch (Exception error)
            {

                return BadRequest(new { error = error.Message.ToString() });

            }

        }


        /// <summary>
        /// Inserir tarefa
        /// </summary>
        /// <param name="tasksRepository"></param>
        /// <param name="dataTransaction"></param>
        /// <param name="mapper"></param>
        /// <param name="_tasks"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TasksViewModel>> Post([FromServices] ITaskRepository tasksRepository,
                                                   [FromServices] IDataTransaction dataTransaction,
                                                   [FromServices] IMapper mapper,
                                                   [FromBody] TasksViewModel _tasks)
        {


            if (_tasks == null)
                return BadRequest(new { error = "Não foi possível abrir tarefa, verifique!" });

            try
            {

                if (!tasksRepository.ValidateUniqueTasksAsync(_tasks.Title).Result)
                    return BadRequest("Já existe uma tarefa com esse nome");

                 var _tasksSent = mapper.Map<Tasks>(_tasks);

                _tasksSent.ChangeDateForStatus(EnTypeStatus.Open);

                await tasksRepository.Save(_tasksSent);

                dataTransaction.Commit();

                return Ok("Tarefa criada");

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                return BadRequest(new { Error = error.Message.ToString() });

            }
        }


        /// <summary>
        /// Ataulizar tarefa
        /// </summary>
        /// <param name="tasksRepository"></param>
        /// <param name="dataTransaction"></param>
        /// <param name="mapper"></param>
        /// <param name="_tasks"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<TasksViewModel>> Put([FromServices] ITaskRepository tasksRepository,
                                                  [FromServices] IDataTransaction dataTransaction,
                                                  [FromServices] IMapper mapper,
                                                  [FromBody] TasksViewModel _tasks)
        {

            if (_tasks == null)
                return BadRequest(new { error = "Não foi possível atualizar tarefa, verifique!" });

            try
            {

                var _tasksSent = mapper.Map<Tasks>(_tasks);
                tasksRepository.Update(_tasksSent);

                dataTransaction.Commit();

                return Ok(new { Success = "Tarefa atualizada" });

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                return BadRequest(new { Error = error.Message.ToString() });

            }
        }

        /// <summary>
        /// Ataulizar status da tarefa
        /// </summary>
        /// <param name="tasksRepository"></param>
        /// <param name="dataTransaction"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:long}/{status:int}")]
        public async Task<ActionResult<TasksViewModel>> PutStatus([FromServices] ITaskRepository tasksRepository,
                                                  [FromServices] IDataTransaction dataTransaction, [FromServices] IMapper mapper,
                                                  long id, int status)

        {

            try
            {
                var _task = mapper.Map<Tasks>(await tasksRepository.Get(id));

                if (_task == null)
                    return NotFound();

                await tasksRepository.UpdateStatusTasks(_task, status);

                dataTransaction.Commit();

                return Ok(new { Success = "Tarefa atualizada" });

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                return BadRequest(new { Error = error.Message.ToString() });

            }
        }


        /// <summary>
        /// Remover tarefa
        /// </summary>
        /// <param name="tasksRepository"></param>
        /// <param name="dataTransaction"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult> Delete([FromServices] ITaskRepository tasksRepository,
                                   [FromServices] IDataTransaction dataTransaction,
                                   int id)
        {

            var _tasks = await tasksRepository.Get(id);

            if (_tasks == null)
                return NotFound();

            try
            {

                tasksRepository.Remove(_tasks);
                dataTransaction.Commit();

                return Ok(new { Success = "Tarefa excluida" });  // CRIAR CONTROLLER TAREFAS CONCLUIDAS, PENDENTES ETC

            } catch (Exception error)
            {

                dataTransaction.RollBack();
                return BadRequest(new { Error = error.Message.ToString() });

            }

        }
    }
}

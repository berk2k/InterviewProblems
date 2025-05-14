using interview3.Models;
using interview3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace interview3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _service;

        public TaskController(TaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAllTasks() {
            var tasks = _service.GetAllTasks();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public ActionResult GetTask(Guid id)
        {
            try
            {
                var task = _service.GetTaskById(id);
                return Ok(task);
            }
            catch(KeyNotFoundException)
            {   
                return NotFound(new { message = "Task not found" });
            }
        }

        [HttpPost]
        public ActionResult PostTask([FromBody] TaskItem task)
        {
            try
            {
                var createdTask = _service.CreateTask(task);
                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(Guid id,[FromBody] TaskItem task)
        {
            try
            {
                var item = _service.UpdateTask(id,task);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Task not found" });
            }

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(Guid id)
        {
            try
            {
                _service.DeleteTask(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Task not found" });
            }
        }

    }
}

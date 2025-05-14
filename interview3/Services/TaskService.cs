using interview3.Models;
using interview3.Repositories;

namespace interview3.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IEnumerable<TaskItem> GetAllTasks() => _taskRepository.GetAllTasks();
        public TaskItem GetTaskById(Guid id) => _taskRepository.GetTaskById(id);
        public TaskItem CreateTask(TaskItem task) => _taskRepository.CreateTask(task);
        public TaskItem UpdateTask(Guid id,TaskItem task) => _taskRepository.UpdateTask(id,task); 
        public void DeleteTask(Guid id) => _taskRepository.DeleteTask(id);
    }
}

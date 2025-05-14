using interview3.Models;

namespace interview3.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskItem> GetAllTasks();
        TaskItem GetTaskById(Guid id);
        TaskItem CreateTask(TaskItem task);
        TaskItem UpdateTask(Guid id,TaskItem task);
        void DeleteTask(Guid id);
    }
}

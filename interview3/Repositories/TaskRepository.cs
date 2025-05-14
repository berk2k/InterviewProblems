using interview3.Models;

namespace interview3.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();
        
        public TaskItem CreateTask(TaskItem task)
        {
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.Now;
            _tasks.Add(task);
            return task;
        }

        public void DeleteTask(Guid id)
        {
            var task = GetTaskById(id);
            if (task != null) {
                _tasks.Remove(task);
            }

        }

        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _tasks;
            
        }

        public TaskItem GetTaskById(Guid id)
        {
            var task = _tasks.Find(t=> t.Id == id);
            return task;
        }

        public TaskItem UpdateTask(Guid id,TaskItem task)
        {

            var existingTask = GetTaskById(id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;
                return existingTask;              
            }
            return task;
            
        }
    }
}

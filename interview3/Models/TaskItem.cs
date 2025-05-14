using System.Text.Json.Serialization;

namespace interview3.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskStatus
    {
        TODO,
        IN_PROGRESS,
        DONE
    }
}

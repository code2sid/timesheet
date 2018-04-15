
namespace API.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int TaskTypeId { get; set; }
        public TaskType TaskType { get; set; }
    }
}
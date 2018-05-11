
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class TaskType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
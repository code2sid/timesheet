
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaskTypeId { get; set; }
    }
}
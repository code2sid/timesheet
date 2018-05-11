
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
    }
}
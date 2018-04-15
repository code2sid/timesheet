using System.Collections.Generic;

namespace API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}
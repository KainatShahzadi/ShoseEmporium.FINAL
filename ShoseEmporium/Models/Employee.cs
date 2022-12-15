using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoseEmporium.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }

        public List<Shose> Shoses { get; set; }
    }
}

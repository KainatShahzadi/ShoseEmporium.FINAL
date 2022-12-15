using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace ShoseEmporium.Models
{
    public class Shose
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public int Price { get; set; }

        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }
        public List<Vendor> Vendors { get; set; }
    }
}

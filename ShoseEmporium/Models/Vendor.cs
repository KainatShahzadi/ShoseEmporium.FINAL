using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoseEmporium.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Nationality { get; set; }
        public DateTime DoB { get; set; }

        public List<Shose> Shoses { get; set; }

    }
}

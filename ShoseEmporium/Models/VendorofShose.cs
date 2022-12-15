using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoseEmporium.Models
{
    public class VendorofShose
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Shoses")]
        public int ShoseId { get; set; }
        public Shose Shoses { get; set; }

        [ForeignKey("Vendors")]
        public int VendorId { get; set; }
        public Vendor Vendors { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomIdentityServer.Models
{
    public class Designation
    {
        [Key]
        public int ID { get; set; }

        [Column("Designation")]
        public string Designation1 { get; set; }
    }
}
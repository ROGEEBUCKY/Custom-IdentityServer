using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomIdentityServer.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [Column("Role")]
        [Required]
        [StringLength(50)]
        public string Role1 { get; set; }
    }
}

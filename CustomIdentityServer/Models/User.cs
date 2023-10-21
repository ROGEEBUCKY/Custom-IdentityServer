using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace CustomIdentityServer.Models
{
    public class User 
    {
        [Key]
        public int UserID { get; set; }

        public string Name { get; set; }


        public string Password { get; set; }
        [ForeignKey("Roles")]
        public int RoleID { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }


        public string Email { get; set; }


        public string mobilenum { get; set; }
        [ForeignKey("Designation")]

        public int? designation { get; set; }

        public virtual Designation Designation { get; set; }
        public virtual Role Role { get; set; }
    }
}

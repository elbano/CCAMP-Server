using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCAMPServerModel.Models
{
    public enum EStatusMode
    {
        sm_Inactive = 0,
        sm_Active = 1,
        sm_Suspended = 2,
    }

    public class User
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        public String AuthUserId { get; set; }

        [Required]
        public EStatusMode Status { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}

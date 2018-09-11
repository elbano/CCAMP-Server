using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class User
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        public String AuthToken { get; set; }

        [Required]
        public Boolean Status { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}

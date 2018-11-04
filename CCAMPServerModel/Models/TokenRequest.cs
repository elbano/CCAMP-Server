using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class TokenRequest
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ResponseUrl { get; set; }

        [Required]
        public string RequestDate { get; set; }
    }
}

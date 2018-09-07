using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public Content Content { get; set; }

        [Required]
        public Advertisement Advertisement { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class Deal
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public Campaign campaign { get; set; }

        [Required]
        public ContentCreator contentCreator { get; set; }
    }
}

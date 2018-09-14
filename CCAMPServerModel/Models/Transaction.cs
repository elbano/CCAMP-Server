using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("ContentId")]
        public Content Content { get; set; }

        [ForeignKey("AdvertisementId")]
        public Advertisement Advertisement { get; set; }
    }
}

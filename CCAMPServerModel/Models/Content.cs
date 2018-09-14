using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class Content
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String URLMediaPath { get; set; }

        [ForeignKey("ContentCreatorId")]
        public ContentCreator ContentCreator { get; set; }

        public List<Transaction> TransactionList { get; set; }
    }
}

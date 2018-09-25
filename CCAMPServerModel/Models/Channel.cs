using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class Channel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("ContentCreatorId")]
        public ContentCreator ContentCreator { get; set; }

        public List<Content> ContentList { get; set; }

        public List<Deal> DealList { get; set; }
    }
}

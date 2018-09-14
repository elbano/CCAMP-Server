using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class Advertisement
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Type { get; set; }

        [Required]
        public String StorageURLMediaPath { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        public List<Transaction> TransactionList { get; set; }
    }
}

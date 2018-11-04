using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCAMPServerModel.Models
{
    public enum EDealStatus
    {
        dm_Proposal = 0,
        dm_Accepted = 1,  // marked by content creator
        dm_Completed = 2, // marked by content creator
        dm_Finished = 3, // marked by sponsor
        dm_Denied = 4,
    }

    public enum EDealModality
    {
        dm_Total = 0,
        dm_PerView = 1
    }


    public class Deal
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

        [Required]
        public EDealStatus Status { get; set; }

        [Required]
        public EDealModality Modality { get; set; }

        [Required]
        public Double Amount { get; set; }
    }
}

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
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String LastName { get; set; }

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

        public String CompanyName { get; set; }

        public List<Campaign> CampaigntList { get; set; }

        public List<Channel> ChannelList { get; set; }
    }
}

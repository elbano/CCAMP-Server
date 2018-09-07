using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCAMPServerModel.Models
{
    public class Sponsor : User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public String CompanyName { get; set; }

        public List<Campaign> CampaigntList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace EasySleepApi.Models
{

    [Table("Request")]
    public partial class Request
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public Boolean IsRequestAccepted { get; set; }

        [Required]
        public string ApplicantId { get; set; }
        [ForeignKey("ApplicantId")]
        public ApplicationUser Applicant { get; set; } 
        
        [Required]
        public int OfferId { get; set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }

    }
}

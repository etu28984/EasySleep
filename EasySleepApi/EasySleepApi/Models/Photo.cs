using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace EasySleepApi.Models
{
    
    [Table("Photo")]
    public partial class Photo
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Link { get; set; }

        public int OfferId { get; set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace EasySleepApi.Models
{
    
    [Table("Location")]
    public partial class Location
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Street { get; set; }

        [Required]
        public int HouseNum { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

    }
}

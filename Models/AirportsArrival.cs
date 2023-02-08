namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AirportsArrival")]
    public partial class AirportsArrival
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(30)]
        public string FlightNumber { get; set; }
        public DateTime Date_Arrival { get; set; }

        [Required]
        [StringLength(30)]
        public string City_Arrival { get; set; }

        [Required]
        [StringLength(30)]
        public string Airline { get; set; }

        [Required]
        [StringLength(30)]
        public string Gate { get; set; }

        [Required]
        [StringLength(30)]
        public string Flight_status { get; set; }
    }
}

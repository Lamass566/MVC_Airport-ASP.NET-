namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public partial class Passengers
    {
        [Key]
        public int Id_pass { get; set; }

        public DateTime Date_bir { get; set; }

        [Required]
        [StringLength(30)]
        public string First_Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Second_Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Nationality { get; set; }

        [Required]
        [StringLength(10)]
        public string Sex { get; set; }

        public int Ticket_Сlass { get; set; }

        //[Required]
        [StringLength(30)]
        public string FlightN { get; set; }

        public virtual AirportsDeparture AirportsDeparture { get; set; }
    }
}

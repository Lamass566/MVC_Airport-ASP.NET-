namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AirportsDeparture")]
    public partial class AirportsDeparture
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AirportsDeparture()
        {
            Passengers = new HashSet<Passengers>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(30)]
        public string FlightNumber { get; set; }
        public DateTime Date_Departure { get; set; }

        [Required]
        [StringLength(30)]
        public string City_Departure { get; set; }

        [Required]
        [StringLength(30)]
        public string Airline { get; set; }

        [Required]
        [StringLength(30)]
        public string Gate { get; set; }

        [Required]
        [StringLength(30)]
        public string Flight_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Passengers> Passengers { get; set; }
    }
}

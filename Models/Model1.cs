using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication3.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model2")
        {
        }

        public virtual DbSet<AirportsArrival> AirportsArrival { get; set; }
        public virtual DbSet<AirportsDeparture> AirportsDeparture { get; set; }
        public virtual DbSet<Passengers> Passengers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirportsDeparture>()
                .HasMany(e => e.Passengers)
                .WithRequired(e => e.AirportsDeparture)
                .HasForeignKey(e => e.FlightN)
                .WillCascadeOnDelete(true);

        }
    }
}

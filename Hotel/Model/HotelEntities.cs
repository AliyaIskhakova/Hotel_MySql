using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class HotelEntities: DbContext
    {
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ReservationTariff> ReservationTariff { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Tariff> Tariff { get; set; }

        public HotelEntities()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=12345;database=hotel;",
                new MySqlServerVersion(new Version(8, 0, 37))
            );
        }
    }
}

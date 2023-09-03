using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelWebApp.Models;

namespace HotelWebApp.Data
{
    public class HotelWebAppContext : DbContext
    {
        public HotelWebAppContext (DbContextOptions<HotelWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<HotelWebApp.Models.Hotel> Hotel { get; set; } = default!;

        public DbSet<HotelWebApp.Models.Room>? Room { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zeldassistant.Models;

namespace zeldassistant.Data
{
    public class ZeldaDataContext : DbContext
    {
        public ZeldaDataContext(DbContextOptions<ZeldaDataContext> options) : base(options)
        {
        }

        public DbSet<Marker> Markers { get; set; }
        public DbSet<UserMarker> UserMarkers { get; set; }

        public DbSet<User> Users { get; set; }
     

        
    }
}

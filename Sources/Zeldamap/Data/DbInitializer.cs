using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zeldassistant.Models;
using Microsoft.EntityFrameworkCore;

namespace zeldassistant.Data
{

    public static class DbInitializer
    {
        public static void Initialize(ZeldaDataContext context)
        {
            context.Database.EnsureCreated();
           
            context.Database.Migrate();
            // Look for any markers.
            if (context.Markers.Any())
            {
                return;   // DB has been seeded
            }
            JsonDataLoader loader = new JsonDataLoader();
            var markers = loader.Load();

            context.Markers.AddRange(markers);
            context.SaveChanges();


        }


        


    }
}



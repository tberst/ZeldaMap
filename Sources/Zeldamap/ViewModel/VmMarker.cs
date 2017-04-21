using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zeldassistant.Models;

namespace zeldassistant.ViewModel
{
    public class VmMarker
    {
        public int Id { get; set; }
         public string Lat { get; set; }
        public String Lng { get; set; }

        public String Description { get; set; }

        public String Name { get; set; }

     
        public MarkerType Type { get; set; }

      
        public Decimal X { get; set; }
        public Decimal Y { get; set; }

     


        public bool IsDeleted { get; set; }

        public int UserId { get; set; }
        public bool IsDone { get; set; }


        public DateTime DoneTime { get; set; }


        public Guid Guid { get; set; }
        public string Color { get; internal set; }
    }
}

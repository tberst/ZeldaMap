using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using zeldassistant.Models;

namespace zeldassistant
{
    public class JsonDataLoader
    {
        
        public List<Marker> Load()
        {
            List<Marker> markers = new List<Marker>();
            using (StreamReader r = File.OpenText(@"AppData\data.json"))
            {
                string json = r.ReadToEnd();
                List<ImportObject> items = JsonConvert.DeserializeObject<List<ImportObject>>(json);

                decimal factor = 2;
                
                foreach (var item in items)
                {
                    item.guid = Guid.NewGuid().ToString();
                    markers.Add(new Marker()
                    {

                        Description = item.description,
                        Name = item.name,
                        Type = (MarkerType)item.markerCategoryId,
                        // X = item.x,
                        // Y = item.y,
                        Lat = (item.y / factor) + (Decimal)24.5,
                        Lng = (item.x / factor) - (Decimal)17,
                        IsDeleted = false,
                        Guid = Guid.NewGuid()
                  

                    });
                }
            } 
            return markers;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zeldassistant.Models
{
    public enum MarkerType
    {
  Point_of_Interest = 1901
, Equipment = 1902
, Weapons = 1903
, Bows_And_Arrows = 1904
, Shields = 1905
, Armor = 1906
, Items = 1910
, Food_Beef = 1911
, Food_Fish = 1912
, Herbs = 1913
, Mushrooms = 1914
, Materials = 1915
, Korok_Seeds = 1916
, Locations = 1920
, Town_House_1 = 1921
, Town_House_2 = 1922
, Sheikah_Tower = 1923
, Shrine_of_Resurrection = 1924
, Shrine_of_Trials = 1925
, Dungeon = 1926
, Temple_of_Time = 1927
, Enemies = 1930
, Enemy_Camp = 1931
, Guardian = 1932
, Boss = 1933
, Memories = 1934
, Side_Quests = 1935
, Cracked_Walls = 1936
, Great_Fairy = 1937
, Stables = 1938
, Custom = 1939
    }

    public class Marker
    {
        public int Id { get; set; }
        public Decimal X { get; set; }
        public Decimal Y { get; set; }

        public Decimal Lat { get; set; }
        public Decimal Lng { get; set; }

        public String Description { get; set; }

        public bool IsDeleted { get; set; }

        public MarkerType Type { get; set; }

        public string Name { get; set; }

     
 
        public Guid Guid { get; set; }

    }

    class UserMarkerComparer : IEqualityComparer<UserMarker>
    {
        public bool Equals(UserMarker x, UserMarker y)
        {
            return x.Guid == y.Guid;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(UserMarker myModel)
        {
            return myModel.Guid.GetHashCode();
        }
    }

    public class UserMarker : Marker
    {
        public int UserId { get; set; }
        public bool IsDone { get; set; }
     

        public DateTime DoneTime { get; set; }

        public UserMarker()
        { }
        public UserMarker(Marker m)
        {
            this.Description = m.Description;
            this.Guid = m.Guid;
            this.IsDeleted = m.IsDeleted;
            this.Lat = m.Lat;
            this.Lng = m.Lng;
            this.Name = m.Name;
            this.Type = m.Type;
            this.X = m.X;
            this.Y = m.Y;
            this.Id = m.Id;
           

        }

    }

    public class ImportObject
    {
        public string id { get; set; }
        public string mapId { get; set; }
        public string submapId { get; set; }
        public object overlayId { get; set; }
        public int markerCategoryId { get; set; }
        public string markerCategoryTypeId { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Decimal x { get; set; }
        public Decimal y { get; set; }
        public string jumpMakerId { get; set; }
        public string tabId { get; set; }
        public string tabTitle { get; set; }
        public string tabText { get; set; }
        public string tabUserId { get; set; }
        public string tabUserName { get; set; }
        public string globalMarker { get; set; }
        public string visible { get; set; }

        public string guid { get; set; }
    }

}

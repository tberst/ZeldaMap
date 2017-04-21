using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zeldassistant.Models;

namespace zeldassistant.Code
{
    public class ColorHelper
    {
        //Primary Color:
        public static string pc1 = "B40000";
        public static string pc2 = "872222";
        public static string pc3 = "750000";
        public static string pc4 = "DA3636";
        public static string pc5 = "DA6262";

        //				
        //Secondary Color A:
        public static string sca1 = "B45200";
        public static string sca2 = "875022";
        public static string sca3 = "753500";
        public static string sca4 = "DA8036";
        public static string sca5 = "DA9862";
        //				
        //Secondary Color B:
        public static string scb1 = "006C6C";
        public static string scb2 = "145151";
        public static string scb3 = "004646";
        public static string scb4 = "2DB6B6";
        public static string scb5 = "52B6B6";
        //				
        //Complementary Color:
        public static string cc1 = "009000";
        public static string cc2 = "1B6C1B";
        public static string cc3 = "005E00";
        public static string cc4 = "32C832";
        public static string cc5 = "5AC85A";
        //		

        public string Get(MarkerType type)
        {
            string result = "black";
            switch (type)
            {
                case MarkerType.Point_of_Interest:
                    result = cc5;
                    break;
                case MarkerType.Memories:
                    result = pc2;
                    break;
                case MarkerType.Side_Quests:
                    result = pc3;
                    break;
                case MarkerType.Cracked_Walls:
                    result = pc4;
                    break;
                case MarkerType.Equipment:
                    result = cc5;
                    break;
                case MarkerType.Weapons:
                    result = sca1;
                    break;
                case MarkerType.Bows_And_Arrows:
                    result = sca2;
                    break;
                case MarkerType.Shields:
                    result = sca3;
                    break;
                case MarkerType.Armor:
                    result = sca4;
                    break;
                case MarkerType.Items:
                    result = sca5;
                    break;
                case MarkerType.Food_Beef:
                    result = scb1;
                    break;
                case MarkerType.Food_Fish:
                    result = scb2;
                    break;
                case MarkerType.Herbs:
                    result = scb3;
                    break;
                case MarkerType.Mushrooms:
                    result = scb4;
                    break;
                case MarkerType.Materials:
                    result = scb5;
                    break;
                case MarkerType.Korok_Seeds:
                    result = cc1;
                    break;
                case MarkerType.Locations:
                    result = cc2;
                    break;
                case MarkerType.Town_House_1:
                    result = cc3;
                    break;
                case MarkerType.Town_House_2:
                    result = cc3;
                    break;
                case MarkerType.Sheikah_Tower:
                    result = cc4;
                    break;
                case MarkerType.Shrine_of_Resurrection:
                    result = pc5;
                    break;
                case MarkerType.Shrine_of_Trials:
                    result = pc1;
                    break;
                case MarkerType.Dungeon:
                    result = pc1;
                    break;
                case MarkerType.Temple_of_Time:
                    result = pc1;
                    break;
                case MarkerType.Enemies:
                    result = pc1;
                    break;
                case MarkerType.Enemy_Camp:
                    result = pc1;
                    break;
                case MarkerType.Guardian:
                    result = pc1;
                    break;
                case MarkerType.Boss:
                    result = pc1;
                    break;
             
             
             
                case MarkerType.Great_Fairy:
                    result = pc1;
                    break;
                case MarkerType.Stables:
                    result = pc1;
                    break;
                case MarkerType.Custom:
                    break;

            }
            return "black";
        }
    }
}

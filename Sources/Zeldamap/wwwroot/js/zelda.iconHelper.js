var _globalCtx = _globalCtx = _globalCtx || {};

_globalCtx.iconHelper = {
    getHtml: function (dm) {
        var type = dm.type;

        var pc1 = "B40000";
        var pc2 = "872222";
        var pc3 = "750000";
        var pc4 = "DA3636";
        var pc5 = "DA6262";

        //				
        //Secondary Color A:
        var sca1 = "B45200";
        var sca2 = "875022";
        var sca3 = "753500";
        var sca4 = "DA8036";
        var sca5 = "DA9862";
        //				
        //Secondary Color B:
        var scb1 = "006C6C";
        var scb2 = "145151";
        var scb3 = "004646";
        var scb4 = "2DB6B6";
        var scb5 = "52B6B6";
        //				
        //Complementary Color:
        var cc1 = "009000";
        var cc2 = "1B6C1B";
        var cc3 = "005E00";
        var cc4 = "32C832";
        var cc5 = "5AC85A";

        color = "black";
        icon = "BotW_Dungeon";
        switch (type) {


            case 1939: color = pc1;     icon="BotW_Points-of-Interest";break;
            case 1901: color = pc2;     icon="BotW_Locations";break;
            case 1902: color = pc3;     icon="BotW_Equipment";break;
            case 1903: color = pc4;     icon="BotW_Equipment";break;
            case 1904: color = pc5;     icon="BotW_Bow-n-Arrows";break;
            case 1905: color = sca1;    icon="BotW_Shields"; break;
            case 1906: color = sca2;    icon="BotW_Armor"; break;
            case 1910: color = sca3;    icon="BotW_Items"; break;
            case 1911: color = sca4;    icon="BotW_Beef"; break;
            case 1912: color = sca5;    icon="BotW_Fish"; break;
            case 1913: color = scb1;    icon="BotW_Herbs"; break;
            case 1914: color = scb2;    icon="BotW_Mushrooms"; break;
            case 1915: color = scb3;    icon="BotW_Food-n-Materials"; break;
            case 1916: color = scb4;    icon="BotW_Korok-Seeds"; break;
            case 1920: color = scb5;    icon="BotW_Locations"; break;
            case 1921: color = cc1;     icon="BotW_Locations";break;
            case 1922: color = cc2;     icon="BotW_Locations";break;
            case 1923: color = cc3;     icon="BotW_Sheikah-Tower";break;
            case 1924: color = cc4;     icon="BotW_Shrine-Quest";break;
            case 1925: color = cc5;     icon="BotW_Shrines-of-Trials";break;
            case 1926: color = cc1;     icon="BotW_Dungeon";break;
            case 1927: color = cc2;     icon="BotW_Points-of-Interest";break;
            case 1930: color = cc3;     icon="BotW_Enemies";break;
            case 1931: color = cc4;     icon="BotW_Enemy-Camp";break;
            case 1932: color = cc5;     icon="BotW_Guardian";break;
            case 1933: color = pc1;     icon="BotW_Boss";break;
            case 1934: color = pc2;     icon="BotW_Memories";break;
            case 1935: color = pc3;     icon="BotW_Points-of-Interest";break;
            case 1936: color = pc4;     icon="BotW_Points-of-Interest";break;
            case 1937: color = pc5;     icon="BotW_Great-Fairy";break;
            case 1938: color = sca1;    icon="BotW_Locations"; break;
        }

        var retval = '<div class="circle circleMap" style="background-color: #' + color + '; border-color: ' + color + '"><span style="font-size: 16px;" class="icon-' + icon + '"></span>';
        if (dm.isDone) {
            retval += '<span class="icon-checkmark completeMarker"></span>';
        }
        retval += '</div > ';

        return retval
    }

}
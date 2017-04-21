var _globalCtx = _globalCtx = _globalCtx || {};

_globalCtx.defaultFilter = ["Done", "NotDone", 1939, 1923, 1925];

function VmMainMenu() {

    var availableFilters = [
        { id: "Done", value: "Done" },
        { id: "NotDone", value: "Not Done" },
        { id: 1939, value: "My markers" },
        { id: 1901, value: "Point_of_Interest " },
        //{ id: 1902, value: "Equipment " },
        //{ id: 1903, value: "Weapons " },
        //{ id: 1904, value: "Bows_And_Arrows " },
        //{ id: 1905, value: "Shields " },
        //{ id: 1906, value: "Armor " },
        //{ id: 1910, value: "Items " },
        //   { id: 1911, value: "Food_Beef " },
        // { id: 1912, value: "Food_Fish " },
        //   { id: 1913, value: "Herbs " },
        //    { id: 1914, value: "Mushrooms " },
        //{ id: 1915, value: "Materials " },
        { id: 1916, value: "Korok_Seeds " },
        //{ id: 1920, value: "Locations " },
        { id: 1921, value: "Town_House_1 " },
        //{ id: 1922, value: "Town_House_2 " },
        { id: 1923, value: "Sheikah_Tower " },
        { id: 1924, value: "Shrine_of_Resurrection" },
        { id: 1925, value: "Shrine_of_Trials " },
        { id: 1926, value: "Dungeon " },
        { id: 1927, value: "Temple_of_Time " },
        //  { id: 1930, value: "Enemies " },
        //   { id: 1931, value: "Enemy_Camp " },
        { id: 1932, value: "Guardian " },
        { id: 1933, value: "Boss " },
        { id: 1934, value: "Memories " },
        { id: 1935, value: "Side_Quests " },
        { id: 1936, value: "Cracked_Walls " },
        { id: 1937, value: "Great_Fairy " },
        { id: 1938, value: "Stables " }
    ];

    var storage = new _globalCtx.Storage();

    var self = this;
    self.checkboxlist = ko.observableArray(availableFilters);


    var savedFilters = storage.loadFilters();
    if (savedFilters)
    {
        self.selectedFilters = ko.observableArray(savedFilters); 
    }
    else
    {
        self.selectedFilters = ko.observableArray(_globalCtx.defaultFilter); 
    }
    self.selectedFilters.subscribe(function (newValue) {
        if (typeof (_globalCtx.ZeldaMap) != "undefined") {
            storage.save(newValue);
            _globalCtx.ZeldaMap.filterMap(newValue);
        }
    });
    
  
}
var domFilters = document.getElementById("filters");
if (domFilters)
{

    ko.applyBindings(new VmMainMenu(),domFilters );
}

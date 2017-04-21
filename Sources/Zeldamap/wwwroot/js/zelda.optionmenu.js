var _globalCtx = _globalCtx = _globalCtx || {};

_globalCtx.defaultOptions = ["Group", "Color"];

function VmOptionMenu() {
    var availableOptions = [{ id: "Group", value: "Group" }, { id: "Color", value: "Color/B&W" }];
    var storage = new _globalCtx.Storage();

    var self = this;
    self.optionlist = ko.observableArray(availableOptions);


    var savedOptions = storage.loadOptions();
    if (savedOptions) {
        self.selectedOptions = ko.observableArray(savedOptions);
    }
    else {
        self.selectedOptions = ko.observableArray(_globalCtx.defaultOptions);
    }
    self.selectedOptions.subscribe(function (newValue) {
        if (typeof (_globalCtx.ZeldaMap) != "undefined") {
            storage.save(null, newValue);

            _globalCtx.ZeldaMap.setGrouping($.inArray("Group", newValue) != -1);
            _globalCtx.ZeldaMap.setColor($.inArray("Color", newValue) != -1)
        }
    });



}
var domFilters = document.getElementById("options");
if (domFilters) {
    ko.applyBindings(new VmOptionMenu(), domFilters);
}

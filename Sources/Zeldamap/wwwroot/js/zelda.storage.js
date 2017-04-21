var _globalCtx = _globalCtx = _globalCtx || {};


_globalCtx.Storage = function () {
    var self = this;
    var FILTERS = "filters";
    var OPTIONS = "options";
    var LOCATION = "location";
    var ZOOM = "zoom";

    var load = function (key)
    {
        var result = localStorage.getItem(key);
        if (typeof (result) != "undefined") {
            result = JSON.parse(result);
        }
        return result;
    }

    var save = function (key, value)
    {
        if (typeof (value) != "undefined") {
            localStorage.setItem(key, JSON.stringify(value));
        }
    }

    self.storeLocation = function (position, zoom)
    {
        save(LOCATION, position);
        save(ZOOM, zoom);
    }

    self.loadLocation = function (position, zoom)
    {
        var zoom = load(ZOOM);
        var location = load(LOCATION);
        return { zoom: zoom, location: location };
    }

    self.loadFilters = function () {
        return load(FILTERS);    
    }

    self.loadOptions = function () {
        return load(OPTIONS);    
    }

    self.save = function (filters, options) {
        save(FILTERS, filters);
        save(OPTIONS, options);

    }
}
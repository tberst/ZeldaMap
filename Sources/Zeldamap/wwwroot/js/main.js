var _globalCtx = _globalCtx = _globalCtx || {};
_globalCtx.map = {};





var ZeldaMap = function () {
    var self = this;
    var _cache = new _globalCtx.MarkerCache();
    var _storage = new _globalCtx.Storage();
    self.map = {};
    self.markers = L.markerClusterGroup();
    self.ungroupedMarkers = L.featureGroup();
    var options = _storage.loadOptions();
    if (typeof (options) != "undefined" && options) {
        if (options.length > 0) {
            self.isGrouped = options[0] == "Group";
        }
        else {
            self.isGrouped = false;
        }
    }
    else {
        self.isGrouped = true;
    }


    // PUBLIC METHODS
    //Initialize the map object
    self.initialize = function () {
        var position = _storage.loadLocation();
        if (!position.zoom)
        {
            position.zoom = 3;
        }
        if (!position.location)
        {
            position.location = [-100, 180];
        }
        self.map = L.map('mapid', {
            maxZoom: 8,
            minZoom: 2,
            crs: L.CRS.Simple,
            contextmenu: true,
            contextmenuWidth: 140,
            contextmenuItems: _contextMenuEntries,
            tap: true
        }).setView(position.location, position.zoom );

        var southWest = self.map.unproject([0, 20224], self.map.getMaxZoom());
        var northEast = self.map.unproject([24064, 0], self.map.getMaxZoom());

        self.map.doubleClickZoom.disable();
        self.map.setMaxBounds(new L.LatLngBounds(southWest, northEast));

        var options = _storage.loadOptions();
        var url = '/images/color/{z}/{x}_{y}.png';

        if (options && options.indexOf("Color") < 0)
        {
            $("#mapid").css("background-color", "white");
            url = '/images/bw/{z}/{x}_{y}.png'
        }
        else
        {
            $("#mapid").css("background-color", "black");
        }
        self.tileLayer = L.tileLayer(url, {
            attribution: '...'
        }).addTo(self.map);
        if (_globalCtx.isAuthenticated) {
            self.map.on('dblclick', addMarker);


        }
        else {
            self.map.on('dblclick', function () { alert("please login/register to set markers"); });
        }
        loadData();
        window.setTimeout(saveLocation, 10000);

    }
    function saveLocation()
    {
        var position = self.map.getCenter();
        var zoom = self.map.getZoom()
        _storage.storeLocation(position, zoom);
    }
    self.updateMarker = function (dm) {
        var leafletMarker = _cache.getLeafletMarker(dm);
        leafletMarker.options.customProp.dm = dm;

        var html = _globalCtx.iconHelper.getHtml(dm);
        var myIcon = L.divIcon({ className: 'map-icon-svg', html: html });
        leafletMarker.setIcon(myIcon);

        if (dm.description || dm.name) {
            leafletMarker.bindPopup(dm.name).on('click', onMarkerClick);

        }
        //  console.log(leafletMarker);
    }
    //reset and re-display the markers (based on filters)
    self.filterMap = function (filterOptions) {
        if (self.markers) {
            self.markers.clearLayers();
            self.ungroupedMarkers.clearLayers();
        }

        for (var i = 0; i < filterOptions.length; i++) {
            if (filterOptions[i] == "Done") {
                var includeDone = true;
            }
            if (filterOptions[i] == "NotDone") {
                var includeUndone = true;
            }
        }

        var filteredArray = _cache.cachedData.filter(function (elt) {
            for (var i = 0; i < filterOptions.length; i++) {
                if (elt.type == filterOptions[i]) {
                    return (elt.isDone == includeDone) || (!elt.isDone == includeUndone);
                }
            }
        });
        $.each(filteredArray, function (index, elt) {
            addDbMarkerToMap(elt);

        });
        if (self.isGrouped) {
            self.map.addLayer(self.markers);
        }
        else {
            self.map.addLayer(self.ungroupedMarkers);
        }
    }

    self.setColor = function (isColor)
    {
        if (isColor)
        {
            self.tileLayer.setUrl('/images/color/{z}/{x}_{y}.png');
            $("#mapid").css("background-color", "black");

        }
        else
        {
            self.tileLayer.setUrl('/images/bw/{z}/{x}_{y}.png');
            $("#mapid").css("background-color", "white");
        }
    }
    self.setGrouping = function (isGrouping) {
        self.isGrouped = isGrouping;
        if (isGrouping) {
            self.map.removeLayer(self.ungroupedMarkers);
            self.map.addLayer(self.markers);
        }
        else {
            self.map.removeLayer(self.markers);
            self.map.addLayer(self.ungroupedMarkers);
        }

    }
    //ENDOF PUBLIC METHODS



    // REGION BACKEND CALLS

    function addMarker(e) {
        var payload = { lat: e.latlng.lat, lng: e.latlng.lng, type: constants.CUSTOM_MARKER, name: "my marker" };

        $.post("/marker/set", payload, function (result) {
            // Add marker to map at click location; add popup window
            addDbMarkerToMap(result);

            _cache.add(result);
        });
    }


    function deleteMarker(marker) {
        var id = marker.options.customProp.Id;
        $.post("/marker/Delete", { id: id }, function (data) {
            console.log("successfully deleted");
            self.markers.removeLayer(marker);
            self.ungroupedMarkers.removeLayer(marker);
            _cache.remove(id);
        });
    }
    function doneMarker(marker) {
        var id = marker.options.customProp.Id;

        $.post("/marker/done", { id: id }, function (data) {
            console.log("successfully marked as done");
            var dm = _cache.setDoneStatus(id, true);

            var html = _globalCtx.iconHelper.getHtml(dm);
            var myIcon = L.divIcon({ className: 'map-icon-svg', html: html });

            marker.setIcon(myIcon);
            marker.replaceContextMenuItem({
                text: 'Mark as not done',
                index: 0,
                callback: function (data) { undoneMarker(data.relatedTarget); }
            });
        });
    }
    function undoneMarker(marker) {
        var id = marker.options.customProp.Id;

        $.post("/marker/undone", { id: id }, function (data) {
            console.log("successfully marked as not done");
            var dm = _cache.setDoneStatus(id, false);

            var html = _globalCtx.iconHelper.getHtml(dm);
            var myIcon = L.divIcon({ className: 'map-icon-svg', html: html });

            marker.setIcon(myIcon);
            marker.replaceContextMenuItem({
                text: 'Mark as done',
                index: 0,
                callback: function (data) { doneMarker(data.relatedTarget); }
            });
        });
    }
    function loadData() {
        $.get("/marker", function (result) {
            $.each(result.data, function (idx, elt) {

                _cache.add(elt);
            });
            self.filterMap(_globalCtx.defaultFilter);
        });
    }
    //ENDOF BACKEND CALLS

    function addDbMarkerToMap(dm) {
        dm.isUserDefined = (dm.type == constants.CUSTOM_MARKER);
        var ctxMenu = getMarkerCtxMenu(dm);
        var coord = L.latLng({ lat: dm.lat, lng: dm.lng });
        var html = _globalCtx.iconHelper.getHtml(dm);

        var myIcon = L.divIcon({ className: 'map-icon-svg', html: html });
        ctxMenu.icon = myIcon;
        var marker = L.marker(coord, ctxMenu);
        if (dm.description || dm.name) {
            marker.bindPopup(dm.name).on('click', onMarkerClick);

        }
        marker.on('contextmenu', function (data) { console.log(data); });
        addMarkerToMap(marker);
    }

    function addMarkerToMap(marker) {

        marker.on('click', onMarkerClick);
        self.markers.addLayer(marker);
        self.ungroupedMarkers.addLayer(marker);
        _cache.update(marker);
    }
    function onMarkerClick(evt) {
        var $selectedPanel = $("#selectedPanel");
        if ($selectedPanel.is(":visible")) {//do nothing
        } else {
            $selectedPanel.collapse('show');
        }

        _globalCtx.selectedMarkerVm.setActiveMarker(evt.target.options.customProp.dm);
        return false;
    }

    function getMarkerCtxMenu(dm) {
        var markerCtxMenu = {
            customProp: {
                Id: dm.id,
                dm: dm,
                Type: dm.type
            },



        };

        if (_globalCtx.isAuthenticated) {
            markerCtxMenu.contextmenu = true;
            markerCtxMenu.contextmenuItems = [];

            if (dm.isDone) {
                markerCtxMenu.contextmenuItems.push({
                    text: 'Mark as not done',
                    index: 0,
                    callback: function (data) { undoneMarker(data.relatedTarget); }
                });
            }
            else {
                markerCtxMenu.contextmenuItems.push({
                    text: 'Mark as done',
                    index: 0,
                    callback: function (data) { doneMarker(data.relatedTarget); }
                });
            }
            if (dm.isUserDefined) {
                markerCtxMenu.contextmenuItems.push({
                    text: 'Delete',
                    index: 1,
                    callback: function (data) { deleteMarker(data.relatedTarget); }
                });
            }
        }
        return markerCtxMenu;
    }


    self.initialize();
}

$(function () {

    _globalCtx.ZeldaMap = new ZeldaMap();

});
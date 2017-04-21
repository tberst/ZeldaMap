var _globalCtx = _globalCtx = _globalCtx || {};

_globalCtx.MarkerCache = function () {
    var self = this;
    self.cachedData = [];

    self.add = function (dm) {
        self.cachedData.push(dm);
    }
    self.update = function (leafletMarker) {
        var dm = leafletMarker.options.customProp.dm

        var markers = $.grep(self.cachedData, function (elt) { return elt.id == dm.id });
        if (markers.length > 0) {
            markers[0].leafletMarker = leafletMarker;
        }
    }

    self.getLeafletMarker = function (dm) {
        var markers = $.grep(self.cachedData, function (elt) { return elt.id == dm.id });
        if (markers.length > 0) {
            return markers[0].leafletMarker;
        }
    }

    self.remove = function (markerId) {
        self.cachedData = self.cachedData.filter(function (el) {
            return el.id !== markerId;
        });

    }

    self.setDoneStatus = function (markerId, doneStatus) {
        var markers = $.grep(self.cachedData, function (elt) { return elt.id == markerId });
        markers[0].isDone = doneStatus;
        return markers[0];
    }

}
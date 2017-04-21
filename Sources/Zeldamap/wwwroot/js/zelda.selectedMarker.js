var _globalCtx = _globalCtx = _globalCtx || {};

var SelectedMarkerVm = function ()
{

    var self = this;
    self.chkDone = ko.observable(true);
    self.name = ko.observable();
    self.description = ko.observable();
    self.guid = ko.observable("");
    self.userId = ko.observable(0);
    self.id = ko.observable(0);

    this.isSaveEnabled = ko.computed(function () {
        return self.id()!= 0;
    }, this);

    self.btnSaveClicked = function () {
        
        var payload = {
            id: self.id(),
            description: self.description(),
            isDone: self.chkDone(),
            name: self.name(),
            userId : self.userId()
        }
        $.post("/marker/save", payload, function (result) {
            _globalCtx.ZeldaMap.updateMarker(result);
            
        });

    }
   
    self.setActiveMarker = function (dm)
    {
        self.name(dm.name);
        self.description(dm.description);
        self.guid(dm.guid);
        self.chkDone(dm.isDone);
        self.userId(dm.userId);
        self.id(dm.id);

    }
}
_globalCtx.selectedMarkerVm = new SelectedMarkerVm();
ko.applyBindings(_globalCtx.selectedMarkerVm, document.getElementById("selectedMarker"));
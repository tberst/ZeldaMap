

var _contextMenuEntries = [
    //{
    //text: 'Show coordinates',
    //callback: showCoordinates
    //},
    {
    text: 'Center map here',
    callback: centerMap,
   
    },
//    '-', {
//    text: 'Zoom in',
//    icon: 'images/zoom-in.png',
//    callback: zoomIn
//}, {
//    text: 'Zoom out',
//    icon: 'images/zoom-out.png',
//    callback: zoomOut
//    }
];


function showCoordinates(e) {
    alert(e.latlng);
}
function centerMap(e) {
    _globalCtx.ZeldaMap.map.panTo(e.latlng);
}
function zoomIn(e) {
   
    _globalCtx.map.zoomIn();
}
function zoomOut(e) {
    _globalCtx.map.zoomOut();
}
var _globalCtx = _globalCtx = _globalCtx || {};
_globalCtx.Tour = function () {
    var template = "<div class='popover tour'> \
            <div class='arrow' ></div>\
            <h3 class='popover-title'></h3>\
            <div class='popover-content'></div>\
          <div class='popover-navigation'><input type='checkbox' id='chkEndTour' checked> show the tutorial next time\</div>\
            <div class='popover-navigation'>\
                <button class='btn btn-default' data-role='prev'>« Prev</button>\
                <span data-role='separator'>|</span>\
                <button class='btn btn-default' data-role='next'>Next »</button>\
         <button class='btn btn-default' data-role='end'>End tour</button>\
            </div>\
           \
        </div > ";

    var onEnd = function () {
        var result = $("#chkEndTour").prop("checked");
        console.log(result);
        if (result) {
            window.localStorage.removeItem(name + "_current_step");
            window.localStorage.removeItem(name + "_end");
        }
    };

    this.startAnonymousTour = function () {
        var name = "anonymous";
        // Instance the tour
        var tour = new Tour({
            name: name,
            template: template,
            steps: [
                {
                    element: "#brandLogo",
                    title: "Welcome",
                    content: "Welcome to ZeldaMap, an interactive map of Hyrule for Zelda Breath of the Wild"
                },
                {
                    element: "#btnRegister",
                    title: "Register",
                    content: "Register an account and gain access to some cool features (custom markers, filtering, editing, ....)"
                }
               
            ],
            onEnd: onEnd
        });

        // Initialize the tour
        tour.init();

        // Start the tour
        tour.start();
    }

    this.startRegisteredTour = function () {
        var name = "registered";
        // Instance the tour
        var tour = new Tour({
            name: name,
            template: template,
            steps: [
                {
                    element: "#mapid",
                    title: "Map",
                    content: "double click on the map to place your own markers ",
                    smartPlacement: false,
                    placement: "top"
                },
                {
                    element: "#btnFilter",
                    title: "Filter",
                    content: "Click here to select the markers you want to display on the map"
                },
                {
                    element: "#btnSelectedMarker",
                    title: "Edit markers",
                    content: "click on a marker to select it. You'll be able to rename it, change the description or mark it as done "
                },
                ,
                {
                    element: "#btnOption",
                    title: "Various options",
                    content: "Things and stuff... more to come.. have fun ! "
                },
                
            ],
            onEnd: onEnd
        });

        // Initialize the tour
        tour.init();

        // Start the tour
        tour.start();
    }
    return this;



};



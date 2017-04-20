// site.js
//immediately invoked function(IIF or Anonymous Function) is pattern used to keep from using the global scope in javascript
(function () {
    
    //var ele = $("#username");
    //ele.text("Taylor Shuff");

    //var main = $("#main");
    //main.on("mouseenter",function () {
    //    main.style = "background-color: #888;";
    //});
    //main.on("mouseleave", function () {
    //    //goes back to css style
    //    main.style = "";
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    var me = $(this);
    //    alert(me.text());
    //});

    //a jquery wrapped set
    var $sidebarAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });
    




})();
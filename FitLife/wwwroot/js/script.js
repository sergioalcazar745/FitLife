$(document).ready(function () {

    scroll();

    //if (window.matchMedia("(max-width: 992px)").matches) {
    //    $("#landing").css("height", "100%")
    //    $("#landing").css("padding-top", "70px")
    //} else {
    //    $("#landing").css("height", "100vh")
    //}

    $(document).scroll(function () {
        scroll();
    })

    $("#theme-toggle").click(function(){
        scroll();
    });
});

function scroll() {
    var scroll = $(window).scrollTop();
    if (scroll == 0) {
        var result = $(document.documentElement).hasClass("dark");
        if(result == false){       
            $("#titulo").removeClass("text-gray-900").addClass("text-white")
        }
        $("#nav").removeClass("dark:bg-gray-800 bg-gray-200").addClass("bg-transparent");
    } else {
        var result = $(document.documentElement).hasClass("dark");
        if(result == false){         
            $("#titulo").removeClass("text-white").addClass("text-gray-900")
        }else{
            $("#titulo").removeClass("text-gray-900").addClass("text-white")
        }
        $("#nav").removeClass("bg-transparent").addClass("dark:bg-gray-800 bg-gray-200");
    }
    //border-b border-gray-200 dark:border-gray-600
}
$(document).ready(function () {

    $("ul#menu li").hover(function () {

        $(this).parent().find("ul#sousMenu").slideDown('fast').show();

        $(this).parent().hover(function () {
        }, function () {
            $(this).parent().find("ul#sousMenu").slideUp('slow');
        });

    }).hover(function () {
        $(this).addClass("subhover");
    }, function () {
        $(this).removeClass("subhover");
    });

});


$('.dropright')
    .hover(function () {
        $(this).children('.drophover-right:first').show();
    }, function () {
        $(this).children('.drophover-right:first').hide();
    });
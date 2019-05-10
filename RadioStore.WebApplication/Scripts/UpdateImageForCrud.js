
$("#imgSrcFromCrud").on("input propertychange paste", function () {
    $('#imgForCrud').attr("src", $(this).val());
});
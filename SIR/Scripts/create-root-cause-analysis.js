$(document).ready(function () {
    var twhyEditors = $("#create-why").children(".twhy-editor");

    for (var i = 0; i < twhyEditors.length; i++) {
        var editor = twhyEditors[i];
        var margin = i * 20;
        editor.style.marginLeft = margin.toString() + "px";

        var whyText = editor.getElementsByTagName("textarea")[0].value;
        var hide = i > 0 && !(whyText);
        if (hide) {
            editor.style.display = "none";
        }
        //else {
        //    // Hide buttons in all previous Whys
        //}
    }

    $(".create-rca").hide();
    $("#choose-incident").show();
});

$(".btn-next").click(function () {
    var currStep = $(this).parents(".create-rca").first();
    currStep.hide();

    var nextStep = currStep.next();
    nextStep.show();
});

$(".btn-back").click(function () {
    var currStep = $(this).parents(".create-rca").first();
    currStep.hide();

    var prevStep = currStep.prev();
    prevStep.show();
});

$(".btn-add-twhy").click(function () {
    // Show next Why
    var currWhy = $(this).parents(".twhy-editor").first();
    var nextWhy = currWhy.next(".twhy-editor");
    nextWhy.show();

    // Hide buttons for this Why
    var currWhyButtons = currWhy.children(".twhy-buttons").first();
    currWhyButtons.hide();
});

$(".btn-delete-twhy").click(function () {
    // Clear text
    var currWhy = $(this).parents(".twhy-editor").first();
    currWhy.children("textarea").val("");

    // If there is a previous Why, show its buttons and hide this Why
    var prevWhy = currWhy.prev(".twhy-editor");
    if (prevWhy.length > 0) {
        prevWhy.children(".twhy-buttons").show();
        currWhy.hide();
    }
});
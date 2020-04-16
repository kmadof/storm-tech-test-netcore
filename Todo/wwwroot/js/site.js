// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("hideDoneItems").addEventListener("change", function () {
    var style = "block";
    if (this.checked) {
        style = "none";
    }

    var doneElements = document.getElementsByClassName("is-done");
    for (i = 0; i < doneElements.length; i++) {
        doneElements[i].style.display = style;
    }
});


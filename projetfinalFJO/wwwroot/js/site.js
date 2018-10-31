// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Write your JavaScript code.
$(function () {
    var formModal = fndefinirModal();
    $("#createRoleModal").on("click", function () { $(formModal).dialog("open"); });
    $("#btSubmit").on("click", function () { $(formModal).dialog("close"); });
    $("#btCancel").on("click", function () { $(formModal).dialog("close"); });
});

function fndefinirModal() {
    var formModal = $("#divaddmodal").dialog({
        autoOpen: false,
        height: 400,
        width: 350,
        modal: true,


        close: function () {
            formModal.dialog("close");

        }
    });
    return formModal;
}

function onComplete() {
    var formModal = fndefinirModal();
    $("#createRoleModal").on("click", function () { $(formModal).dialog("open"); });
    $('form input[type="text"]').prop("value", "");
}
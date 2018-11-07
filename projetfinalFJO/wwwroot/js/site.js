﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Write your JavaScript code.
/*
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
}*/

function fnAddAjax() {
    var url = "/Competences/Create";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        ObligatoireCégep: $("#ObligatoireC_gep").val(),
        Description: $("#Description").val(),
        ContextRealisation: $("#ContextRealisation").val(),
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },
        success: function (result) {
            alert(status);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}
/*
function fndelete(roleId) {
    $.ajax({
        // data: jQuery.param({ id: roleId }),
        type: "GET",
        url: "/Role/Delete/" + roleId,
        datatype: "text/plain",
        contentType: "application/html; charset=utf-8",
        success: function (result) {
            alert(result);
            $("#listeTable tr[id=\"" + roleId + "\"]").remove();
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}

function fneditGet(roleId) {

    $.ajax({
        // data: jQuery.param({ id: roleId }), // envoyer le paramètre dans le querystring
        type: "GET",
        url: "/Role/Edit/" + roleId, // envoyer le paramètre dans l'uri
        contentType: "application/html; charset=utf-8",
        datatype: "json",
        success: function (result) {
            $("form").prop("title", "Edit");
            formModal.dialog("open");
            $("#Id").val(result.id);
            $("#RoleName").val(result.roleName);
            $("#Description").val(result.description);

        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}
function fneditPost(roleId) {
    var data = {
        id: $("#Id").val(),
        RoleName: $("#RoleName").val(),
        Description: $("#Description").val()
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: "/Role/Edit/",
        contentType: "application/json; charset=utf-8",
        datatype: "text/plain",
        success: function (result, textStatus) {
            alert(textStatus + ":" + result);
            var tr = "<td>" + $("#Id").val() + "</td><td>" + $("#RoleName").val() + "</td><td>" + $("#Description").val() + "</td>" +
                "<td>" +
                "<a class=\"btn btn-info\" onclick=\"fneditGet(" + $("#Id").val() + ")\"> <i class=\"glyphicon glyphicon-pencil\"></i> Edit </a>" +
                " <a class=\"btn btn-danger\" onclick=\"fndelete(" + $("#Id").val() + ")\"><i class=\"glyphicon glyphicon-trash\"></i>Delete</a>" +
                "</td>";
            $("#listeTable tr[id=\"" + $("#Id").val() + "\"]").html(tr);

        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}*/

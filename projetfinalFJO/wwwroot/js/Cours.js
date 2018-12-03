var formModal;
$(function () {
    $("#btSubmit").on("click", function () {
        $(formModal).dialog("close");
        $("form").prop("title") === "Add" ? fnAddAjax() : fneditPost();
    });
    $("#btCancel").on("click", function () { $(formModal).dialog("close"); });

});

//Ajouter un cours
function AjouterCoursAjax() {
    var url = "/Cours/AjouterCours";
    var data = {
        NoCours: $("#NoCours").val(),
        NomCours: $("#NomCours").val(),
        PonderationCours: $("#PonderationCours").val(),
        DepartementCours: $("#DepartementCours").val(),
        TypedeCours: $("#TypedeCours").val(),
        NoProgramme: $("#NoProgramme").val(),
        NomSession: $("#NomSession").val(),
        NomGroupe: $("#NomGroupe").val()
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: url/* https://stackoverflow.com/questions/20011282/redirecttoaction-not-working-after-successful-jquery-ajax-post*/,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },
        success: function (result) {
            alert(status);
            $('#cours').show();
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

//Modifier un cours
function ModifierCoursAjax()
{
    var data = {
        NoCours: $("#NoCours").val(),
        NomCours: $("#NomCours").val(),
        PonderationCours: $("#PonderationCours").val(),
        DepartementCours: $("#DepartementCours").val(),
        TypedeCours: $("#TypedeCours").val(),
        NoProgramme: $("#NoProgramme").val(),
        NomSession: $("#NomSession").val(),
        NomGroupe: $("#NomGroupe").val()
    };
        $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: "/Cours/ModifierCours",
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },
        success: function (result) {
            alert(status);
            $('#cours').show();
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });

}

//Supprimer un cours
function SupprimerCoursAjax(NoCours) {
    $.ajax({
        data: jQuery.param({ id: NoCours }),
        type: "POST",
        url: "/Cours/SupprimerCours",
        datatype: "text/plain",
        contentType: "application/html; charset=utf-8",
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },
        success: function (result) {
            alert(result);
            $("#cours tr[id=\"" + NoCours + "\"]").remove();
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}

function DimensionAjoutAjax(divId) {
    var formModal = $(divId).dialog({
        autoOpen: true,
        close: function () {
            formModal.dialog("close");
        }
    });
}
var formModal;
$(function () {
    //formModal = DimensionAjoutAjax("#AjoutCours");
    //$("#AjouterCours").on("click", function () {
    //    $(formModal).dialog("open");
    //    $("form").prop("title", "Add");
    //});
    $("#btSubmit").on("click", function () {
        $(formModal).dialog("close");
        $("form").prop("title") === "Add" ? fnAddAjax() : fneditPost();
    });
    $("#btCancel").on("click", function () { $(formModal).dialog("close"); });

});

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
            $('#elementcomp').show();
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

function DimensionAjoutAjax(divId) {
    var formModal = $(divId).dialog({
        autoOpen: true,
        //height: 400,
        //width: 700,
        //modal: true,

        close: function () {
            formModal.dialog("close");
        }
    });
}
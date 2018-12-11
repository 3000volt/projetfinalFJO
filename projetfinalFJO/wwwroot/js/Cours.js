var formModal;
$(function () {
    alert("000");
    //Charger le selectlist initialement
    ChagerAjax();
    $("#btSubmit").on("click", function () {
        $(formModal).dialog("close");
        $("form").prop("title") === "Add" ? fnAddAjax() : fneditPost();
    });
    $("#btCancel").on("click", function () { $(formModal).dialog("close"); });
    //Quand le select change, ajuste le selectlist en dessous des groupes
    $("#NomSession").on('change', function () {
        ChagerAjax();
    });
});

//Charger le selectlist du groupe
function ChagerAjax() {
    var url = "/Cours/ChargerGroupe";
    $.ajax({
        data: { NomSession: $("#NomSession").val() },
        type: "POST",
        async: false,
        url: url,
        datatype: "json",
        success: function (data) {
            $("select[id='NomGroupe']").empty();
            var choix = '';
            for (var i = 0; i < data.length; i++) {
                choix += '<option value="' + data[i] + '">' + data[i] + '</option>';
            }
            $("select[id='NomGroupe']").append(choix);
            //https://stackoverflow.com/questions/3446069/populate-dropdown-select-with-array-using-jquery
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}

//Ajouter un cours
function AjouterCoursAjax() {
    var url = "/Cours/AjouterCours";
    var data = {
        NoCours: $("#NoCours").val(),
        NomCours: $("#NomCours").val(),
        PonderationCours: $("#PonderationCours").val(),
        DepartementCours: $("#DepartementCours").val(),
        NoProgramme: $("#NoProgramme").val(),
        NomSession: $("#NomSession").val(),
        NomGroupe: $("#NomGroupe").val()
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: url,//https://stackoverflow.com/questions/20011282/redirecttoaction-not-working-after-successful-jquery-ajax-post*/,
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
//pas fini
function AjouterPrealableAjax() {
    var url = "/Cours/AjouterPrealable";
    var data = {
        Prealable: $("#NomSequence2").val(),
    };
    $.ajax({
        data: JSON.stringify(data),
        //data: $("form").serialize(),
        type: "POST",
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        //contentType : "application/x-www-form-urlencoded; charset=utf-8",
        success: function (result) {
            alert(result);
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

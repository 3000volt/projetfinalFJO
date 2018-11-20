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
        url: url,
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
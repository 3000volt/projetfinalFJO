function SupprimerActualisation(y) {
    $.ajax({
        data: { password: $("#mdp").val(), NumActualisation: y },
        type: "POST",
        url: "/Actualisation/SupprimerActualisation",
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        //beforeSend: function (request) {
        //    request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        //},
        success: function (result) {
            alert(result);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}
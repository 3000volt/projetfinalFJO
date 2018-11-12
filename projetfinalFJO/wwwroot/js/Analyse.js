function Ajustation(item) {
    var a = document.getElementById(item).id;
    num = a.charAt(6);
    AnalyseElementCompetence(num);
}

function AnalyseCompetence() {

    var url = "/AnalyseCompetence/Create";
    var data = {
        NiveauTaxonomique: $("#NiveauTaxonomique").val(),
        Reformulation: $("#Reformulation").val(),
        Context: $("#Context").val(),
        SavoirFaireProgramme: $("#SavoirFaireProgramme").val(),
        SavoirEtreProgramme: $("#SavoirEtreProgramme").val(),
        AdresseCourriel: $("#AdresseCourriel").val(),
        CodeCompetence: $("#CodeCompetence").val(),
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


function AnalyseElementCompetence(i) {

    var url = "/AnalyseElementCompetence/Create";
    var data = {
        NiveauTaxonomique: $("#Formulaire_" + i + " input[id=NiveauTaxonomique]").val(),
        Reformulation: $("#Formulaire_" + i + " input[id=Reformulation]").val(),
        Context: $("#Formulaire_" + i + " input[id=Context]").val(),
        SavoirFaireProgramme: $("#Formulaire_" + i + " input[id=SavoirFaireProgramme]").val(),
        SavoirEtreProgramme: $("#Formulaire_" + i + " input[id=SavoirEtreProgramme]").val(),
        AdresseCourriel: $("#Formulaire_" + i + " input[id=AdresseCourriel]").val(),
        //Obligatoire: true,
        Idelementcomp: $("#Formulaire_" + i + " input[id=Idelementcomp]").val(),
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
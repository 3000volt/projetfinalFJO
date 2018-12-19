var formModal;
$(function () {
    var code = $("#CodeCompetence").val().toString();
    alert("test2");
    $("#Titre").val(AfficherDescription(code));
    //$("#Description").val(AfficherDescription(code));
    //Appeler la fonction au changement de valeur du select
    $("#CodeCompetence").on('change', function () {
        //https://stackoverflow.com/questions/11179406/jquery-get-value-of-select-onchange
        //appeler la function ajax qui va chanegr la valeur de la description
        var code = $("#CodeCompetence").val().toString();
        //alert(code);
        $("#Titre").val(AfficherDescription(code));
    });
});

function Ajustation(item) {
    alert(item);
    var a = document.getElementById(item).id;
    num = a.charAt(6);
    AnalyseElementCompetence(num);
}

function Ajustation2(item) {
    alert(item);
    var a = document.getElementById(item).id;
    num = a.charAt(6);
    AnalyseElementCompetence2(num);
}


function AnalyseElementCompetence(i) {
    var url = "/AnalyseElementCompetence/Create";
    var data = {
        NiveauTaxonomique: $("#Formulaire_" + i + " select[id=NiveauTaxonomique]").val(),
        Reformulation: $("#Formulaire_" + i + " textarea[id=Reformulation]").val(),
        Context: $("#Formulaire_" + i + " textarea[id=Context]").val(),
        SavoirFaireProgramme: $("#Formulaire_" + i + " textarea[id=SavoirFaireProgramme]").val(),
        SavoirEtreProgramme: $("#Formulaire_" + i + " textarea[id=SavoirEtreProgramme]").val(),
        ElementCompétence: $("#Formulaire_" + i + " #ElementComp_tence").val(),
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (request) {
            //alert(AdresseCourriel);
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },
        success: function (result) {
            alert("Analyse de l'élément de compétence ajoutée avec succès!");
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}


function AnalyseElementCompetence2(i) {
    var url = "/AnalyseElementCompetence/Create";
    var data = {
        NiveauTaxonomique: $("#Formulaire_" + i + " select[id=NiveauTaxonomique]").val(),
        Reformulation: $("#Formulaire_" + i + " textarea[id=Reformulation]").val(),
        Context: $("#Formulaire_" + i + " textarea[id=Context]").val(),
        SavoirFaireProgramme: $("#Formulaire_" + i + " textarea[id=SavoirFaireProgramme]").val(),
        SavoirEtreProgramme: $("#Formulaire_" + i + " textarea[id=SavoirEtreProgramme]").val(),
        ElementCompétence: $("#Formulaire_" + i + " #ElementComp_tence option:selected").text(),
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (request) {
            //alert(AdresseCourriel);
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },
        success: function (result) {
            alert("Analyse de l'élément de compétence ajoutée avec succès!");
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

function AjouterFamilleAjax() {
    //Voir si le champs a été remplis 
    if ($("#NomFamille2").val() != "") {
        var url = "/AnalyseCompetence/AjouterFamille";
        var data = {
            NomFamille: $("#NomFamille2").val(),
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
                alert("Famille ajoutée avec succès!");
            },
            error: function (xhr, status) { alert("erreur:" + status); }
        });
    }
    else {
        alert("Vous devez remplir le champs pour ajouter une famille!");
    }
}

function AjouterSequenceAjax() {
    //Voir si le champs a été remplis
    if ($("#NomSequence2").val() != "") {
        var url = "/AnalyseCompetence/AjouterSequence";
        var data = {
            NomSequence: $("#NomSequence2").val(),
        };
        $.ajax({
            data: JSON.stringify(data),
            type: "POST",
            url: url,
            datatype: "text/plain",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                alert("Séquence ajoutée avec succès!");
            },
            error: function (xhr, status) { alert("erreur:" + status); }
        });
    }
    else {
        alert("Vous devez remplir le champs pour ajouter une séquence!");
    }
}

function DimensionAjoutAjax(divId) {
    var formModal = $(divId).dialog({
        autoOpen: false,
        height: 400,
        width: 350,
        modal: true,

        close: function () {
            formModal.dialog("close");

        }
    });
    return formModal;
} var formModal;

function AssocierFamilleAjax() {
    if ($("#NomFamille option:selected").text() != "") {
        var url = "/AnalyseCompetence/AssocierFamille";
        var data = {
            CodeCompetence: $("#CodeCompetence").val(),
            NomFamille: $("#NomFamille").val()
        };
        $.ajax({
            data: JSON.stringify(data),
            type: "POST",
            url: url,
            datatype: "text/plain",
            contentType: "application/json; charset=utf-8",

            success: function (result) {
                alert(result);
            },
            error: function (xhr, status) { alert("erreur:" + status); }
        });
        return false;
    }
    else {
        alert("Créez des familles avant d'associer!");
    }
}

function AssocierSequencejax() {
    if ($("#NomSequence option:selected").text() != "") {
        var url = "/AnalyseCompetence/AssocierSequence";
        var data = {
            CodeCompetence: $("#CodeCompetence").val(),
            NomSequence: $("#NomSequence").val()
        };
        $.ajax({
            data: JSON.stringify(data),
            type: "POST",
            url: url,
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
    else {
        alert("Créez des séquences avant d'associer!");
    }
}

function AnalyseCompetence() {
    var url = "/AnalyseCompetence/Create";
    var data = {
        NiveauTaxonomique: $("#NiveauTaxonomique").val(),
        Reformulation: $("#Reformulation").val(),
        Context: $("#Context").val(),
        Titre: $("#Titre").val(),
        SavoirFaireProgramme: $("#SavoirFaireProgramme").val(),
        SavoirEtreProgramme: $("#SavoirEtreProgramme").val(),
        CodeCompetence: $("#CodeCompetence").val()
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
            alert("Analyse de la compétence ajouté avec succès");
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

function DimensionAjoutAjax(divId) {
    var formModal = $(divId).dialog({
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

function AfficherDescription(i) {
    var Des = "";
    var url = "/AnalyseCompetence/AfficherDescription";
    $.ajax({
        data: { codeComp: i },
        type: "POST",
        async: false,
        url: url,
        datatype: "json",
        success: function (data) {
            //$("#Description").val(result);
            Des = data
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return Des;
}
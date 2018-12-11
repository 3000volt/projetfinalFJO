//ajouter une compétence
$(function () {
    //Variables globales pour aider au fonctionnement
    var obligatoire;
    var Description;
    var Contexte;
});
function fnAddcommpetenceAjax() {
    var url = "/Competences/Create";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        ObligatoireCégep: $("[name=Obli]:checked").val(),
        Description: $("#Description").val(),
        ContextRealisation: $("#ContextRealisation").val(),
        Titre: $("#Titre").val(),
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
            disableEnvoie();
            return true;
        },
        error: function (xhr) { alert("erreur: Le code de compétence doit être unique!"); }
    });
    return false;
}

//Modifier la compétence
function fnUpdatecommpetenceAjax() {
    var url = "/Competences/Update";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        ObligatoireCégep: $("[name=Obli]:checked").val(),
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
            $('#elementcomp').show();
            ConfirmerModification();
            return true;
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

//ajouter un élément de la compétence
function fnAddelecommpetenceAjax() {
    var url = "/Elementcompetences/Create";
    var data = {
        ElementCompétence: $("#ElementComp_tence").val(),
        CriterePerformance: $("#CriterePerformance").val(),
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
            var name = $("#ElementComp_tence").val();
            fnAssocierelecommpetenceAjax();
            $("#accordion").append("<div class=\"card cardcollapse\"><div class=\"card-header\" id=\"headingOne\">" +
                "<h5 class=\"mb-0\">" +
                "<a class=\"collapselien\" data-toggle=\"collapse\" data-target=\"#" + name + "\" aria-expanded=\"true\" aria-controls=\"collapseOne\"" + "style=" + "color:white;" + " >" +
                "" + name + "" +
                "</a></h5></div>" +
                "<div id=" + name + " class=\"collapse\" aria-labelledby=\"headingOne\" data-parent=\"#accordion\">" +
                "<div class=\"card-body\"><p>" + $("#CriterePerformance").val() + "</p></div>" +
                "</div></div> ")
        },
        error: function (xhr, status) { alert("erreur: l'élément de compétence doit être unique!"); }
    });
    return false;
}

//associer un élément de la compétences à une compétence
function fnAssocierelecommpetenceAjax() {
    var url = "/CompetencesElementCompetences/Create";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        ElementCompétence: $("#ElementComp_tence").val(),
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
/*==================================================================
  [ Show pass ]*/
//ajouter un groupe
function fnGroupeAjax() {
    var url = "/Groupes/Create";
    var data = {
        NomGroupe: $("#NomGroupe2").val(),
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
            alert(result);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

function fnGroupeCompAjax() {
    var url = "/GroupeCompetences/Create";
    var data = {
        CodeCompetence: $("select[id='CodeCompetence'] ").val(),
        NomGroupe: $("select[id='NomGroupe'] ").val(),
        NomSession: $("select[id='NomSession'] ").val(),
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
            alert(result);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

//ajouter un répartition des heures max de la compétence
function fnAddRepartitonHeureMaxCompAjax() {
    var url = "/RepartirHeureCompetences/Create";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        NbHtotalCompetence: $("#NbHtotalCompetence").val(),
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
            alert(result);
            //Désactive le champs de la répartition des heures max
            $("#CodeCompetence").attr('disabled', true);
            $("#NbHtotalCompetence").attr('disabled', true);
            $("div[id='heuremax'] input[type='button']").attr('disabled', true);

            //Affiche les autres div de répartition
            $('#tabs').show();
            $('#tabs2').show();
            
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

//ajouter un répartition des heures max de la compétence
function fnRepartitionHeureSessionAjax() {
    var url = "/RepartitionHeuresessions/Create";
    var data = {
        NbhCompetenceSession: $("div[id='ui-id-8'] input[id='NbhCompetenceSession']").val(),
        AdresseCourriel: $("div[id='ui-id-8'] select[id='AdresseCourriel']").val(),
        CodeCompetence: $("div[id='ui-id-8'] select[id='CodeCompetence']").val(),
        NomSession: $(" div[id='ui-id-8'] select[id='NomSession'] ").val(),
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
            alert(result);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

//ajouter un session
function fnSessionAjax() {
    alert($("input[id=NomSession]").val());
    var url = "/Sessions/Create";
    var data = {
        NomSession: $("input[id=NomSession]").val(),
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
            alert(result);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

function disableEnvoie() {
    //Appeller la function ajax pour créer la compétence
    $("#divDocumentCreation *").attr('disabled', true);
    //https://stackoverflow.com/questions/8423812/enable-disable-a-div-and-its-elements-in-javascript
    //Mettre le bouton modifier utilisable
    $("#btnModifier").attr('disabled', false);
    //Mettre l'ajout innaccessible
    $("#btnAjout").attr('disabled', true);
}

function ModifierEnvoie() {
    //rendre accessible la div de création 
    //Sauf le code qui n'est pas changeable
    $("#divDocumentCreation *").not($("#CodeCompetence")).attr('disabled', false);
    //https://stackoverflow.com/questions/8423812/enable-disable-a-div-and-its-elements-in-javascript
    //Mettre le bouton modifier innutilisable
    $("#btnModifier").attr('disabled', true);
    //Rendre le bouton de confirmation de modification visible
    $("#btnConfirmerModif").attr('hidden', false);
    //Mettre a div des elements de compétences innaccessible
    $("#divElementsCompetence *").attr('disabled', true);
    //Mettre le bouton d'envoies innaccessibles
    $("#divBoutonsEnvoie").attr('disabled', true);
    $("#btnAnalyser").attr('disabled', true);
    //Mettre le bouton d'annulation visible
    $("#btnAnnulerModif").attr('hidden', false);
    //$("#divBoutonsEnvoie").attr('hidden', true);
    $("#btnListe").hide();
    $("#btnAnalyser").hide();
    //Sauvagarder les données
    SauvegardeVariable();
}

function AnnulerModification() {

    //recacher le bouton
    $("#btnAnnulerModif").attr('hidden', true);
    //recacher le bouton confrimer
    $("#btnConfirmerModif").attr('hidden', true);
    //Remmettre modifier accissible
    $("#btnModifier").attr('disabled', false);
    //Remmettre la div innaccessible
    $("#divDocumentCreation *").attr('disabled', true);
    //Remmettre les boutons d'envoies accessibles
    $("#btnListe").attr('disabled', false);
    $("#btnAnalyser").attr('disabled', false);
    //Mettre a div des elements de compétences accessibles
    $("#divElementsCompetence *").attr('disabled', false);
    //Remmettre les bonnes variables dans les endroits appropriés
    $("#Description").val(Description);
    $("#Contexte").val(Contexte);
    $("#divObligatoireCegep input").each(function () {
        if ($(this).val().toString() == obligatoire.toString()) {
            $(this).prop('checked', true);
        }
    });
    //$("#divBoutonsEnvoie").attr('hidden', true);
    $("#btnListe").show();
    $("#btnAnalyser").show();
}

function ConfirmerModification() {
    //recacher le bouton
    $("#btnConfirmerModif").attr('hidden', true);
    $("#btnAnnulerModif").attr('hidden', true);
    //Remmettre modifier accissible
    $("#btnModifier").attr('disabled', false);
    //Remmettre la div innaccessible
    $("#divElementsCompetence *").attr('disabled', false);
    $("#divDocumentCreation *").attr('disabled', true);
    //Remmettre les boutons d'envoies accessibles
    $("#btnListe").attr('disabled', false);
    $("#btnAnalyser").attr('disabled', false);
    //$("#divBoutonsEnvoie").attr('hidden', true);
    $("#btnListe").show();
    $("#btnAnalyser").show();
}

function SauvegardeVariable() {
    Description = $("#Description").val();
    Contexte = $("#Contexte").val();
    if ($("[name=Obli]:checked").val() == "true") {
        obligatoire = true;
    }
    else {
        obligatoire = false;
    }
}
//ajouter une compétence
$(function () {
    //Variables globales pour aider au fonctionnement
   
});
var compname = "";
var name = "";
function fnAddcommpetenceAjax() {
    //S'assurer que les champs de code compétence et de titre sont remplis
    if ($("#CodeCompetence").val() != "" && $("#Titre").val() != "") {
        var url = "/Competences/Create";
        var data = {
            CodeCompetence: $("#CodeCompetence").val(),
            ObligatoireCégep: $("[name=Obli]:checked").val(),
            Description: $("#Description").val(),
            Titre: $("#Titre").val(),
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
                alert("Compétence créé avec succès!");
                compname = $("#CodeCompetence").val();
                $('#tabs').show();
                disableEnvoie();
                return true;
            },
            error: function (xhr) { alert("erreur: Le code de compétence doit être unique et remplis!"); }
        });
        return false;
    }
    else {
        alert("Les champs de code dompétence et de titre doivent être remplis!")
    }
}

//Modifier la compétence
function fnUpdatecommpetenceAjax() {
    var url = "/Competences/Update";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        ObligatoireCégep: $("[name=Obli]:checked").val(),
        Description: $("#Description").val(),
        Titre: $("#Titre").val(),
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
            alert("Compétence modifié avec succès!");
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
        ElementCompétence: $("div[id='ui-id-4'] input[id='ElementComp_tence']").val(),
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
            //Message de statut réussie
            alert("Éléments de compétence ajouté avec succès!");
                           
            //Effacer les champs de l'élément
            $("div[id='ui-id-4'] input[id='ElementComp_tence']").val("");
            $("#CriterePerformance").val("");
        },
        error: function (xhr, status) { alert("erreur: l'élément de compétence doit être unique!"); }
    });
    return false;
}

//Modifier l'element de compétence
function fnModifierElemComp(i, y) {
    var url = "/ElementCompetences/Modifier";
    $.ajax({
        data: { ElementCompétence: i, Critere: y},
        type: "POST",
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },
        success: function (result) {
            // alert(status);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

//associer un élément de la compétences à une compétence
function fnAssocierelecommpetenceAjax() {
    var url = "/CompetencesElementCompetences/Create";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        ElementCompétence: $("div[id='ui-id-2'] select[id='ElementComp_tence']").val(),
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
            name = $("div[id='ui-id-2'] select[id='ElementComp_tence']").val();
            $("#accordion").append("<div class=\"card cardcollapse\"><div class=\"card-header\" id=\"headingOne" + name + "\">" +
                "<div class=\"row\"><div class=\"col-sm-4\"><h5 class=\"mb-0\">" +
                "<a class=\"collapselien\" data-toggle=\"collapse\" data-target=\"#" + name + "\" aria-expanded=\"true\" aria-controls=\"collapseOne\"" + "style=" + "color:white;" + " >" +
                "" + name + "" +
                "</a></h5></div><div class=\"col-sm-8\" style=\"text-align: right;\"><a class=\"btn btn-info btnlist testing\"onclick=\"fnSuppElemComp('" + name + "','" + compname + "')\" >" +
                "<i class=\"fas fa-trash iconlist\"></i><span>Supprimer</span></a></div> </div></div>" +
                "<div id=" + name + " class=\"collapse\" aria-labelledby=\"headingOne\" data-parent=\"#accordion\">" +
                "<div class=\"card-body\"><p>" + result +
                "</p></div>" +
                "</div></div> ");
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}
//associer un élément de la compétences à une compétence
function fnSuppElemComp(i, y) {
    alert(i+y)
    var url = "/CompetencesElementCompetences/supprimer";
    var data = { ElementCompétence: i, CodeCompetence: y, };
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
            var titre = "#headingOne";
            titre += result;
            $("#"+result).hide();
            $(titre).hide();
            
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
            // alert(result);
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
            //alert(result);
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
            //  alert(result);
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
            // alert(result);
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
            //  alert(result);
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
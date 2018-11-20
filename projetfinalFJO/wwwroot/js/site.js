// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Write your JavaScript code.

//ajouter une compétence


function fnAddcommpetenceAjax() {
    var url = "/Competences/Create";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        ObligatoireCégep: $("[name=Obli]:checked").val(),
        Description: $("#Description").val(),
        ContextRealisation: $("#ContextRealisation").val(), 
        NoProgramme: $("#NoProgramme").val()
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
                "<h5 class=\"mb-0\">"+
                "<a class=\"collapselien\" data-toggle=\"collapse\" data-target=\"#" + name + "\" aria-expanded=\"true\" aria-controls=\"collapseOne\""+"style="+"color:white;"+" >"+
                                ""+name+""+
                        "</a></h5></div>"+
                    "<div id="+name+" class=\"collapse\" aria-labelledby=\"headingOne\" data-parent=\"#accordion\">"+
                "<div class=\"card-body\"><p>" + $("#CriterePerformance").val()+"</p></div>"+
                    "</div></div> ")
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
    alert($("#NomGroupe2").val());
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
//ajouter un répartition des heures max de la compétence
function fnAddRepartitonHeureMaxCompAjax() {
    alert($("#CodeCompetence").val());
    var url = "/RepartirHeureCompetences/Create";
    var data = {
        CodeCompetence: $("#CodeCompetence").val(),
        NbHsessionCompetence: $("#NbHsessionCompetence").val(),
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
function fnRepartitionHeureSessionAjax() {
    alert($("#CodeCompetence").val());
    var url = "/RepartirHeuresessions/Create";
    var data = {
        NbhCompetenceCours: $("#NbhCompetenceCours").val(),
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
            alert(result);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}
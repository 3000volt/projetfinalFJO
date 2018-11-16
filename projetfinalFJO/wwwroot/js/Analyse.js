var formModal;
$(function () {

    $("#AjouterFamilleModal").on("click", function () {
        formModal = DimensionAjoutAjax("#divAjouterFamille");
        $(formModal).dialog("open");
        $("form").prop("title", "Add");
    });
    $("#btSubmit").on("click", function () {
        $(formModal).dialog("close");
        //AjouterFamilleAjax();
    });
    $("#btCancel").on("click", function () { $(formModal).dialog("close"); });

    $("#AjouterSequenceModal").on("click", function () {
        formModal = DimensionAjoutAjax("#divAjouterSequence");//TODO**********
        $(formModal).dialog("open");
        $("form").prop("title", "Add");
    });

});

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
        Famille: $("#Famille").val(),
        Sequence: $("#Sequence").val(),
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
        ElementCompétence: $("#Formulaire_" + i + " input[id=ElementComp_tence]").val(),
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        beforeSend: function (request) {
            alert(AdresseCourriel);
            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
        },        
        success: function (result) {
            alert(status);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

function AjouterFamilleAjax() {
    var url = "/Competences/AjouterFamille";
    var data = {
        NomFamille: $("#NomFamille").val(),
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
            var tr = "<tr id=" + $("#Id").val() + "><td>" + $("#NomFamille").val() + "</td>" +
                "<td>" +
                "<a class=\"btn btn-info\" onclick=\"fneditGet(" + $("#Id").val() + ")\"> <i class=\"glyphicon glyphicon-pencil\"></i> Edit </a>" +
                " <a class=\"btn btn-danger\" onclick=\"fndelete(" + $("#Id").val() + ")\"><i class=\"glyphicon glyphicon-trash\"></i>Delete</a>" +
                "</td></tr >";
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}

function AjouterSequenceAjax() {
    var url = "/Competences/AjouterSequence";
    var data = {
        NomSequence: $("#NomSequence").val(),
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
            var tr = "<tr id=" + $("#Id").val() + "><td>" + $("#NomSequence").val() + "</td>" +
                "<td>" +
                "<a class=\"btn btn-info\" onclick=\"fneditGet(" + $("#Id").val() + ")\"> <i class=\"glyphicon glyphicon-pencil\"></i> Edit </a>" +
                " <a class=\"btn btn-danger\" onclick=\"fndelete(" + $("#Id").val() + ")\"><i class=\"glyphicon glyphicon-trash\"></i>Delete</a>" +
                "</td></tr >";
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
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
$(function () {

    $("#AjouterFamilleModal").on("click", function () {
        formModal = DimensionAjoutAjax("#divAjouterFamille");
        $(formModal).dialog("open");
        $("form").prop("title", "Add");
    });
    $("#btSubmit").on("click", function () {
        $(formModal).dialog("close");
        //AjouterFamilleAjax();
    });
    $("#btCancel").on("click", function () { $(formModal).dialog("close"); });

    $("#AjouterSequenceModal").on("click", function () {
        formModal = DimensionAjoutAjax("#divAjouterSequence");//TODO**********
        $(formModal).dialog("open");
        $("form").prop("title", "Add");
    });

});

function Ajustation(item) {
    alert(item);
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
        Famille: $("#Famille").val(),
        Sequence: $("#Sequence").val(),
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
        ElementCompétence: $("#Formulaire_" + i + " input[id=ElementComp_tence]").val(),
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

function AjouterFamilleAjax() {
    var url = "/Competences/AjouterFamille";
    var data = {
        NomFamille: $("#NomFamille").val(),
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
            var tr = "<tr id=" + $("#Id").val() + "><td>" + $("#NomFamille").val() + "</td>" +
                "<td>" +
                "<a class=\"btn btn-info\" onclick=\"fneditGet(" + $("#Id").val() + ")\"> <i class=\"glyphicon glyphicon-pencil\"></i> Edit </a>" +
                " <a class=\"btn btn-danger\" onclick=\"fndelete(" + $("#Id").val() + ")\"><i class=\"glyphicon glyphicon-trash\"></i>Delete</a>" +
                "</td></tr >";
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}

function AjouterSequenceAjax() {
    var url = "/Competences/AjouterSequence";
    var data = {
        NomSequence: $("#NomSequence").val(),
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
            var tr = "<tr id=" + $("#Id").val() + "><td>" + $("#NomSequence").val() + "</td>" +
                "<td>" +
                "<a class=\"btn btn-info\" onclick=\"fneditGet(" + $("#Id").val() + ")\"> <i class=\"glyphicon glyphicon-pencil\"></i> Edit </a>" +
                " <a class=\"btn btn-danger\" onclick=\"fndelete(" + $("#Id").val() + ")\"><i class=\"glyphicon glyphicon-trash\"></i>Delete</a>" +
                "</td></tr >";
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
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
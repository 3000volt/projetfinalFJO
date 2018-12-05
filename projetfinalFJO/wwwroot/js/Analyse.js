var formModal;
$(function () {
    alert("TROLL");
    var code = $("#CodeCompetence").val().toString();
    alert("testss");
    $("#Description").val(AfficherDescription(code));
    //$("#Description").val(AfficherDescription(code));
    //Appeler la fonction au changement de valeur du select
    $("#CodeCompetence").on('change', function () {
        //https://stackoverflow.com/questions/11179406/jquery-get-value-of-select-onchange
        //appeler la function ajax qui va chanegr la valeur de la description
        var code = $("#CodeCompetence").val().toString();
        //alert(code);
        $("#Description").val(AfficherDescription(code));
    });
});

function Ajustation(item) {
    alert(item);
    var a = document.getElementById(item).id;
    num = a.charAt(6);
    AnalyseElementCompetence(num);
}


function AnalyseElementCompetence(i) {

    var url = "/AnalyseElementCompetence/Create";
    var data = {
        NiveauTaxonomique: $("#Formulaire_" + i + " select[id=NiveauTaxonomique]").val(),
        Reformulation: $("#Formulaire_" + i + " input[id=Reformulation]").val(),
        Context: $("#Formulaire_" + i + " input[id=Context]").val(),
        SavoirFaireProgramme: $("#Formulaire_" + i + " input[id=SavoirFaireProgramme]").val(),
        SavoirEtreProgramme: $("#Formulaire_" + i + " input[id=SavoirEtreProgramme]").val(),
        //AdresseCourriel: $("#Formulaire_" + i + " input[id=AdresseCourriel]").val(),
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
            alert(status);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return false;
}

function AjouterFamilleAjax() {
    alert("allo");
    alert($("#NomFamille2").val());
    var url = "/Competences/AjouterFamille";
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
            alert(result);
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
}

function AjouterSequenceAjax() {
    var url = "/Competences/AjouterSequence";
    var data = {
        NomSequence: $("#NomSequence2").val(),
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
//$(function () {

//    $("#AjouterFamilleModal").on("click", function () {
//        formModal = DimensionAjoutAjax("#divAjouterFamille");
//        $(formModal).dialog("open");
//        $("form").prop("title", "Add");
//    });
//    $("#btSubmit").on("click", function () {
//        $(formModal).dialog("close");
//        //AjouterFamilleAjax();
//    });
//    $("#btCancel").on("click", function () { $(formModal).dialog("close"); });

//    $("#AjouterSequenceModal").on("click", function () {
//        formModal = DimensionAjoutAjax("#divAjouterSequence");//TODO**********
//        $(formModal).dialog("open");
//        $("form").prop("title", "Add");
//    });

//});

function Ajustation(item) {
    alert(item);
    var a = document.getElementById(item).id;
    num = a.charAt(6);

    AnalyseElementCompetence(num);
}

//function AnalyseCompetence() {
//    alert("test Analyse");
//    var url = "/AnalyseCompetence/Create";
//    var data = {
//        NiveauTaxonomique: $("#NiveauTaxonomique").val(),
//        Reformulation: $("#Reformulation").val(),
//        Context: $("#Context").val(),
//        SavoirFaireProgramme: $("#SavoirFaireProgramme").val(),
//        SavoirEtreProgramme: $("#SavoirEtreProgramme").val(),
//        AdresseCourriel: $("#AdresseCourriel").val(),
//        Famille: $("#Famille").val(),
//        Sequence: $("#Sequence").val(),
//        CodeCompetence: $("#CodeCompetence").val(),
//    };
//    $.ajax({
//        data: JSON.stringify(data),
//        type: "POST",
//        url: url,
//        datatype: "text/plain",
//        contentType: "application/json; charset=utf-8",
//        beforeSend: function (request) {
//            request.setRequestHeader("RequestVerificationToken", $("input[name='__RequestVerificationToken']").val());
//        },
//        success: function (result) {
//            alert(status);
//        },
//        error: function (xhr, status) { alert("erreur:" + status); }
//    });
//    return false;
//}
function AnalyseCompetence() {
    alert("test REPBS");
    var url = "/AnalyseCompetence/Create";
    var data = {
        NiveauTaxonomique: $("#NiveauTaxonomique").val(),
        Reformulation: $("#Reformulation").val(),
        Context: $("#Context").val(),
        SavoirFaireProgramme: $("#SavoirFaireProgramme").val(),
        SavoirEtreProgramme: $("#SavoirEtreProgramme").val(),
        //AdresseCourriel: $("#AdresseCourriel").val(),
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
            alert(status);
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
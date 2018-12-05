$(function () {
    alert("tobo");

    //for (var o = 1; o < 7; o++) { //todo : 7
    $("#divTableau table").each(function () {
        var table = document.getElementById(this.id).id;
        var ranges = document.getElementById(table).rows.length; //5
        //Nombre de colonne
        var range = document.getElementById(this.id).rows[0].cells.length;//4
        for (var t = 1; t < (ranges - 1); t++) {//4
            //Trouver le cours en question
            var laCompetence = TrouverNumCompetence((this.id), t);
            for (var w = 1; w < range; w++) {
                //Trouver la compétence
                var leCours = TrouverNumCours((this.id), w);
                //Si le cours est dans la compétence
                var leId = t.toString() + w.toString();
                if (HeureCoursCompetence(leCours, laCompetence)) {

                    $("table[id='" + table + "']¸td[id='" + leId + "'] input[id='NbHCoursCompetence']").attr("readonly", false);
                }
                else {
                    $("table[id='" + table + "'] td[id='" + leId + "'] input[id='NbHCoursCompetence']").attr("readonly", true);
                    $("table[id='" + table + "'] td[id='" + leId + "'] input[id='NbHCoursCompetence']").attr("disabled", true);
                }
            }
        }
        var col = 0;
        var session = this.id;
        $("table[id='" + session + "'] tr:last td input").each(function () {
            col++;
            //Trouver le nom du cours
            var cours = TrouverNumCours(session, col);
            var heure = GererPonderation(cours);
            $(this).val(heure);
        });
        //https://stackoverflow.com/questions/10431987/jquery-each-loop-in-table-row
    });
});

function AjouterTableau(i) {
    var colonne = 0;
    var range = 1;
    var colonneMax = (document.getElementById(i).rows[0].cells.length) - 1; //$("table[id='" + i + "'").rows[0].cells.length;//$("table[id='" + i + ']" 
    var tableau = new Array();
    //Trouver la bonne table 
    $("table[id='" + i + "'] tr td input").each(function () {
        //Si la colonne est deja a 3, il faut la remmettre a 0, et chanegr de rangé
        if (colonne == colonneMax) {
            colonne = 0;
            range++;
        }
        //Ajouter au compteur de la colonne
        colonne++;

        //S'assurer qu'il s'agit d'une classe remplise
        if (!$(this).prop('disabled')) {
            //Trouver le nbHeure
            var nbH = $(this).val();
            //Trouver le cours concerné
            var coursNbH = TrouverNumCours(i, colonne);
            //Trouver la compétence concernée
            var competenceNbH = TrouverNumCompetence(i, range);
            tableau.push({ NomCours: coursNbH, CodeCompetence: competenceNbH, NbHCoursCompetence: nbH });
            //break;
        }
    });//https://stackoverflow.com/questions/8963781/find-if-a-textbox-is-disabled-or-not-using-jquery

    //Appeller la focntion pour ajouter le nb d heures
    AjouterNbHeuresCours(tableau);
}


function TrouverNumCours(y, i) {
    var Cours = $("table[id='" + y + "'] tr[id='Tete'] th")[i].id;
    return Cours;
    //https://stackoverflow.com/questions/10964378/get-id-value-by-index-using-name-jquery
}

function TrouverNumCompetence(y, i) {
    var Competence = $("table[id='" + y + "'] tr")[i].id;
    return Competence;
}


function HeureCoursCompetence(i, y) {
    var bool = false;
    var url = "/RepartitionHeureCoursSessionCompetences/CoursCompetence";
    var data = {
        NomCours: i,
        CodeCompetence: y,
        Complete: false
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        async: false,
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            bool = data;
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return bool;
    //https://stackoverflow.com/questions/23078650/ajax-return-true-false-i-have-implemented-a-callback
    //https://forum.jquery.com/topic/ajax-returning-bool-value-out-of-success
}

function AjouterNbHeuresCours(tableau_donner) {
    var url = "/RepartitionHeureCoursSessionCompetences/AjouterNbHeures";
    var data = tableau_donner;
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            alert(result);
            return true;
        },
        error: function (xhr, result) { alert("erreur:" + result); }
    });
    return false;
}

function GererPonderation(i) {
    var heures;
    var url = "/RepartitionHeureCoursSessionCompetences/GererPonderation";
    $.ajax({
        data: "{'NomCours':'" + i + "'}",
        type: "POST",
        async: false,
        url: url,
        datatype: "text/plain",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            heures = data;
        },
        error: function (xhr, status) { alert("erreur:" + status); }
    });
    return heures;
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function fct_alternate(id) {
    if (document.getElementsByTagName) {

        var table = document.getElementById(id);

        var rows = table.getElementsByTagName("tr");

        for (var i = 0; i < table.rows.length; i++) {  

            if (parseInt(table.rows[i].cells.item(5).innerHTML) <= -30) {
                rows[i].className = "negativeMAX";
            }
            if (parseInt(table.rows[i].cells.item(5).innerHTML) <= -20 && parseInt(table.rows[i].cells.item(5).innerHTML) > -30) {
                rows[i].className = "negative30";
            }
            if (parseInt(table.rows[i].cells.item(5).innerHTML) <= -10 && parseInt(table.rows[i].cells.item(5).innerHTML) > -20) {
                rows[i].className = "negative20";
                }
            if (parseInt(table.rows[i].cells.item(5).innerHTML) < 0 && parseInt(table.rows[i].cells.item(5).innerHTML) > -10) {
                rows[i].className = "negative10";
            }
            if (parseInt(table.rows[i].cells.item(5).innerHTML) <= 0 && parseInt(table.rows[i].cells.item(5).innerHTML) >= 0) {
                rows[i].className = "neutral";
            }
            if (parseFloat(table.rows[i].cells.item(5).innerHTML) > 0 && parseInt(table.rows[i].cells.item(5).innerHTML) < 10) {
                    rows[i].className = "positive10";
                }
            if (parseFloat(table.rows[i].cells.item(5).innerHTML) >=10 && parseInt(table.rows[i].cells.item(5).innerHTML) < 20) {
                rows[i].className = "positive20";
            }
        }
    }
}

function fct_searchable() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("thetable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

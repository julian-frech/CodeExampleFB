// Quick Table Search
$('#search').keyup(function () {
    var regex = new RegExp($('#search').val(), "i");
    var rows = $('table tr:gt(0)');
    rows.each(function (index) {
        title = $(this).children("#title").html()
        if (title.search(regex) != -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
});

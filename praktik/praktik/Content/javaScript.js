$(function () {
    $(document).tooltip({
        track: true,
        content: function () {
            var element = $(this);
            return element.attr("title");
        }
    });
});

$(document).ready(function () {
    $("#tbl-data").tablesorter();
}
);



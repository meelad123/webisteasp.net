$(function () {
    $(document).tooltip({
        track: true,
        content: function () {
            var element = $(this);
            return element.attr("title");
        }
    });
});



function printData() {
    var divToPrint = document.getElementById("tbl-data");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}
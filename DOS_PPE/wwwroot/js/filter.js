var sortAsc = true; // เก็บสถานะการเรียงลำดับ (true: asc, false: desc)
function sortCol(col) {
    var rows = $(".table-row");
    $("#descMo0, #descMo1, #descMo2, #descMo3, #descMo4, #descMo5, #descMo6, #descMo7, #descMo8, #descMo9, #descMo10").addClass("text-body-tertiary").removeClass("text-black");
    $("#ascMo0, #ascMo1, #ascMo2, #ascMo3, #ascMo4, #ascMo5, #ascMo6, #ascMo7, #ascMo8, #ascMo9, #ascMo10").addClass("text-body-tertiary").removeClass("text-black");

    rows.sort(function (a, b) {
        var keyA = $(a).find("td:eq(" + col + ")").text();
        var keyB = $(b).find("td:eq(" + col + ")").text();

        // เรียงลำดับตัวอักษร a-z หรือ z-a
        var compareResult = sortAsc ? keyA.localeCompare(keyB) : keyB.localeCompare(keyA);

        if (sortAsc) {
            $("#ascMo" + col).addClass("text-black").removeClass("text-body-tertiary");
        } else {
            $("#descMo" + col).addClass("text-black").removeClass("text-body-tertiary");
        }

        return compareResult;
    });

    $(".type-row").remove();
    $(".table tbody").append(rows);

    // สลับสถานะการเรียงลำดับ
    sortAsc = !sortAsc;
}

function searchChange(id) {
    var searchText = $("#" + id).val().toLowerCase();

    // ใช้ jQuery เลือกแถวของ table และซ่อน/แสดงตามเงื่อนไขการค้นหา
    $("#table-data tbody tr").each(function () {
        var rowText = $(this).text().toLowerCase();
        if (rowText.includes(searchText)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

$(document).ready(function () {
    $("#searchInput").on("input", function () {
        var searchText = $("#searchInput").val().toLowerCase();

        // ใช้ jQuery เลือกแถวของ table และซ่อน/แสดงตามเงื่อนไขการค้นหา
        $("#table-data tbody tr").each(function () {
            var rowText = $(this).text().toLowerCase();
            if (rowText.includes(searchText)) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });
});

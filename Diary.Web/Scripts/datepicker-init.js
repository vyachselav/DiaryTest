$(function () {
    $('.input-daterange').datepicker({
        format: "yyyy-mm-dd",
        weekStart: 1,
        autoclose: true,
        todayHighlight: true,
        orientation: "bottom auto"
    });

    $(document).on('click', '.dropdown-menu', function (e) {
        e.stopPropagation();
    });

    $('#btn-filter').click(function () {
        var fromVal = "?from=" + $("#from").val();
        var toVal = "&to=" + $("#to").val();
        this.href = "/" + fromVal + toVal;
    });
});
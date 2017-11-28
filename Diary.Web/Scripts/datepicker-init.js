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

        //this.href = "Home/Index/"+fromVal+toVal;
        this.href = "/" + fromVal + toVal;


        //var url = this.attr('href');
        // this.attr('href', url + fromVal + toVal);
    });
});
$(function () {
    var id = $("#GrayPurchaseId").val();
    $.ajax({
        url: baseUrl,
        data: { grayPurchaseId: id },
        dataType: 'json'
    })
        .done(function (data) {
            $('#calenderIcon').click(function () {
                $(function () {
                    $("#DyingJob_DOT").datepicker(
                        {
                            format: 'dd M yyyy',
                            startDate: data.value,
                            endDate: '+2w',
                            autoclose: true
                        }).focus();
                })
            });
        })
        .fail(function () { alert("Error"); });
});
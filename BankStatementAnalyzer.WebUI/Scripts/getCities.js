function getCityList(url) {
    var stateId = $("#stateDropDown").val();
    var cityId = $("#cityDropDown :selected").val();
    $.ajax
        ({
            url: url,
            type: 'POST',
            datatype: 'application/json',
            contentType: 'application/json',
            data: JSON.stringify({
                stateId: stateId
            }),
            success: function (result) {
                $("#cityDropDown").html("");

                $.each($.parseJSON(result), function (i, city) {
                    if (city.Value != stateId) {
                        $("#cityDropDown").append('<option value="' + city.Value + '">' + city.Text + '</option>');
                    }
                    else {
                        $("#cityDropDown").append('<option value="' + city.Value + '" selected>' + city.Text + '</option>');
                    }
                })
                //$("#cityDropDown").append($("<option></option>").val("0").html("Other"));
            }
        });
}
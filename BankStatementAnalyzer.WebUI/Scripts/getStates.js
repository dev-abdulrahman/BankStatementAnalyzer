function getStateList(url) {
    var countryId = $("#CountryId").val();
    var stateId = $("#stateDropDown :selected").val();
    $.ajax
        ({
            url: url,
            type: 'POST',
            datatype: 'application/json',
            contentType: 'application/json',
            data: JSON.stringify({
                countryId: countryId
            }),
            success: function (result) {
                //$("#stateDropDown").remove($("<option></option>").val("0"));
                $("#stateDropDown option[value='0']").remove();

                $.each($.parseJSON(result), function (i, state) {
                    if (state.Value != stateId) {
                        $("#stateDropDown").append('<option value="' + state.Value + '">' + state.Text + '</option>');
                    }
                    else {
                        $("#stateDropDown").append('<option value="' + state.Value + '" selected>' + state.Text + '</option>');
                    }
                })
                $("#cityDropDown").append($("<option></option>").val("0").html("Other"));
            }
        });
}
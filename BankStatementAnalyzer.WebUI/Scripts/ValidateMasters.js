function validateAgentMaster() {
    var name = $("#AgentName").val();
    $("#submitAgent").attr('disabled', false);

    $.ajax({
        type: "POST",
        url: "/GrayPurchase/ValidateAgentMaster",
        data: { name: name },
        success: function (data) {
            if (data.result == true) {
                $("#AgentName").css('border-color', 'red');

                $(".validationMessage").text("Agent with Same Name Already Exist");

                $("#submitAgent").attr('disabled', true);
            }
            else {
                $("#AgentName").css('border-color', '');

                $(".validationMessage").text("");
            }
        }
    })
}

function validatePartyMaster() {
    var name = $("#PartyMaster").val();
    $("#submitParty").attr('disabled', false);

    $.ajax({
        type: "POST",
        url: "/PartyMaster/ValidatePartyMaster",
        data: { name: name },
        success: function (data) {
            if (data.result == true) {
                $("#PartyMaster").css('border-color', 'red');

                $(".validationMessage").text("Party with Same Name Already Exist");

                $("#submitParty").attr('disabled', true);
            }
            else {
                $("#PartyMaster").css('border-color', '');

                $(".validationMessage").text("");
            }
        }
    })
}

function validateGrayQuality() {
    var name = $("#GrayQuality").val();
    $("#submitGrayQuality").attr('disabled', false);

    $.ajax({
        type: "POST",
        url: "/GrayPurchase/ValidateGrayQuality",
        data: { name: name },
        success: function (data) {
            if (data.result == true) {
                $("#GrayQuality").css('border-color', 'red');

                $(".validationMessage").text("Gray Quality with Same Name Already Exist");

                $("#submitGrayQuality").attr('disabled', true);
            } else {
                $("#AgentName").css('border-color', '');

                $(".validationMessage").text("");
            }
        }
    })
}

function validateGray() {
    var name = $("#Gray").val();
    $("#submitGray").attr('disabled', false);

    $.ajax({
        type: "POST",
        url: "/GrayPurchase/ValidateGray",
        data: { name: name },
        success: function (data) {
            if (data.result == true) {
                $("#Gray").css('border-color', 'red');

                $(".validationMessage").text("Gray Same Name Already Exist");

                $("#submitGray").attr('disabled', true);
            } else {
                $("#AgentName").css('border-color', '');

                $(".validationMessage").text("");
            }
        }
    })
}

function validateMeterInwards() {
    var meterInward = $("#MeterInwards").val();
    var dyingJobId = $("#DyingJobId").val();
    $("#submitDyingReturn").attr('disabled', false);

    $.ajax({
        type: "POST",
        url: "/DyingReturn/validateMeterInwards",
        data: { meterInward: meterInward, dyingJobId: dyingJobId },
        success: function (data) {
            if (data.result == true) {
                $('#srinkage').removeAttr('hidden');
                $("#srinkage").text('Skrinage : ' + data.Srinkage + ' meter (s)')
            }
            else {
                $("#MeterInwards").css('border-color', 'red');

                $.toast({
                    heading: 'Meter inwards',
                    text: data.text,
                    position: 'top-right',
                    loaderBg: '#ff6849',
                    icon: 'warning',
                    hideAfter: 5000,
                    stack: 6
                });

                $("#submitDyingReturn").attr('disabled', true);
            }
        }
    })
}

function Calculate() {
    var textboxArray = $('input[id^="meters"]');
    var quantity = 0;
    for (i = 0; i < textboxArray.length; i++) {
        quantity += parseFloat($(textboxArray[i]).val());
    }
    $("#quantity").text('Total Quantity : ' + quantity.toFixed(2) + ' meter (s)');

    var weightTextboxArray = $('input[id^="lumpsweight"]');
    var weight = 0;
    for (i = 0; i < weightTextboxArray.length; i++) {
        weight += parseFloat($(weightTextboxArray[i]).val());
    }
    $("#weight").text('Total weight : ' + weight.toFixed(2) + ' kg (s)');

    var rate = $("#GrayPurchase_Rate").val();
    var total = rate * quantity;
    $("#amount").text('Total Amount : ' + total.toFixed(2) + ' Rs/-');
}

function validateLumpMeter(id, textBoxId, srinkageSpanId) {
    $("#submitDyingJob").attr('disabled', false);
    var meter = $("#" + textBoxId).val();

    $.ajax({
        url: urlForValidateMeter,
        data: { id: id, value: meter },
        dataType: 'json'
    })
        .done(function (data) {
            if (data.result == false) {
                $.toast({
                    heading: 'Meter',
                    text: data.text,
                    position: 'top-right',
                    loaderBg: '#ff6849',
                    icon: 'error',
                    hideAfter: 5000,
                    stack: 6
                });

                $("#submitDyingJob").attr('disabled', true);
            }
            else {
                $("#" + srinkageSpanId).text(data.Srinkage)

                var totalLumpSrinkageTextboxArray = document.getElementsByClassName("srinkageClass");;
                var srinkage = 0;
                for (i = 0; i < totalLumpSrinkageTextboxArray.length; i++) {
                    srinkage += parseFloat($(totalLumpSrinkageTextboxArray[i]).text());
                }
                $("#srinkage").text('Total Skrinage : ' + srinkage + ' meter (s)')
            }
        })
        .fail(function () {
            $.toast({
                heading: 'Meter',
                text: "Meter cannot be empty or 0.",
                position: 'top-right',
                loaderBg: '#ff6849',
                icon: 'error',
                hideAfter: 5000,
                stack: 6
            });
        });
}

function validateLumpWeight(id, textBoxId) {
    $("#submitDyingJob").attr('disabled', false);
    var weight = $("#" + textBoxId).val()

    $.ajax({
        url: urlForValidateWeight,
        data: { id: id, value: weight },
        dataType: 'json'
    })
        .done(function (data) {
            if (data.result == false) {
                $.toast({
                    heading: 'Weight',
                    text: data.text,
                    position: 'top-right',
                    loaderBg: '#ff6849',
                    icon: 'error',
                    hideAfter: 5000,
                    stack: 6
                });
                $("#submitDyingJob").attr('disabled', true);
            }
        })
        .fail(function () {
            $.toast({
                heading: 'Weight',
                text: "Weight cannot be empty or 0.",
                position: 'top-right',
                loaderBg: '#ff6849',
                icon: 'error',
                hideAfter: 5000,
                stack: 6
            });
        });
}

function validateFinishGrayLumpMeter(id, textBoxId) {
    $("#submitDyingReturn").attr('disabled', false);
    var meter = $("#" + textBoxId).val();

    $.ajax({
        url: urlForValidateFinishGrayMeter,
        data: { id: id, value: meter },
        dataType: 'json'
    })
        .done(function (data) {
            if (data.result == false) {
                $.toast({
                    heading: 'Meter',
                    text: data.text,
                    position: 'top-right',
                    loaderBg: '#ff6849',
                    icon: 'error',
                    hideAfter: 5000,
                    stack: 6
                });

                $("#submitDyingReturn").attr('disabled', true);
            }
        })
        .fail(function () {
            $.toast({
                heading: 'Meter',
                text: "Meter cannot be empty or 0.",
                position: 'top-right',
                loaderBg: '#ff6849',
                icon: 'error',
                hideAfter: 5000,
                stack: 6
            });
        });
}

function validateFinishGrayLumpWeight(id, textBoxId) {
    $("#submitDyingJob").attr('disabled', false);
    var weight = $("#" + textBoxId).val()

    $.ajax({
        url: urlForValidateFinishGrayWeight,
        data: { id: id, value: weight },
        dataType: 'json'
    })
        .done(function (data) {
            if (data.result == false) {
                $.toast({
                    heading: 'Weight',
                    text: data.text,
                    position: 'top-right',
                    loaderBg: '#ff6849',
                    icon: 'error',
                    hideAfter: 5000,
                    stack: 6
                });
                $("#submitDyingJob").attr('disabled', true);
            }
        })
        .fail(function () {
            $.toast({
                heading: 'Weight',
                text: "Weight cannot be empty or 0.",
                position: 'top-right',
                loaderBg: '#ff6849',
                icon: 'error',
                hideAfter: 5000,
                stack: 6
            });
        });
}
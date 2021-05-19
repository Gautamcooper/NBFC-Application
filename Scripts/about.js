//Hide all cities at first
$("#currentcity_select").find("option").hide();
// Gender dropdown
var gender1 = $("#gender").attr("value");
$("#gender_select").val(gender1);
var gender_ = $("#gender_select").val();
if (gender_ == null || gender_ == "") {
    $("#gender_select").val("-1");
}
$("#gender_select").change(function () {
    var gender = $("#gender_select").val();
    $("#gender").attr("value", gender);    
});
$("#gender_select").focus(function () {   
    $("#gender_select option[value='-1']").attr('disabled', 'disabled');
});

// Marital Status dropdown
var marital1 = $("#maritalstatus").attr("value");
$("#marital_select").val(marital1);
var marital_ = $("#marital_select").val();
if (marital_ == null || marital_ == "") {
    $("#marital_select").val("-1");
}
$("#marital_select").change(function () {
    var gender = $("#marital_select").val();
    $("#maritalstatus").attr("value", gender);
});
$("#marital_select").focus(function () {
    $("#marital_select option[value='-1']").attr('disabled', 'disabled');
});

// Employment Type dropdown
var employment = $("#employmenttype").attr("value");
$("#employment_select").val(employment);
var employment_ = $("#employment_select").val();
if (employment_ == null || employment_ == "") {
    $("#employment_select").val("-1");
}
$("#employment_select").change(function () {
    var employ = $("#employment_select").val();
    $("#employmenttype").attr("value", employ);
});
$("#employment_select").focus(function () {
    $("#employment_select option[value='-1']").attr('disabled', 'disabled');
});


// City Type dropdown
var city = $("#currentcity").attr("value");
$("#currentcity_select").val(city);
var city_ = $("#currentcity_select").val();
if (city_ == null || city_ == "") {
    $("#currentcity_select").val("-1");
}
$("#currentcity_select").change(function () {
    var cities = $("#currentcity_select").val();
    $("#currentcity").attr("value", cities);
});
$("#currentcity_select").focus(function () {
    $("#currentcity_select option[value='-1']").attr('disabled', 'disabled');
});

// State Type dropdown
var states = $("#currentstate").attr("value");
$("#currentstate_select").val(states);
var state_ = $("#currentstate_select").val();
if (state_ == null || state_ == "") {
    $("#currentstate_select").val("-1");
}
$("#currentstate_select").change(function () {
    var state = $("#currentstate_select").val();
    $("#currentstate").attr("value", state);
});
$("#currentstate_select").focus(function () {
    $("#currentstate_select option[value='-1']").attr('disabled', 'disabled');
});
//Populate state and cities
$("#currentstate_select").change(function () {
    var state_val = $("#currentstate_select").val();
    var state = $("#currentstate_select option[value='"+state_val+"']").text();
    $("#currentcity_select").find("option").hide();
    $("#currentcity_select").find("option[state='" + state + "']").show();
})
// Country Type dropdown
var countries = $("#currentcountry").attr("value");
$("#currentcountry_select").val(countries);
var countries_ = $("#currentcountry_select").val();
if (countries_ == null || countries_ == "") {
    $("#currentcountry_select").val("-1");
}
$("#currentcountry_select").change(function () {
    var country = $("#currentcountry_select").val();
    $("#currentcountry").attr("value", country);
});
$("#currentcountry_select").focus(function () {
    $("#currentcountry_select option[value='-1']").attr('disabled', 'disabled');
});
$("#pan").change(function () {
    var inputvalues = $(this).val();
    var regex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    if (!regex.test(inputvalues)) {
        $("#pan").val("");
        alert("invalid PAN no");
        $("#pan").focus();
    }
});
var uploadedval = $("#uploadedvalue").val();
if (uploadedval == "true") {
    $('#uploaded').prop('checked',true);
}
else {
    $('#uploaded').prop('checked', false);
}
$("#bankname_select").change(function () {
    var bankname = $("#bankname_select").val();
    $("#bankname").attr("value", bankname);
});
$("#coapplicantrelationship_select").change(function () {
    var coapplicantrelation = $("#coapplicantrelationship_select").val();
    $("#coapplicantrelationship").attr("value", coapplicantrelation);
});
var step1 = $("#step1").val();
var uploadedvalue = $("#uploadedvalue").val();
if (step1 == "true" && uploadedvalue == "true") {
    $("#continue").show();
};

$("#save").click(function () {
    if ($('#gender_select').val() == '-1' || $('#marital_select').val() == '-1' || $('#employment_select').val() == '-1' || $('#currentcity_select').val() == '-1' || $('#currentstate_select').val() == '-1' || $('#currentcountry_select').val() == '-1') {
        alert("Please fill in all the Drop down columns!");
        return false;
    }

});

$("#apply").click(function () {
    if ($('#coapplicantrelationship_select').val() == '-1' || $('#bankname_select').val() == '-1') {
        alert("Please fill in all the Drop down columns!");
        return false;
    }

});
// Agreement Number dropdown
var agrm = $("#agreement").attr("value");
$("#agreement_select").val(agrm);
var agrm_ = $("#agreement_select").val();
if (agrm_ == null || agrm_ == "") {
    $("#agreement_select").val("-1");
}
$("#agreement_select").change(function () {
    var agr = $("#agreement_select").val();
    var loantype = agr.split(",");
    $("#loantype").attr("value", loantype[1]);
    $("#agreement").attr("value", loantype[0]);
    $("#save").click();
});
$("#agreement_select").focus(function () {
    $("#agreement_select option[value='-1']").attr('disabled', 'disabled');
});
var total_amnt = 0;
var count = 0;
$(".table_checkbox").change(function () {
    var check_id = this.getAttribute("id");
    var id = check_id.split("_");
    var amount = parseFloat($("#" + id[0] + "_amount").text());
    if (this.checked == true) {
        total_amnt += amount;
        count += 1;
    }
    else {
        total_amnt -= amount;
        count -= 1;
    }
    total_amnt = parseFloat(total_amnt.toFixed(2));
    $("#total_amount").text(total_amnt);
    $("#count").attr("value", count);
    $("#amount").attr("value", total_amnt);
});
if (($("#agrid").val() != null) && ($("#agrloantype").val() != null)) {
    var agrid = $("#agrid").val();
    var agrloantype = $("#agrloantype").val();
    var value = agrid + "," + agrloantype;
    $("#agreement_select").val(value);
}
$(".inp").each(function () {
    if (this.value != "") {
        var id = this.getAttribute("id");
        $("#label_" + id).show();
    }
    else if (this.value == "") {
        var id = this.getAttribute("id");
        $("#label_" + id).hide();
    }
})
$(".inp").change(function () {
    if (this.value != "") {
        var id = this.getAttribute("id");
        $("#label_" + id).show(500);
    }
    else if (this.value == "") {
        var id = this.getAttribute("id");
        $("#label_" + id).hide(500);
    }
})
$(".inp_dropdown").each(function () {
    if (this.value != "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).show();
    }
    else if (this.value == "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).hide();
    }
});
$(".inp_dropdown").change(function () {
    if (this.value != "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).show(500);
    }
    else if (this.value == "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).hide(500);
    }
})
$(".remove").click(function () {
    var id = this.id;
    var id_ = id.substring(7);
    $("#" + id_).val(null);
});
$("#checkaddr").change(function () {
    var checked = $("#checkaddr").prop("checked");
    if (checked == true) {
        $("#address").hide();
    }
    else {
        $("#address").show();
    }
})





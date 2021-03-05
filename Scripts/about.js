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

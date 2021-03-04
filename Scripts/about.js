﻿// Gender dropdown
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

$("#pan").change(function () {
    var inputvalues = $(this).val();
    var regex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    if (!regex.test(inputvalues)) {
        $("#pan").val("");
        alert("invalid PAN no");
        $("#pan").focus();
    }
});
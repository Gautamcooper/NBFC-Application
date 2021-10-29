﻿$('#click').click(function (e) {
    return $('input:file')[0].click();
});

$('#file-attachment-pan').click(function (e) {
    return $('#pan_upload').click();
});

$('#file-attachment-aadhaarfront').click(function (e) {
    return $('#aadhaar_front').click();
});

$('#file-attachment-aadhaarback').click(function (e) {
    return $('#aadhaar_back').click();
});

$("#file-input").change(function () {
    var image = document.getElementById('user_pic');
    image.src = URL.createObjectURL(event.target.files[0]);
});
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
        $("#pan").addClass(" is-invalid");
        $("#validationPan").show();
        $("#pan").focus();
    }
    else {
        $("#pan").removeClass(" is-invalid");
        $("#validationPan").hide();
    }
});
var uploadedval = $("#uploadedvalue").val();
if (uploadedval == "true") {
    $('#uploaded').prop('checked', true);
    $('#upload_success').show();
}
else {
    $('#uploaded').prop('checked', false);
    $('#upload_success').hide();
}
$("#bankname_select").change(function () {
    var bankname = $("#bankname_select").val();
    $("#bankname").attr("value", bankname);
});
$("#coapplicantrelationship_select").change(function () {
    var coapplicantrelation = $("#coapplicantrelationship_select").val();
    $("#coapplicantrelationship").attr("value", coapplicantrelation);
});
$("#nomineerelationship_select").change(function () {
    var nomineerelation = $("#nomineerelationship_select").val();
    $("#nomineerelationship").attr("value", nomineerelation);
});
var step1 = $("#step1").val();
var uploadedvalue = $("#uploadedvalue").val();
if (step1 == "true" && uploadedvalue == "true") {
    $("#continue").show();
};

if (uploadedvalue == "false") {
    $("#addresspanandaadhardetails").hide();
}

$("#save").click(function () {
    var regex = /^[a-zA-Z ]{2,30}$/;
    var panRegex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
    var aadhaarRegexWithSpace = /^[2-9]{1}[0-9]{3}\s{1}[0-9]{4}\s{1}[0-9]{4}$/;
    var aadhaarRegexWithoutSpace = /^[2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}$/;
    var currentPinRegex = /^[1-9]{1}[0-9]{5}$/;

    let fullname = ($("#Fullname").val() !== '') ? !regex.test($("#Fullname").val()) : false;
    let firstname = ($("#firstname").val() !== '') ? !regex.test($("#firstname").val()) : false;
    let middlename = ($("#middlename").val() !== '') ? !regex.test($("#middlename").val()) : false;
    let lastname = ($("#lastname").val() !== '') ? !regex.test($("#lastname").val()) : false;
    let fathername = ($("#fathername").val() !== '') ? !regex.test($("#fathername").val()) : false;
    let pin = ($("#currentpin").val() !== '') ? !currentPinRegex.test($("#currentpin").val()) : false;
    let panfirstname = ($("#panfirstname").val() !== '') ? !regex.test($("#panfirstname").val()) : false;
    let panmiddlename = ($("#panmiddlename").val() !== '') ? !regex.test($("#panmiddlename").val()) : false;
    let panlastname = ($("#panlastname").val() !== '') ? !regex.test($("#panlastname").val()) : false;
    let panfathername = ($("#panfathername").val() !== '') ? !regex.test($("#panfathername").val()) : false;
    let pannumber = ($("#pan").val() !== '') ? !panRegex.test($("#pan").val()) : false;
    let aadharfirstname = ($("#aadharfirstname").val() !== '') ? !regex.test($("#aadharfirstname").val()) : false;
    let aadharmiddlename = ($("#aadharmiddlename").val() !== '') ? !regex.test($("#aadharmiddlename").val()) : false;
    let aadharlastname = ($("#aadharlastname").val() !== '') ? !regex.test($("#aadharlastname").val()) : false;
    let aadharnumber = ($("#aadharnumber").val() !== '') ? (!aadhaarRegexWithSpace.test($("#aadharnumber").val()) && !aadhaarRegexWithoutSpace.test($("#aadharnumber").val())) : false;

    if (fullname || firstname || middlename || lastname || fathername || pin || panfirstname || panmiddlename || panlastname
        || panfathername || pannumber || aadharfirstname || aadharmiddlename || aadharlastname || aadharnumber) {
        $("#validationText")[0].innerText = "Please fill the correct data, highlighted in red!";
        $("#validationModal").modal("show");
        return false;
    }
    else if ($('#gender_select').val() === '-1') {
        $("#validationText")[0].innerText = "Please fill in Gender Drop down column!";
        $("#validationModal").modal("show");
        return false;
    }
    else if ($('#marital_select').val() === '-1') {
        $("#validationText")[0].innerText = "Please fill in Marital Status Drop down column!";
        $("#validationModal").modal("show");
        return false;
    }
    else if ($('#employment_select').val() === '-1') {
        $("#validationText")[0].innerText = "Please fill in Employment Type Drop down column!";
        $("#validationModal").modal("show");
        return false;
    }
});

$("#apply").click(function () {
    if ($('#bankifsccode').val() == '' || $('#bankaccountnumber').val() == '' || $('#bankname_select').val() == '-1') {
        $('#bankDetailsAlert').show();
        return false;
    }
    if ($('#bankifsccode').val() != '' && $('#bankaccountnumber').val() != '' && $('#bankname_select').val() != '-1') {
        $('#step2-modal').modal('hide');
        $('.modal-backdrop').remove();
        $('.bodyclass').css("padding-right", "0px");
        return true;
    }
});

$('#bankifsccode').change(function () {
    if ($('#bankifsccode').val() != '' && $('#bankaccountnumber').val() != '' && $('#bankname_select').val() != '-1') {
        $('#bankDetailsAlert').hide();
    }
    else {
        $('#bankDetailsAlert').show();
    }
});

$('#bankaccountnumber').change(function () {
    if ($('#bankifsccode').val() != '' && $('#bankaccountnumber').val() != '' && $('#bankname_select').val() != '-1') {
        $('#bankDetailsAlert').hide();
    }
    else {
        $('#bankDetailsAlert').show();
    }
});

$('#bankname_select').change(function () {
    if ($('#bankifsccode').val() != '' && $('#bankaccountnumber').val() != '' && $('#bankname_select').val() != '-1') {
        $('#bankDetailsAlert').hide();
    }
    else {
        $('#bankDetailsAlert').show();
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
$(".remove").click(function () {
    var id = this.id;
    var id_ = id.substring(7);
    $("#" + id_).val(null);
});

//var currentaddrsameasaadhar = $("#currentaddrsameasaadhar").val();
//if (currentaddrsameasaadhar == "true") {
//    $("#checkaddr").prop("checked", true);
//    $("#address").hide();
//}
//else {
//    $("#checkaddr").prop("checked", false);
//    $("#address").show();
//}

$("#checkaddr").change(function () {
    var checked = $("#checkaddr").prop("checked");
    if (checked == true) {
        $("#address").hide();
    }
    else {
        $("#address").show();
    }
})
$("#sel_prd").change(function () {
    var value = $("#sel_prd").val();
    $("#product_name").val(value);
});
$("#loanType").change(function () {
    var value = $("#loanType").val();
    $("#typeofloan").val(value);
});
var productId = $("#productId").val();
if (productId != "") {
    $("#sel_prd").val(productId);
}

var slider = $("#myRange");
slider.on('input', function () {
    $("#loanAmount").val($("#myRange").val());
});
$("#loanAmount").change(function () {
    $("#myRange").val($("#loanAmount").val());
});
var slider_1 = $("#myRangeIncome");
slider_1.on('input', function () {
    $("#MonthlyIncome").val($("#myRangeIncome").val());
});
$("#MonthlyIncome").change(function () {
    $("#myRangeIncome").val($("#MonthlyIncome").val());
});
$("#uploadDocs").click(function () {
    $('#myModal').modal('hide');
    $('.modal-backdrop').remove();
    $('.bodyclass').css("padding-right", "0px");
});

(function () {

    'use strict';

    $('.input-file').each(function () {
        var $input = $(this),
            $label = $input.next('.js-labelFile'),
            labelVal = $label.html();

        $input.on('change', function (element) {
            var fileName = '';
            if (element.target.value) fileName = element.target.value.split('\\').pop();
            fileName ? $label.addClass('has-file').find('.js-fileName').html(fileName) : $label.removeClass('has-file').html(labelVal);
        });
    });

    $('#remove_pan_upload').click(function () {
        var labelVal = $('#pan_upload').next('.js-labelFile').html();
        $('#pan_upload').next('.js-labelFile').removeClass('has-file');
        $('#pan_label')[0].children[1].innerHTML = "Choose a file";
    });

    $('#remove_aadhaar_front').click(function () {
        var labelVal = $('#aadhaar_front').next('.js-labelFile').html();
        $('#aadhaar_front').next('.js-labelFile').removeClass('has-file');
        $('#aadhaar_front_label')[0].children[1].innerHTML = "Choose a file";
    });

    $('#remove_aadhaar_back').click(function () {
        var labelVal = $('#aadhaar_back').next('.js-labelFile').html();
        $('#aadhaar_back').next('.js-labelFile').removeClass('has-file');
        $('#aadhaar_back_label')[0].children[1].innerHTML = "Choose a file";
    });

})();

function uploadDocumentsLoader () {
    let k = 0;
    const progress = document.querySelector(".loadprogress");
    const loading = document.querySelector(".loading");
    const uploadPercentage = [0, 0, 1, 1, 2, 3, 10, 22, 25, 28, 37, 40, 42, 45, 54, 58, 58, 58, 58, 64, 71, 75, 83, 87, 90, 90, 94, 95, 95, 95, 99];
    const interval = setInterval(() => {
        progress.style.width = (uploadPercentage[k] < 97) ? (uploadPercentage[k] + 3) + "%" : (uploadPercentage[k] - 1) + "%";
        loading.innerHTML = uploadPercentage[k] + "%";
        k++;
        if (k == uploadPercentage.length) {
            clearInterval(interval);
            loading.innerHTML = "99%";
        }
    }, 1000);
    loading.innerHTML = "0%";
    progress.style.width = "3%";
}

function continueToApplyLoader() {
    let k = 0;
    const progress = document.querySelector(".continuetoapplyloadprogress");
    const loading = document.querySelector(".continuetoapplyloading");
    const uploadPercentage = [0, 10, 48, 83, 99];
    const interval = setInterval(() => {
        progress.style.width = uploadPercentage[k] + "%";
        loading.innerHTML = uploadPercentage[k] + "%";
        k++;
        if (k == uploadPercentage.length) {
            clearInterval(interval);
            loading.innerHTML = "99%";
        }
    }, 1000);
    loading.innerHTML = "0%";
    progress.style.width = "3%";
}

$("#selectAllCheckbox").change(function () {
    let sumamount = 0.00;
    for (var i = 0; i < $(".table_checkbox").length; i++) {
        $(".table_checkbox")[i].checked = $("#selectAllCheckbox")[0].checked;
    }
    if ($("#selectAllCheckbox")[0].checked == true) {
        for (let i = 0; i < $(".table_row").length - 1; i++) {
            sumamount += parseFloat($("#" + i + "_amount")[0].outerText);
        }
    }
    else if ($("#selectAllCheckbox")[0].checked == false) {
        sumamount = 0;
    }
    total_amnt = sumamount.toString().substr(0, 8);
    $("#total_amount").text(total_amnt);
    $("#count").attr("value", count);
    $("#amount").attr("value", total_amnt);
});

(function () {
    $("#paymentStatus")
})();

// Father's Name Validation
$("#fathername").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#fathername").addClass(" is-invalid");
            $("#validationFatherName").show();
            $("#fathername").focus();
        }
        else {
            $("#fathername").removeClass(" is-invalid");
            $("#validationFatherName").hide();
        }
    }
    else {
        $("#fathername").removeClass(" is-invalid");
        $("#validationFatherName").hide();
    }
});

// Full Name Validation
$("#Fullname").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#Fullname").addClass(" is-invalid");
            $("#validationFullName").show();
            $("#Fullname").focus();
        }
        else {
            $("#Fullname").removeClass(" is-invalid");
            $("#validationFullName").hide();
        }
    }
    else {
        $("#Fullname").removeClass(" is-invalid");
        $("#validationFullName").hide();
    }
});

// First Name Validation
$("#firstname").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#firstname").addClass(" is-invalid");
            $("#validationFirstName").show();
            $("#firstname").focus();
        }
        else {
            $("#firstname").removeClass(" is-invalid");
            $("#validationFirstName").hide();
        }
    }
    else {
        $("#firstname").removeClass(" is-invalid");
        $("#validationFirstName").hide();
    }
});

// Middle Name Validation
$("#middlename").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#middlename").addClass(" is-invalid");
            $("#validationMiddleName").show();
            $("#middlename").focus();
        }
        else {
            $("#middlename").removeClass(" is-invalid");
            $("#validationMiddleName").hide();
        }
    }
    else {
        $("#middlename").removeClass(" is-invalid");
        $("#validationMiddleName").hide();
    }
});

// Last Name Validation
$("#lastname").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#lastname").addClass(" is-invalid");
            $("#validationLastName").show();
            $("#lastname").focus();
        }
        else {
            $("#lastname").removeClass(" is-invalid");
            $("#validationLastName").hide();
        }
    }
    else {
        $("#lastname").removeClass(" is-invalid");
        $("#validationLastName").hide();
    }
});

// Current PIN Validation
$("#currentpin").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[1-9]{1}[0-9]{5}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#currentpin").addClass(" is-invalid");
            $("#validationCurrentPin").show();
            $("#currentpin").focus();
        }
        else {
            $("#currentpin").removeClass(" is-invalid");
            $("#validationCurrentPin").hide();
        }
    }
    else {
        $("#currentpin").removeClass(" is-invalid");
        $("#validationCurrentPin").hide();
    }
});

// PAN First Name Validation
$("#panfirstname").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#panfirstname").addClass(" is-invalid");
            $("#validationPanFirstName").show();
            $("#panfirstname").focus();
        }
        else {
            $("#panfirstname").removeClass(" is-invalid");
            $("#validationPanFirstName").hide();
        }
    }
    else {
        $("#panfirstname").removeClass(" is-invalid");
        $("#validationPanFirstName").hide();
    }
});

// PAN Middle Name Validation
$("#panmiddlename").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#panmiddlename").addClass(" is-invalid");
            $("#validationPanMiddleName").show();
            $("#panmiddlename").focus();
        }
        else {
            $("#panmiddlename").removeClass(" is-invalid");
            $("#validationPanMiddleName").hide();
        }
    }
    else {
        $("#panmiddlename").removeClass(" is-invalid");
        $("#validationPanMiddleName").hide();
    }
});

// PAN Last Name Validation
$("#panlastname").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#panlastname").addClass(" is-invalid");
            $("#validationPanLastName").show();
            $("#panlastname").focus();
        }
        else {
            $("#panlastname").removeClass(" is-invalid");
            $("#validationPanLastName").hide();
        }
    }
    else {
        $("#panlastname").removeClass(" is-invalid");
        $("#validationPanLastName").hide();
    }
});

// PAN Father's Name Validation
$("#panfathername").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#panfathername").addClass(" is-invalid");
            $("#validationPanFatherName").show();
            $("#panfathername").focus();
        }
        else {
            $("#panfathername").removeClass(" is-invalid");
            $("#validationPanFatherName").hide();
        }
    }
    else {
        $("#panfathername").removeClass(" is-invalid");
        $("#validationPanFatherName").hide();
    }
});

// Aadhaar First Name Validation
$("#aadharfirstname").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#aadharfirstname").addClass(" is-invalid");
            $("#validationAadhaarFirstName").show();
            $("#aadharfirstname").focus();
        }
        else {
            $("#aadharfirstname").removeClass(" is-invalid");
            $("#validationAadhaarFirstName").hide();
        }
    }
    else {
        $("#aadharfirstname").removeClass(" is-invalid");
        $("#validationAadhaarFirstName").hide();
    }
});

// Aadhaar Middle Name Validation
$("#aadharmiddlename").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#aadharmiddlename").addClass(" is-invalid");
            $("#validationAadhaarMiddleName").show();
            $("#aadharmiddlename").focus();
        }
        else {
            $("#aadharmiddlename").removeClass(" is-invalid");
            $("#validationAadhaarMiddleName").hide();
        }
    }
    else {
        $("#aadharmiddlename").removeClass(" is-invalid");
        $("#validationAadhaarMiddleName").hide();
    }
});

// Aadhaar Last Name Validation
$("#aadharlastname").change(function () {
    var inputvalues = $(this).val();
    var regex = /^[a-zA-Z ]{2,30}$/;
    if (inputvalues !== '') {
        if (!regex.test(inputvalues)) {
            $("#aadharlastname").addClass(" is-invalid");
            $("#validationAadhaarLastName").show();
            $("#aadharlastname").focus();
        }
        else {
            $("#aadharlastname").removeClass(" is-invalid");
            $("#validationAadhaarLastName").hide();
        }
    }
    else {
        $("#aadharlastname").removeClass(" is-invalid");
        $("#validationAadhaarLastName").hide();
    }
});

// Aadhaar Number Validation
$("#aadharnumber").change(function () {
    var inputvalues = $(this).val();
    var regexWithSpace = /^[2-9]{1}[0-9]{3}\s{1}[0-9]{4}\s{1}[0-9]{4}$/;
    var regexWithoutSpace = /^[2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}$/;
    if (inputvalues !== '') {
        if (!regexWithSpace.test(inputvalues) && !regexWithoutSpace.test(inputvalues)) {
            $("#aadharnumber").addClass(" is-invalid");
            $("#validationAadhaarNumber").show();
            $("#aadharnumber").focus();
        }
        else {
            $("#aadharnumber").removeClass(" is-invalid");
            $("#validationAadhaarNumber").hide();
        }
    }
    else {
        $("#aadharnumber").removeClass(" is-invalid");
        $("#validationAadhaarNumber").hide();
    }
});
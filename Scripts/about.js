$("#gender_select").val($("#gender").val());
$("#gender_select").change(function () {
    $("#gender").val($("#gender_select").val());
});
$("#gender_select").focus(function () {
    //$("#Product option[value='-1']").removeAttr('disabled');
    $("#gender_select option[value='-1']").attr('disabled', 'disabled');
});
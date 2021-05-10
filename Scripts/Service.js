$("#services").find('option').hide();
$("#apln_services").find('option').hide();
$("#agrm_services").find('option').hide();
$(".button_service").click(function () {
    var id = this.getAttribute("id");
    if (id == "button_general") {
        var header_id = "header_" + id;        
        $("#header-service-centre").hide(300);
        $("#" + header_id).show(100);
    }
    else if (id == "button_application") {
        var header_id = "header_" + id;
        $("#header-service-centre").hide(300);
        $("#" + header_id).show(100);
    }
    else if (id == "button_agreement") {
        var header_id = "header_" + id;
        $("#header-service-centre").hide(300);
        $("#" + header_id).show(100);
    }
    else if (id == "button_loantype") {
        var header_id = "header_" + id;
        $("#header-service-centre").hide(300);
        $("#" + header_id).show(100);
    } 
});
$(".back").click(function () {
    var id = this.getAttribute("id");
    id = id.replace("check", "header");
    $("#" + id).hide(300);
    $("#header-service-centre").show(300);
});

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
});
$("#agreement_select").change(function () {
    var value = $("#agreement_select").val();
    $("#agreement_num").val(value);
});
$("#application_select").change(function () {
    var value = $("#application_select").val();
    $("#application_num").val(value);
});
$("#product_select").change(function () {
    var value = $("#product_select").val();
    $("#product_name").val(value);
});
$("#type").change(function () {
    var value = $("#type").val();
    $("#ser_type").val(value);
});
$("#services").change(function () {
    var value = $("#services").val();
    $("#prd_service").val(value);
});
$("#product_select").change(function () {
    $("#services").find('option').hide();
    var prdId = $("#product_select").val();
    $("#services").find('option[alt="'+prdId+'"]').show();
});
$("#apln_services").change(function () {
    var value = $("#apln_services").val();
    $("#appl_ser").val(value);
});
$("#application_select").change(function () {
    $("#apln_services").find('option').hide();
    var prdId = $("#application_select option:selected").attr("alt");
    $("#apln_services").find('option[alt="' + prdId + '"]').show();
});
$("#aplntype").change(function () {
    var value = $("#aplntype").val();
    $("#aplnser_type").val(value);
});
$("#agrm_type").change(function () {
    var value = $("#agrm_type").val();
    $("#agrmser_type").val(value);
});
$("#agrm_services").change(function () {
    var value = $("#agrm_services").val();
    $("#agrm_ser").val(value);
});
$("#agreement_select").change(function () {
    $("#agrm_services").find('option').hide();
    var prdId = $("#agreement_select option:selected").attr("alt");
    $("#agrm_services").find('option[alt="' + prdId + '"]').show();
});


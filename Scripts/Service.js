$("#services").find('option').hide();
$("#apln_services").find('option').hide();
$("#agrm_services").find('option').hide();
$("#Product").hide();
$("#Application").hide();
$("#Agreement").hide();
$("#Service_Type").hide();
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
$("#relatedto").change(function () {
    var value = $("#relatedto").val();
    $("#related_to").val(value);
});

$("#relatedto").change(function () {
    var val = $("#relatedto").val();
    if (val == "83b0dd07-82e6-4026-8e5d-b43cb4025670") {
        $("#Product").show(); 
        $("#Service_Type").show();
        $("#Application").hide();
        $("#Agreement").hide(); }
    else if (val == "975f084b-fbd0-4e7a-b2bb-f36841c849bd") {
        $("#Agreement").show();
        $("#Service_Type").show();
        $("#Product").hide();
        $("#Application").hide();}
    else if (val == "05d77726-b0ef-4433-8679-3826aefab78b") {
        $("#Application").show();
        $("#Service_Type").show();
        $("#Product").hide();
        $("#Agreement").hide();}
    else if (val == "a9a4ba04-7b52-4565-a64c-6c26c7a67330") {
        $("#Application").hide();
        $("#Service_Type").hide();
        $("#Product").hide();
        $("#Agreement").hide();
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


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
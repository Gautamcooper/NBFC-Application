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
$(".back_button").click(function () {
    var id = this.getAttribute("id");
    id = id.replace("check", "header");
    $("#" + id).hide(300);
    $("#header-service-centre").show(300);
});
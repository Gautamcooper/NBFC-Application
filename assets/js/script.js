var otp = $("#next_clicked").val();
if (otp == "false") {
    $("#main_page").show();
    $("#otp_verify").hide();
}
else {
    $("#main_page").hide();
    $("#otp_verify").show();
}
var way = $("#temp_data").val();
if (way == "login") {
    $("#signup_label").hide(); 
    $("#login_label").show();
}
if (way == "signup") {
    $("#login_label").hide();
    $("#signup_label").show();  
}
$("#login_button").hover(function () {
    $("#login_button").removeClass("btn-outline-primary");
    $("#login_button").addClass("btn-primary");
})
$("#login_button").mouseleave(function () {
    $("#login_button").removeClass("btn-primary");
    $("#login_button").addClass("btn-outline-primary");
})
$("#signup_button").hover(function () {
    $("#signup_button").removeClass("btn-outline-primary");
    $("#signup_button").addClass("btn-primary");
})
$("#signup_button").mouseleave(function () {
    $("#signup_button").removeClass("btn-primary");
    $("#signup_button").addClass("btn-outline-primary");
})



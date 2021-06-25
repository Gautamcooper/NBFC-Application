var err = 0;
$(document).ready(function () {

    var current_fs, next_fs, previous_fs;
    var opacity;

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

});
var i = $("#idvar").val();
if (i != "" && err == 0) {
    if (i == "Button2") {
        current_fs = $("#Button2").parent();
        next_fs = $("#Button2").parent().next();

        //Add Class Active    
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    }
    else if (i == "Button3") {
        current_fs = $("#Button2").parent();
        next_fs = $("#Button2").parent().next();

        //Add Class Active    
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
        current_fs = $("#Button3").parent();
        next_fs = $("#Button3").parent().next();

        //Add Class Active    
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    }

}
var slider = $("#myRange");
slider.on('input', function () {
    $("#TextBox3").val($("#myRange").val());
});
$("#TextBox3").change(function () {
    $("#myRange").val($("#TextBox3").val());
});

var myRegEx = new RegExp('[6-9][0-9]{9}');
$('#mnumber').change(function () {
    var var1 = $('#mnumber').val();
    if (var1 != myRegEx.exec(var1)) {
        alert("Please enter correct Mobile Number");
        $('#mnumber').focus();
    }
});

var myRegExemail = new RegExp('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}');
$('#email').change(function () {
    var var1 = $('#email').val();
    if (myRegExemail.exec(var1) == null) {
        alert("Please enter correct email id");
        $('#email').focus();
    }
});

var myRegExpan = new RegExp('[A-Z]{5}[0-9]{4}[A-Z]{1}');
$("#Pan_number").change(function () {
    var var1 = $('#Pan_number').val();
    if (myRegExpan.exec(var1) == null) {
        alert("Please enter correct Pan Number");
        $('#Pan_number').focus();
    }
})
$("#Button2").click(function () {
    var var1 = $('#mnumber').val();
    if (var1 != myRegEx.exec(var1)) {
        alert("Please enter correct Mobile Number");
        $('#mnumber').focus();
        return false;
    }
    var var2 = $('#email').val();
    if (myRegExemail.exec(var2) == null) {
        alert("Please enter correct email id");
        $('#email').focus();
        return false;
    }

});
if ($("#temp_data").val() == "login") {
    $("#header").hide();
    $("#myRange").hide();
    $("#TextBox3").hide();
    $("#header2").show();
}
else {
    $("#header").show();
    $("#myRange").show();
    $("#TextBox3").show();
    $("#header2").hide();
}
$("#Product").focus(function () {
    //$("#Product option[value='-1']").removeAttr('disabled');
    $("#Product option[value='-1']").attr('disabled','disabled');
});
$("#Industry_type").focus(function () {
    //$("#Product option[value='-1']").removeAttr('disabled');
    $("#Industry_type option[value='-1']").attr('disabled', 'disabled');
});
$("#Reason").focus(function () {
    //$("#Product option[value='-1']").removeAttr('disabled');
    $("#Reason option[value='-1']").attr('disabled', 'disabled');
});
$("#Loan_type").focus(function () {
    //$("#Product option[value='-1']").removeAttr('disabled');
    $("#Loan_type option[value='-1']").attr('disabled', 'disabled');
});
$("#longtermrow").hide();
$("#shorttermrow").hide();
//$("#label_shortterm").hide();
$("#Loan_type").change(function () {
    var loantype = $("#Loan_type").val();
    if (loantype == "short") {
        $("#shorttermrow").show();
        $("#longtermrow").hide();
    }
    else if (loantype == "long") {
        $("#longtermrow").show();
        $("#shorttermrow").hide();
        //$("#label_shortterm").hide();
    }
});
$("#shortterm").focus(function () {
    //$("#Product option[value='-1']").removeAttr('disabled');
    $("#shortterm option[value='-1']").attr('disabled', 'disabled');
});
$("#longterm").focus(function () {
    //$("#Product option[value='-1']").removeAttr('disabled');
    $("#longterm option[value='-1']").attr('disabled', 'disabled');
});
$("#Button1").click(function () {
    if ($("#Loan_type").val() == -1 || ($("#shortterm").val() == "-1" && $("#longterm").val() == "-1") || $("#Pan_number").val() == "" || $("#Monthly_income").val() == "" || $("#Reason").val() == -1 || $("#Industry_type").val() == -1 || $("#Product").val() == -1) {
        alert("Please fill in all the details");
        return false;
    }
})
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
$(".inp-dropdown").each(function () {
    if (this.value != "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).show();
    }
    else if (this.value == "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).hide();
    }
});
$(".inp-dropdown").change(function () {
    if (this.value != "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).show(500);
    }
    else if (this.value == "-1") {
        var id = this.getAttribute("id");
        $("#label_" + id).hide(500);
    }
})
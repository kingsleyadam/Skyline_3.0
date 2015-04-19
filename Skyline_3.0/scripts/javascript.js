function WebForm_OnSubmit() {
    if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
        for (var i in Page_Validators) {
            try {
                var control = document.getElementById(Page_Validators[i].controltovalidate);
                if (!Page_Validators[i].isvalid) {
                    $("#" + control.id).addClass("has-error");
                } else {
                    $("#" + control.id).removeClass("has-error");
                }
            } catch (e) { }
        }
        return false;
    }
    return true;
}

function validateEmailField(source, args) {
    var expr = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/
    if (args.Value.length == 0 || !IsValidExpression(args.Value, expr)) {
        args.IsValid = false;
    } else {
        args.IsValid = true;
    };
}

function validatePhoneField(source, args) {
    var expr = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/
    if (args.Value.length == 0 || !IsValidExpression(args.Value, expr)) {
        args.IsValid = false;
    } else {
        args.IsValid = true;
    };
}

function IsValidExpression(fldValue, expr) {
    return expr.test(fldValue);
};

function openModal() { $('#login').modal('show'); }

function Confirm() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Are you sure?")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}
function WebForm_OnSubmit() {
    if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
        for (var i in Page_Validators) {
            try {
                var control = document.getElementById(Page_Validators[i].controltovalidate);
                if (!Page_Validators[i].isvalid) {
                    if (control.id.toString().indexOf("txt") != -1)
                        control.className = "form-control has-error";
                    else
                        control.className = "form-control has-error";

                    HideLoadingModal();
                } else {
                    if (control.id.toString().indexOf("txt") != -1)
                        control.className = "form-control";
                    else
                        control.className = "form-control";
                }
            } catch (e) { }
        }
        return false;
    }
    return true;
}
function openModal() { $('#login').modal('show'); }
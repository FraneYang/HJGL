function ValidatePage(validationGroups) {
    var list = validationGroups.split('&');
    for (var i = 0; i < Page_Validators.length; i++) {
        var validator = Page_Validators[i];
        if ((validator.validationGroup && ExistsGroup(list, validator.validationGroup))
            || (!validator.validationGroup && ExistsGroup(list, ''))) {
            ValidatorValidate(validator, validator.validationGroup);
            Page_IsValid = Page_IsValid && validator.isvalid;
        }
        else {
            validator.isvalid = true;
            ValidatorUpdateDisplay(validator);
        }
    }
    for (var i = 0; i < list.length; i++) {
        ValidationSummaryOnSubmit(list[i]);
    }
    Page_BlockSubmit = !Page_IsValid;
    return Page_IsValid;
}

function ExistsGroup(list, group) {
    var found = false;
    for (i = 0; i < list.length; i++) {
        if (list[i] == group) {
            found = true;
            break;
        }
    }
    return found;
}
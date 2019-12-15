window.addEventListener("load", function () {
    var questions = document.querySelectorAll('.form__question');

    questions.forEach(function (question) {
        if (question.classList.contains("type_1")) questionnaireHandleTypeOpen(question)
        else if (question.classList.contains("type_2")) questionnaireHandleTypeMultipleChoise(question)
        else if (question.classList.contains("type_3")) questionnaireHandleTypeSelect(question)
        else if (question.classList.contains("type_4")) questionnaireHandleTypeImage(question)
    });
});

function questionnaireHandleTypeOpen() {

}

function questionnaireHandleTypeMultipleChoise(question) {
    var helpers = question.querySelectorAll('.checkbox_helper');
    var checkboxs = question.querySelectorAll('.checkbox');


    for (var i = 0; i < checkboxs.length; i++) {
        handleCheckbox(checkboxs[i], helpers[i]);
    }

    function handleCheckbox(checkbox, helper) {
        checkbox.addEventListener("change", function () {
            if (checkbox.checked) helper.value = checkbox.getAttribute("data-string-value");
            else helper.value = null;
        });
    }
}

function questionnaireHandleTypeSelect() {

}

function questionnaireHandleTypeImage(question) {

}
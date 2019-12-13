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
    //var helper = question.querySelector('.image_helper');
    //var image = question.querySelector('.image');

    //image.addEventListener("change", async function () {
    //    console.log("change")
    //    helper.value = "";

    //    for (var i = 0; i < image.files.length; i++) {
    //        var base64 = await getBase64(image.files[i]);
    //        helper.value += base64 + "--image_divider--";
    //        var img = document.createElement("img");
    //        img.src = base64;
    //        img.width = 100;
    //        insertAfter(image, img);
    //    }
    //});

    //function insertAfter(referenceNode, newNode) {
    //    referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
    //}

    //function getBase64(file) {
    //    return new Promise(function (resolve) {
    //        console.log(file)
    //        var reader = new FileReader();
    //        reader.readAsDataURL(file);
    //        reader.onload = function () {
    //            resolve(reader.result);
    //        };
    //        reader.onerror = function (error) {
    //            console.log('Error: ', error);
    //        };
    //    })
    //}
}
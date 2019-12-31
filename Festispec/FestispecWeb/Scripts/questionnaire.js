window.addEventListener("load", function() {
    var questions = document.querySelectorAll('.form__question');

    questions.forEach(function(question) {
        if (question.classList.contains("type_1")) questionnaireHandleTypeOpen(question)
        else if (question.classList.contains("type_2")) questionnaireHandleTypeMultipleChoise(question)
        else if (question.classList.contains("type_3")) questionnaireHandleTypeSelect(question)
        else if (question.classList.contains("type_4")) questionnaireHandleTypeImage(question)
        else if (question.classList.contains("type_5")) questionnaireHandleTypeTable(question)
    });
});

function questionnaireHandleTypeOpen() {

}

function questionnaireHandleTypeMultipleChoise(question) {
    var helpers = question.querySelectorAll('.checkbox_helper');
    var checkboxs = question.querySelectorAll('.checkbox');

    for (var i = 0; i < checkboxs.length; i++) {
        handleCheckbox(checkboxs[i], helpers[i]);
        console.log(checkboxs[i], helpers[i])
    }

    function handleCheckbox(checkbox, helper) {
        if (checkbox.checked) helper.value = checkbox.getAttribute("data-string-value");
        else helper.value = null;

        checkbox.addEventListener("change", function() {
            if (checkbox.checked) helper.value = checkbox.getAttribute("data-string-value");
            else helper.value = null;
        });
    }
}

function questionnaireHandleTypeSelect() {

}

function questionnaireHandleTypeImage(question) {

}

function questionnaireHandleTypeTable(question) {
    var json = JSON.parse(question.querySelector(".jsonInput").value || "{}")
    var table = question.querySelector("table");
    var body = table.querySelector("tbody");
    var columns = parseInt(table.getAttribute("data-columns"));
    var button = question.querySelector(".AddRow")

    json.body.forEach(data => {
        var row = document.createElement("tr");

        for (var i = 0; i < columns; i++) {
            var td = document.createElement("td");
            var input = document.createElement("input");

            if (i !== 0) {
                input.type = "number";
            }

            input.value = data[i];
            input.onchange = function(e) { HandleTableChange(question) };

            td.appendChild(input);
            row.appendChild(td);
        }

        var td = document.createElement("td");
        var button = document.createElement("button");
        button.innerHTML = "X";
        button.onclick = function(e) { row.remove(); HandleTableChange(question);}

        td.appendChild(button);
        row.appendChild(td);

        body.appendChild(row);
    });

    button.onclick = function(e) {
        e.preventDefault();
        var row = document.createElement("tr");

        for (var i = 0; i < columns; i++) {
            var td = document.createElement("td");
            var input = document.createElement("input");

            if (i !== 0) {
                input.type = "number";
            }

            input.onchange = function(e) { HandleTableChange(question) };

            td.appendChild(input);
            row.appendChild(td);
        }

        var td = document.createElement("td");
        var button = document.createElement("button");
        button.innerHTML = "X";
        button.onclick = function(e) { row.remove(); HandleTableChange(question); }

        td.appendChild(button);
        row.appendChild(td);
        body.appendChild(row);
    }
}

function HandleTableChange(question) {
    var jsonInput = question.querySelector(".jsonInput");
    var data = {
        head: [],
        body: []
    }

    question.querySelectorAll("thead th span").forEach(function(head) {
        data.head.push(head.innerHTML);
    });

    question.querySelectorAll("tbody tr").forEach(function(row) {
        var rowData = [];

        row.querySelectorAll('td input').forEach(function(d) {
            rowData.push(d.value);
        })

        data.body.push(rowData);
    });

    jsonInput.value = JSON.stringify(data);
}
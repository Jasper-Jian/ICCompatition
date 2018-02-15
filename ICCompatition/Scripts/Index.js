﻿
$.get("/Home/GetList", null, DataBind);
function DataBind(List) {
    //This Code For Receive All Data From Controller And Show It In Client Side
    var SetData = $("#SetList");
    for (var i = 0; i < List.length; i++) {
        var Data = "<tr class='row_" + List[i].Id + "'>" +
            "<td>" + List[i].Id + "</td>" +
            "<td>" + List[i].ExerciseName + "</td>" +
            "<td>" + List[i].ExerciseDateTime + "</td>" +
            "<td>" + List[i].DurationInMinutes + "</td>" +
            "</tr>";
        SetData.append(Data);
        $("#LoadingStatus").html(" ");

    }
}

//render data table
$(document).ready(function () {
    $('#GetTable').DataTable({
        "ajax": {
            url: "/Home/GetList",
            type: "Get",
            dataType: "json"
        },
        columns: [
            { data: "Id" },
            { data: "ExerciseName" },
            { data: "ExerciseDateTime" },
            { data: "DurationInMinutes" },
        ],
        order: [[2, "dsce"]]
    });

});
//popup window
function AddNewStudent(Id) {
    $("#Id").val(0);
    $("#form")[0].reset();
    $("#ModalTitle").html("Add New Student");
    $("#MyModal").modal("show");
}
//on click save record button
$("#SaveRecord").click(function () {
    $('.ui.form').form('validate form');       
})
//submission form validation using semantic ui
$("#form").form(
    {
        inline: true,
        on: 'blur',
        fields: {
            ExerciseName: {
            identifier: 'ExerciseName',
                rules: [
                    {
                        type: 'empty',
                        prompt: "Name can't be null"
                    },
                    {
                        type: 'maxLength[100]',
                        prompt: "Could not excess 100 characters"
                    }
                ]
            },
            ExerciseDateTime: {
            identifier: 'ExerciseDateTime',
                rules: [
                    {
                        type: 'empty',
                        prompt: "Date Time can't be null"
                    },
                    {
                        type: 'regExp[/^([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8])))$/]',
                        prompt: 'Please correct date time formate'
                    }
                ]
            },
            DurationInMinutes: {
            identifier: 'DurationInMinutes',
                rules: [
                    {
                        type: 'empty',
                        prompt: "Duration In Minutes can't be null"
                    },
                    {
                        type: 'regExp[/^(?:[1-9][0-9]?|1[01][0-9]|120)$/]',
                        prompt: 'Please correct duration in 1~120 range'
                    }
                ]
            }
        },
            onSuccess: function (event, fields) {
                $('#info').html("on success");
                event.preventDefault();
                add();
    }
});
// add the record using ajax
function add() {
    var data = $("#form").serialize();
    $.ajax({
    type: "post",
        url: "/Home/AddExercise",
        data: data,
        success: function (result) {
        window.location.href = "/Home/index";
        $("#MyModal").modal("hide");
        }
    })
}
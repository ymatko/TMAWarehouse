﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        order: [[0, 'desc']],
        "ajax": { url: "/item/getall" },
        "columns": [
            { data: 'itemID', "width": "5%" },
            { data: 'name', "width": "10%" },
            { data: 'group', "width": "10%" },
            { data: 'unitOfMeasurement', "width": "10%" },
            { data: 'quantity', "width": "5%" },
            { data: 'priceWithoutVAT', "width": "10%" },
            { data: 'status', "width": "10%" },
            { data: 'storageLocation', "width": "10%" },
            { data: 'contactPerson', "width": "15%" },
            {
                data: 'photoUrl',
                render: function (data) {
                    if (data) {
                        return '<img src="' + data + '" alt="Item Photo" style="max-width: 200px; max-height: 200px;">';
                    } else {
                        return '<img src="https://placehold.co/600x400" alt="Item Photo" style="max-width: 200px; max-height: 200px;">';
                    }
                },
                "width": "10%"
            },
            {
                data: 'itemID',
                "render": function (data) {
                    return '<div class="btn-toolbar" role="toolbar">' +
                        '<div class="btn-group mr-2" role="group">' +
                        '<a href="/item/MakeOrder?itemID=' + data + '" class="btn btn-success"><i class="bi bi-box-arrow-down"></i></a>' +
                        '</div>' +
                        '</div>';
                },
                "width": "1%"
            }
        ]
    });
}
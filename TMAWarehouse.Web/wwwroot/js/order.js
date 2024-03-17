﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        order: [[0, 'desc']],
        "ajax": { url: "/order/getall" },
        "columns": [
            { data: 'requestID', "width": "5%" },
            { data: 'employeeName', "width": "5%" },
            { data: 'itemID', "width": "10%" },
            { data: 'unitOfMeasurement', "width": "10%" },
            { data: 'quantity', "width": "10%" },
            { data: 'priceWithoutVAT', "width": "10%" },
            { data: 'comment', "width": "10%" },
            { data: 'status', "width": "20%" },
            {
                data: 'requestID',
                "render": function (data, type, row) {
                    var disabled = row.status === 'Approve' ? 'disabled' : '';
                    return '<div class="btn-toolbar" role="toolbar">' +
                        '<div class="btn-group mr-2" role="group">' +
                        '<a href="/order/ConfirmOrder?orderId=' + data + '" class="btn btn-dark ' + disabled + '"><i class="bi bi-check-circle-fill"></i>' +
                        '</div>' +
                        '<div class="btn-group" role="group">' +
                        '<a href="/order/OrderDelete?orderId=' + data + '" class="btn btn-danger"><i class="bi bi-trash"></i></a>' +
                        '</div>' +
                        '</div>';
                },
                "width": "30%"
            }
        ]
    });
}

var dataTable;
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
            { data: 'group', "width": "5%" },
            { data: 'unitOfMeasurement', "width": "7%" },
            { data: 'quantity', "width": "5%" },
            { data: 'priceWithoutVAT', "width": "5%" },
            { data: 'status', "width": "10%" },
            { data: 'storageLocation', "width": "10%" },
            { data: 'contactPerson', "width": "15%" },
            {
                data: 'photoUrl',
                render: function (data) {
                    if (data) {
                        return '<img src="' + data + '" alt="Item Photo" style="max-width: 100px; max-height: 100px;">';
                    } else {
                        return '<img src="https://placehold.co/600x400" alt="Item Photo" style="max-width: 100px; max-height: 100px;">';
                    }
                },
                "width": "10%"
            },
            {
                data: 'itemID',
                "render": function (data) {
                    return '<div class="btn-toolbar" role="toolbar">' +
                        '<div class="btn-group mr-2" role="group">' +
                        '<a href="/item/ItemUpdate?itemID=' + data + '" class="btn btn-success"><i class="bi bi-pencil-square"></i></a>' +
                        '</div>' +
                        '<div class="btn-group" role="group">' +
                        '<a href="/item/ItemDelete?itemID=' + data + '" class="btn btn-danger"><i class="bi bi-trash"></i></a>' +
                        '</div>' +
                        '</div>';
                },
                "width": "20%"
            }
        ]
    });
}
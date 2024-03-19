var dataTable;
$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        order: [[0, 'desc']],
        "ajax": { url: "/auth/getall" },
        "columns": [
            { data: 'id', "width": "25%" },
            { data: 'userName', "width": "5%" },
            { data: 'email', "width": "10%" },
            { data: 'phoneNumber', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    var role = '';
                    $.ajax({
                        url: '/auth/GetRole?userid=' + data,
                        type: 'GET',
                        async: false,
                        success: function (result) {
                            role = result;
                        }
                    });
                    return role;
                },
                "width": "15%"
            },
            {
                data: 'id',
                "render": function (data) {
                    var options = '';

                    options += '<option value="">-role-</option>';
                    options += '<option value="ADMIN">ADMIN</option>';
                    options += '<option value="EMPLOYEE">EMPLOYEE</option>';
                    options += '<option value="COORDINATOR">COORDINATOR</option>';

                    return '<select class="select-role" data-userid="' + data + '">' + options + '</select>';
                },
                "width": "10%"
            }
        ]
    });
    $(document).on('change', '.select-role', function () {
        var userId = $(this).data('userid');
        var newRole = $(this).val();

        $.ajax({
            url: '/auth/ChangeRole',
            type: 'GET',
            data: { userId: userId, newRole: newRole },
            success: function (result) {
                dataTable.ajax.reload(null, false);
            },
            error: function (xhr, status, error) {
                console.error("Error when changing user role:", xhr.responseText);
            }
        });
    });
}


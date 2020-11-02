$(function () {
    var table = $('#product-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách sản phẩm',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                filename: 'Danh sách sản phẩm'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh sách sản phẩm',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#product-table_wrapper');
});


$(function () {
    var table = $('#category-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh mục sản phẩm',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                filename: 'Danh mục sản phẩm'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh mục sản phẩm',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#category-table_wrapper');
});


$(function () {
    var table = $('#manufacturer-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Hãng sản xuất',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                filename: 'Hãng sản xuất'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Hãng sản xuất',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#manufacturer-table_wrapper');
});

$(function () {
    var table = $('#new-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách tin tức',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                filename: 'Tin tức'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Tin tức',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#new-table_wrapper');
});


$(function () {
    var table = $('#like-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách lượt thích',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                filename: 'Danh sách lượt thích'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh sách lượt thích',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#like-table_wrapper');
});


$(function () {
    var table = $('#commemt-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách bình luận',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                filename: 'Danh sách bình luận'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh sách bình luận',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#commemt-table_wrapper');
});

$(function () {
    var table = $('#customer-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: [-1,-2] },
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách thông tin khách hàng',
                exportOptions: {
                    columns: "thead th:not(.noExport)"
                },
                filename: 'Danh sách thông tin khách hàng'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh sách thông tin khách hàng',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#customer-table_wrapper');
});


$(function () {
    var table = $('#user-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 },
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách tài khoản khân viên',
                exportOptions: {
                    columns: "thead th:not(.noExport)"
                },
                filename: 'Danh sách tài khoản nhân viên'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh sách tài khoản khân viên',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        order: [[0, 'desc']]
    });
    table.buttons().containers().insertAfter('#user-table_wrapper');
});

$(function () {
    var table = $('#customer-order-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ], buttons: [
        ],
        order: [[0, 'desc']]
    });
});


$(function () {
    var table = $('#sell-offs-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 },
            { orderable: false, targets: -2 }
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách thanh lý tài sản',
                exportOptions: {
                    columns: "thead th:not(.noExport)"
                },
                filename: 'Danh sách thanh lý tài sản'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh sách thanh lý tài sản',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]
    });
    table.buttons().containers().insertAfter('#sell-offs-table_wrapper');
});


$(function () {
    var table = $('#activity-table').DataTable({
        dom: 'Blfrtip',
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Tất cả"]],
        language: {
            infoEmpty: "Hiển thị 0 bản ghi",
            emptyTable: "Không có dữ liệu",
            lengthMenu: "Hiển thị mỗi _MENU_ kết quả",
            infoFiltered: "(lọc từ _MAX_ total bản ghi)",
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←'
            },
            search: 'Tìm kiếm',
            info: "Hiển thị _START_ - _END_ trong _TOTAL_ kết quả"
        },
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: 'Xuất file Excel ',
                title: 'Danh sách hoạt động',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                filename: 'Danh sách hoạt động'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: 'In',
                title: 'Danh sách hoạt động',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]
    });
    table.buttons().containers().insertAfter('#activity-table_wrapper');
});
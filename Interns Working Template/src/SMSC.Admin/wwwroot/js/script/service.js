$(document).ready( async function () {

    $('#addServiceModal').modal({
        backdrop: 'static',
        keyboard: false
    });

    // Handle close button manually
    $('#btnCloseModal').click(function () {
        $('#addServiceModal').modal('hide');
        window.$("#formAddService").parsley().destroy();
    });

    // Initialize the grid


    initializeGrid();
    window.$("#formAddService").parsley();

    await fetchServiceList();
   // $('#formAddRole').parsley();

    $('#formAddService').submit(async function (event) {
        event.preventDefault();

        if ($('#formAddService').parsley().isValid()) {
            await addService();
            await fetchServiceList();
        }
    });
    $('#formUpdateService').submit(async function (event) {
        event.preventDefault();
        debugger;
        if ($('#formUpdateService').parsley().isValid()) {
            await updateService();
            await fetchServiceList();
            
        }
    });


    // Event handler for button click to get employee by ID
   
});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [
    {
        headerName: '#',
        field: 'serviceId',
        filter: false,
        suppressSizeToFit: false,
        resizable: false,
        menuTabs: [],
    },
    {
        headerName: 'Name',
        field: 'serviceName',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Service Charges',
        field: 'serviceCharges',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'IsActive',
        field: 'isActive',
        filter: "agTextColumnFilter",
        cellRenderer: (data) => {
            if (data.data !== undefined) {
                switch (data.data.isActive) {
                    case true:
                        return '<span class="badge badge-outline-success">Yes</span>';
                    case false:
                        return '<span class="badge badge-outline-danger">No</span>';
                }
            }
            return "";
        }
    },
    {
        headerName: 'Create Date Time',
        field: 'createdDateTime',
        filter: "agTextColumnFilter",
    },
];

// Grid options configuration
const gridOptions = {
    columnDefs: columnDefs,
    rowData: [],
    animateRows: true,
    pagination: true,

    // sets 10 rows per page (default is 100)
    paginationPageSize: 10,
    defaultColDef: {
        resizable: true,
        filter: true,
        sortable: true,
        floatingFilter: true,
        allowContextMenuWithControlKey: true,
        menuTabs: ['filterMenuTab', 'columnsMenuTab', 'generalMenuTab']
    },
    autoGroupColumnDef: {
        minWidth: 200,
    },
    sideBar: {
        toolPanels: ["columns","filters"],
    },
    pivotPanelShow: "always",
    columnDefs: columnDefs,
    getContextMenuItems: function (params) {
        return [
            {
                name: 'Add',
                action: function () {
                    $('#formAddService')[0].reset();
                    $('#addServiceModal').modal('show');
                },
                icon: '<img src="/images/icon_new_rate.png" alt="">',
            },
            {
                name: 'update',
                action: async function () {
                    debugger;
                    const serviceId = params.node.data.serviceId;
                   await GetServiceById(serviceId);

                },
                icon: '<i class="bi bi-pencil-fill"></i>'
            },
            {
                name: 'Delete',
                action: function () {
                    console.log('params' + params.node.data);
                    const row = params.node.data;

                    if (row.serviceId !== null) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Do you want to delete this service?",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, delete it!',
                            cancelButtonText: 'No, cancel!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                debugger;
                                deleteService(row.serviceId);
                                Swal.fire(
                                    'Deleted!',
                                    'The Service has been deleted.',
                                    'success'
                                );
                            } else {
                               
                            }
                        });
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Something went wrong!",
                        });
                    }
                },
                icon: '<img src="/images/icondelete.png" alt="">',
            }
        ];
    },
    onGridReady: function (params) {
        params.api.sizeColumnsToFit();
        fetchServiceList(); // Fetch data when grid is ready
    }
};

// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#ServicesGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}


async function fetchServiceList() {

    var response = await ajaxHelpers.ajaxCall("GET", '/Service/GetServiceList');
    debugger;
    if (response.isSuccess) {
        gridOptions.api.setRowData(response.result);
    }
}

async function addService() {

    var formData = $('#formAddService').serialize();
    debugger;

    var response = await ajaxHelpers.ajaxCall("POST", '/Service/AddService', formData);
    debugger;

    if (response.isSuccess) {

        if (response.result) {

            $('#addServiceModal').modal('hide');

            Swal.fire({
                icon:"success",
                title: "Success",
                text: "Service Added Successfully.",
                imageWidth: 400,
                imageHeight: 200,
            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Service already Exists",
            });
        }



    }

}


async function deleteService(serviceId) {
    const data = { ServiceId: serviceId };
    debugger;
    var response = await ajaxHelpers.ajaxCall("POST", '/Service/DeleteServiceById', data);


    if (response.isSuccess) {

        if (response.result) {


            Swal.fire({
                icon: "success",
                title: "Success!",
                text: "Service deleted successfully.",

            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: 'Service with this name exists'
            });
        }

        await fetchServiceList();

    }

}

async function GetServiceById(serviceId) {
    const jsonData = { ServiceId: serviceId };

    var response = await ajaxHelpers.ajaxCall("GET", '/Service/GetServiceById', jsonData);
    debugger;
    console.log(response)
    if (response.isSuccess) {
        $('#updatedServiceId').val(response.result.serviceId);
        $('#updatedServiceName').val(response.result.serviceName);
        $('#updatedServiceCharges').val(response.result.serviceCharges);

        if (response.result.isActive) {
            $('#updatedIsActiveYes').prop('checked', true);
        } else {
            $('#updatedIsActiveNo').prop('checked', true);
        }

        $('#updateServiceModal').modal('show');

    }

}


async function updateService() {
    debugger;
    formData = $('#formUpdateService').serialize();
    debugger;
    var response = await ajaxHelpers.ajaxCall("POST", '/Service/UpdateService', formData);
    debugger;

    if (response.isSuccess) {

        if (response.result) {

            $('#updateServiceModal').modal('hide');
            Swal.fire({
                icon: "success",
                title: "Success",
                text: "Service Updated Successfully.",
                imageWidth: 400,
                imageHeight: 200,
            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Service with this name  Exists",
            });
        }



    }

}


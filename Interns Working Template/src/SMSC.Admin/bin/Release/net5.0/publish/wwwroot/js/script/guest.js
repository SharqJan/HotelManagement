
var gridOptions, RoomId, GuestId, fileNotRequired = false;

$(document).ready(async function () {
    debugger;
    $('.select2').select2();
    //$('#ParentId').select2();
    RoomId = localStorage.getItem('RoomId');
    GuestId = localStorage.getItem('GuestId');

    if (RoomId != null && GuestId != null) {
        localStorage.removeItem('RoomId');
        localStorage.removeItem('GuestId');
        $('#RoomId').val(RoomId);
        await GetGuestById(GuestId);

    }
    //await GetGuestById(guestId);
    debugger;
    await BindService();
    fetchGuestList();
    await BindGuest();
    

   


    $('#addGuestModal').modal({
        backdrop: 'static',
        keyboard: false
    });

    // Handle close button manually
    $('#btnCloseModal').click(function () {
        $('#addRoleModal').modal('hide');
        window.$("#formAddRole").parsley().destroy();
    });

    // Initialize the grid


    initializeGrid();
    window.$("#formAddGuest").parsley();



    $('#formAddGuest').submit(async function (event) {
        event.preventDefault();

        if ($('#formAddGuest').parsley().isValid()) {
            await addGuest();
            await fetchGuestList();
            
        }
    });
    $('#formUpdateGuest').submit(async function (event) {
        event.preventDefault();
        debugger;
        if ($('#formUpdateGuest').parsley().isValid()) {
            await updateGuest();
            await fetchGuestList();
            
        }
    });


    
});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [
    {
        headerName: '#',
        field: 'guestId',
        filter: false,
        suppressSizeToFit: false,
        resizable: false,
        menuTabs: [],
    },
    {
        headerName: 'First Name',
        field: 'firstName',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Last Name',
        field: 'lastName',
        filter: "agTextColumnFilter",
    },

    {
        headerName: 'Email',
        field: 'email',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Phone Number',
        field: 'phoneNo',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Address',
        field: 'address',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Id Card Type',
        field: 'idCardType',
        filter: "agTextColumnFilter",
    },
 /*   {
        headerName: 'parent',
        field: 'parentId',
        filter: "agTextColumnFilter",
    },*/
    {
        headerName: 'Is Active ',
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
];

// Grid options configuration
gridOptions = {
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
        toolPanels: ["columns"],
    },
    pivotPanelShow: "always",
    columnDefs: columnDefs,
    getContextMenuItems: function (params) {
        return [
            {
                name: 'Add',
                action: async function () {

                    await BindGuest();
                    $('#formAddGuest')[0].reset();
                    $('#CardDiv').show();
                    $('#ParentDiv').hide();
                    $("#formAddGuest").parsley().destroy();
                    $("#formAddGuest").parsley();
                    $('.ParentClass').attr('required', 'required');

                   
                    $('#addGuestModal').modal('show');
                },
                icon: '<img src="/images/icon_new_rate.png" alt="">',
            },
            {
                name: 'Update',
                action: async function () {
                    debugger;
                    $('#GuestServices').hide();
                    await BindGuest();
                    $('#formUpdateGuest')[0].reset();
                    $("#formUpdateGuest").parsley().destroy();
                    $("#formUpdateGuest").parsley();
                   


                    const guestId = params.node.data.guestId;
                    await GetGuestById(guestId);

                },
                icon: '<i class="bi bi-pencil-fill"></i>'

            }, 

            {
                name: 'Delete',
                action: function () {
                    console.log('params' + params.node.data);
                    const row = params.node.data;
                    if (row.guestId !== null) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Do you want to delete this Guest?",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, delete it!',
                            cancelButtonText: 'No, cancel!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                debugger;
                                deleteGuest(row.guestId);
                                Swal.fire(
                                    'Deleted!',
                                    'The Guest has been deleted.',
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
        fetchGuestList(); // Fetch data when grid is ready
    }
};

// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#GuestGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

// Function to fetch employee list and set it to the grid


async function fetchGuestList() {
    debugger;
    var response = await ajaxHelpers.ajaxCall("GET", '/Guest/GetGuestList');
    
    if (response.isSuccess) {
        gridOptions.api.setRowData(response.result);
    }
}


async function addGuest() {
    debugger;

    var formData = new FormData();
    $('#formAddGuest').serializeArray().forEach(function (item) {
        formData.append(item.name, item.value);
    });
    debugger;

    var fileInput = $('#IdCard')[0];
    if (fileInput.files.length > 0) {
        formData.append('IdCard', fileInput.files[0]);
    }


    var response = await ajaxHelpers.fileUploadAjax('POST', '/Guest/AddGuest', formData, false, 'json', false)
    debugger;
    if (response.isSuccess) {
        if (response.result) {
            $('#addGuestModal').modal('hide');
            Swal.fire({
                icon: "success",
                text: "Guest Added Successfully.",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
            await BindGuest();
        }
        else {
            Swal.fire({
                icon: "error",
                text: "Email already exists",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
        }
    }

}



// Function to delete selected rows
async function deleteGuest(guestId) {
    const data = { GuestId: guestId };
    debugger;
    var response = await ajaxHelpers.ajaxCall("POST", '/Guest/DeleteGuestById', data);


    if (response.isSuccess) {

        if (response.result) {


            Swal.fire({


                icon: "success",
                title: "Success!",
                text: "Guest deleted successfully.",

            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Oops...",
            });
        }

        await fetchGuestList();

    }

}
    


async function GetGuestById(guestId) {
    debugger;
    const jsonData = { GuestId: guestId };

    var response = await ajaxHelpers.ajaxCall("GET", '/Guest/GetGuestById', jsonData);
    debugger;
    let data = response.result;
    debugger;

    if (response.isSuccess) {

        debugger;
        //length = data.idCard.length;

        $('#GuestId').val(data.guestId);
        $('#updatedParentId').val(data.parentId).trigger('change');
        debugger;
        $('#updatedFirstName').val(data.firstName);
        $('#updatedLastName').val(data.lastName);
        $('#updatedEmail').val(data.email);
        $('#updatedAddress').val(data.address);

        $('#updatedIdCardType').val(data.idCardType).trigger('change');
        $('#updatedPhoneNo').val(data.phoneNo);

        if (data.isActive) {
            $('#updatedIsActiveYes').prop('checked', true);
        } else {
            $('#updatedIsActiveNo').prop('checked', true);
        }

        if (data.assignParent) {
            $('#updatedAssignParentYes').prop('checked', true);
        } else {
            $('#updatedAssignParentNo').prop('checked', true);
        }
        if (data.idCard != null) {
            debugger;
            if (data.idCard.length) {
                $('#updatedIdCard').removeAttr('required');
                $('#uploadedImage').show();
                fileNotRequired = true;

            }
        }
            else {
                $('#uploadedImage').hide();
            }



            if (data.parentId) {

                $('#updatedCardDiv').hide();
                $('#updatedParentDiv').show()
                $('#updatedIdCardType,#updatedIdCard').removeAttr('required');
            }
            else {
                $('#updatedCardDiv').show();
                $('#updatedParentDiv').hide()
            }

            $(`#updatedParentId option[value='${data.guestId}']`).remove();


        if (RoomId != null && GuestId != null) { 
          /*  RoomId = null;
            GuestId = null;*/
            $('#GuestServices').show();

        }


            $('#updateGuestModal').modal('show');

        }

    }


    async function updateGuest() {
        debugger;

        var formData = new FormData();
        $('#formUpdateGuest').serializeArray().forEach(function (item) {
            formData.append(item.name, item.value);
        });

        //Handling MultiSelect 
        ServiceIds = $('#ServiceIds').val().toString();
        if(ServiceIds !='')
          formData.append('ServiceId', ServiceIds);

       // ServiceIds = ServiceIds.join(',');

        debugger;

        var fileInput = $('#updatedIdCard')[0];
        if (fileInput.files.length > 0) {
            formData.append('IdCard', fileInput.files[0]);
        }
        window.$("#formUpdateGuest").parsley();




        var response = await ajaxHelpers.fileUploadAjax('POST', '/Guest/UpdateGuest', formData, false, 'json', false);
        
        debugger;
        if (response.isSuccess) {

            if (response.result) {
                
                $('#updateGuestModal').modal('hide');
                if (RoomId == null && GuestId == null) {
                    Swal.fire({
                        icon: "success",
                        text: "Guest Updated Successfully.",
                        imageWidth: 400,
                        imageHeight: 200,
                        imageAlt: "Custom image"
                    });
                    debugger;
                    fileNotRequired = false;
                }

                await BindGuest();
                if (RoomId != null && GuestId != null) {
                    RoomId = null;
                    GuestId = null;

                    Swal.fire({
                        icon: "success",
                        text: "Room Check-in Successful.",
                        imageWidth: 400,
                        imageHeight: 200,
                        imageAlt: "Custom image"
                    });
                    
                }
            }
            else {
                Swal.fire({
                    icon: "error",
                    text: "Email already exists",
                    imageWidth: 400,
                    imageHeight: 200,
                    imageAlt: "Custom image"
                });
            };

        }

    }



    async function BindGuest() {

        var response = await ajaxHelpers.ajaxCall("GET", '/Guest/GetGuestList');

        if (response.isSuccess) {
            data = response.result;

            activeGuest = data.filter(guest => guest.isActive == true && guest.parentId == 0);
            debugger;

            
            var options = '<option value="" >Select Parent </option>';
            $.each(activeGuest, function (index, guest) {
                options += `<option value="${guest.guestId}">${guest.firstName} ${guest.lastName}  </option>`;

            });
            $('#ParentId,#updatedParentId').empty().append(options);
        }


    }



async function BindService() {

    var response = await ajaxHelpers.ajaxCall("GET", '/Guest/GetServiceList');
    debugger;
    if (response.isSuccess) {
        data = response.result;

        activeService = data.filter(service => service.isActive == true);
        debugger;

        var options;
        $.each(activeService, function (index, service) {
            options += `<option value="${service.serviceId}">${service.serviceName}  </option>`;

        });
        $('#ServiceIds').empty().append(options);
    }


}














    $('input[name="AssignParent"]').on('change', function () {
        var selectedValue = $(this).val();
        debugger;
        if (selectedValue === 'true') {
            $('#CardDiv,#updatedCardDiv').hide();
            $('#ParentDiv,#updatedParentDiv').show();
            $('.ParentClass').removeAttr('required');
            $('#ParentId,#updatedParentId').attr('required', 'required');



        } if (selectedValue === 'false') {
            $('#CardDiv,#updatedCardDiv').show();
            $('#ParentDiv,#updatedParentDiv').hide();
            $('.ParentClass').attr('required', 'required');
            $('#ParentId,#updatedParentId').removeAttr('required');

            //if file is uploaded 
            if (fileNotRequired) {
                $('#updatedIdCard,#updatedIdCardType').removeAttr('required');
            }

            if (!fileNotRequired) {
                $('#updatedIdCard,#updatedIdCardType').attr('required', 'required');
            }
            }
    });



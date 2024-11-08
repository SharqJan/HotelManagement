var fieldCount;
$(document).ready(async function () {
    fetchRoomList();
    debugger;
 /*   window.$("#formAddRoom").parsley().destroy();
    if ($('#formAddRoom').parsley().isValid()) {
        x = 2;
    };*/

    $('#addRoomModal').modal({
        backdrop: 'static',
        keyboard: false
    });

   

    // Initialize the grid


    initializeGrid();



    $('#formAddRoom').submit(async function (event) {
        debugger;
        event.preventDefault();
         $("#formAddRoom").parsley();
        if ($('#formAddRoom').parsley().isValid()) {
            await addRoom();
            fetchRoomList();
            $('#addRoomModal').modal('hide');
        }
    });
    $('#formUpdateRoom').submit(async function (event) {
        event.preventDefault();
        debugger;
        if ($('#formUpdateRoom').parsley().isValid()) {
            await updateRoom();
            await fetchRoomList();
            debugger;
            $('#updateRoomModal').modal('hide');
        }
    });




    //Dynamic Append File Type
    let maxFields = 5;
    let wrapper = $('#file-inputs');
    let updatedWrapper = $('#updatedfile-inputs');
   
    let addButton = $('#add-btn');
    let updatedaddButton = $('#updatedAdd-btn');

    fieldCount = wrapper.find('input[type="file"]').length + 1;
    updatedfieldCount = updatedWrapper.find('input[type="file"]').length + 1;





    $(addButton).click(function () {
        debugger;
        if (fieldCount < maxFields) {
            debugger;
            fieldCount++;
            $(wrapper).append(
                `<div class="input-group mb-3 align-items-center file-input-group">
                <div class="col-md-10">
                    <input type="file" class="form-control" name="RoomImage${fieldCount}" id="RoomImage${fieldCount}" required data-parsley-required-message="Please upload Room Image${fieldCount}">
                </div>
                <div class="col-md-2 text-right">
                    <button type="button" class="btn btn-danger remove-btn">-</button>
                </div>
            </div>`
            );
        }
    });

    $(updatedaddButton).click(function () {
        window.$("#formAddRoom").parsley().destroy();
        if (updatedfieldCount < maxFields) {
            debugger;
            updatedfieldCount++;
            $(updatedWrapper).append(
                `<div class="input-group mb-3 align-items-center file-input-group">
                <div class="col-md-10">
                    <input type="file" class="form-control" name="RoomImage${updatedfieldCount}" id="updatedProfileImage${updatedfieldCount}">
                </div>
                <div class="col-md-2 text-right">
                    <button type="button" class="btn btn-danger updateRemove-btn">-</button>
                </div>
            </div>`
            );
          
        }
    });
    $(wrapper).on("click", ".remove-btn", function () {
        debugger;
        $(this).closest(".file-input-group").remove();
        fieldCount--;
    });

    $(updatedWrapper).on("click", ".updateRemove-btn", function () {
        $(this).closest(".file-input-group").remove();
        updatedfieldCount--;
    });

});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [
    {
        headerName: '#',
        field: 'roomId',
        filter: false,
        suppressSizeToFit: false,
        resizable: false,
        menuTabs: [],
    },
    {
        headerName: 'Room No',
        field: 'roomNo',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Floor No',
        field: 'floorNo',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Room Default Price',
        field: 'roomDefaultPrice',
        filter: "agTextColumnFilter",
    },

    {
        headerName: 'Room Additional Tax',
        field: 'roomAdditionalTax',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Room Status',
        field: 'roomStatus',
        filter: "agTextColumnFilter",
    },
    
    {
        headerName: 'Is Room Available ',
        field: 'isRoomAvailable',
        filter: "agTextColumnFilter",
        cellRenderer: (data) => {
            if (data.data !== undefined) {
                switch (data.data.isRoomAvailable) {
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
        toolPanels: ["columns"],
    },
    pivotPanelShow: "always",
    columnDefs: columnDefs,
    getContextMenuItems: function (params) {
        return [
            {
                name: 'Add',
                action: function () {


                    $('#formAddRoom')[0].reset();
                    $("#formAddRoom").parsley().destroy();
                    $("#formAddRoom").parsley();
                    $("#file-inputs").empty();
                    fieldCount = 1; // Min 1 field is required and is by Default
                    

                    $('#addRoomModal').modal('show');
                },
                icon: '<img src="/images/icon_new_rate.png" alt="">',
            },
            {
                name: 'Update',
                action: async function () {
                    $('#formUpdateRoom')[0].reset();
                    $("#updatedfile-inputs").empty();
                    updatedfieldCount = 1;
                    const roomId = params.node.data.roomId;
                    await GetRoomById(roomId);

                    //$('#updateRoleModal').modal('show');
                },
                   icon: '<i class="bi bi-pencil-fill"></i>'
            },
            {
                name: 'Delete',
                action: function () {
                    console.log('params' + params.node.data);
                    const roomId = params.node.data.roomId;
                    //alert(row.userId);

                    if (roomId !== null) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Do you want to delete this room?",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, delete it!',
                            cancelButtonText: 'No, cancel!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                debugger;
                                deleteRoom(roomId);
                                Swal.fire(
                                    'Deleted!',
                                    'The room has been deleted.',
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
                            footer: '<a href="#">Why do I have this issue?</a>'
                        });
                    }
                },
                icon: '<img src="/images/icondelete.png" alt="">',
            }
        ];
    },
    onGridReady: function (params) {
        params.api.sizeColumnsToFit();
        fetchRoomList(); // Fetch data when grid is ready
    }
};

// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#RoomGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}


// Function to fetch room list and set it to the grid

async function fetchRoomList() {

    var response = await ajaxHelpers.ajaxCall("GET", '/Room/GetRoomList');
    debugger;
    if (response.isSuccess) {
        gridOptions.api.setRowData(response.result);
    }
}




// Function to add New Room 

async function addRoom() {
    debugger;

    var formData = new FormData();
    $('#formAddRoom').serializeArray().forEach(function (item) {
        formData.append(item.name, item.value);
    });
    debugger;

    let maxFiles = 5;
    for (var count = 0; count <= maxFiles; count++) {

        var fileInputElement = $(`#RoomImage${count}`)[0];
        if (fileInputElement && fileInputElement.files.length > 0) {

            formData.append(`ProfileImage${count}`, fileInputElement.files[0]);
        }
    }

    var response = await ajaxHelpers.fileUploadAjax('POST', '/Room/AddRoom', formData, false, 'json', false)
    debugger;
    if (response.isSuccess) {
        if (response.result) {
            $('#addRoomModal').modal('hide');
            Swal.fire({
                icon: "success",
                text: "Room Added Successfully.",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
            //await BindGuest();
            await fetchRoomList();
        }
        else {
            Swal.fire({
                icon: "error",
                text: "Error in Adding Room",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
        }
    }
}


async function deleteRoom(RoomId) {
    const data = { roomId: RoomId };
    debugger;
    var response = await ajaxHelpers.ajaxCall("POST", '/Room/DeleteRoomById', data);

    debugger;
    if (response.isSuccess) {


        if (response.result) {


            Swal.fire({
                icon: "success",
                title: "Success!",
                text: "Room deleted successfully.",

            });
        }
        /*
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: '?'
            });
        }
        */
        await fetchRoomList();

    }

}





//***** */




async function GetRoomById(roomId) {

    const jsonData = { RoomId: roomId };

    var response = await ajaxHelpers.ajaxCall("GET", '/Room/GetRoomById', jsonData);
    debugger;
    let data = response.result;

    if (response.isSuccess) {

        //$('#updatedF').val(data.roleId).trigger('change');
        $('#RoomId').val(data.roomId);
        $('#updatedRoomNo').val(data.roomNo);
        $('#updatedFloorNo').val(data.floorNo);
        $('#updatedRoomAdditionalTax').val(data.roomAdditionalTax);
        $('#updatedRoomDefaultPrice').val(data.roomDefaultPrice);
        $('#updatedRoomStatus').val(data.roomStatus);

        // $('#updatedRoleId').val(response.roleId).trigger('change');



        if (data.isRoomAvailable) {
            $('#updatedIsRoomAvailableYes').prop('checked', true);
        } else {
            $('#updatedIsRoomAvailableNo').prop('checked', true);
        }


        $('#updateRoomModal').modal('show');

    }

}



async function updateRoom() {

    var formData = new FormData();

    var formData = new FormData();
    $('#formUpdateRoom').serializeArray().forEach(function (item) {
        formData.append(item.name, item.value);
    });
    debugger;

    maxFiles = 5;
    for (var count = 1; count <= maxFiles; count++) {

        var fileInputElement = $(`#updatedProfileImage${count}`)[0];
        if (fileInputElement && fileInputElement.files.length > 0) {

            formData.append(`ProfileImage${count}`, fileInputElement.files[0]);
        }
    }


    window.$("#formUpdateRoom").parsley();
    debugger;
    try {
        await $.ajax({
            url: '/Room/UpdateRoom',
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                debugger;
                Swal.fire({
                    title: "Sweet!",
                    text: "Room Updated Successfully.",
                    imageWidth: 400,
                    imageHeight: 200,
                    imageAlt: "Custom image"
                });
                console.log('User added:', response);
                if (response != 'OK : User ID = 0') {
                    debugger;
                    Swal.fire({
                        title: "Success!",
                        text: "Room Updated Successfully.",
                        imageWidth: 400,
                        imageHeight: 200,
                        imageAlt: "Custom image"
                    });
                }
                else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: 'Email  exists'
                    });
                }
            },
            error: function (error) {
                console.error('Error updating User:', error);
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                    footer: '<a href="#">Why do I have this issue?</a>'
                });
            }
        });
    } catch (error) {
        console.error('Error in add User function:', error);
    }

}



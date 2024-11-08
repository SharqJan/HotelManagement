
var gridOptions, flatPicker, UpdatedflatPicker, favouriteNo;

 
$(document).ready(async function () {

    favouriteNo = localStorage.getItem('FavouriteNo');
   // flag = localStorage.getItem('FavouriteNo');
    fetchReservationList();

    if (favouriteNo == 7) {
        $('#ReservationGrid').hide();
        $('#addReservationModal').modal('show');
        localStorage.removeItem('FavouriteNo');
       
       
    }

    flatPicker = flatpickr("#ReservationDateTime", {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
        defaultDate: new Date(),
        minDate : new Date(),
        onChange: function (selectedDates, dateStr, instance) {

            $("#ReservationDateTime input").val(dateStr);
        }
    });
    UpdatedflatPicker = flatpickr("#updatedReservationDateTime", {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
        defaultDate: new Date(),
        minDate : new Date(),
        onChange: function (selectedDates, dateStr, instance) {

            $("#updatedReservationDateTime input").val(dateStr);
        }
    });

    $('#addReservationModal').modal({
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
    window.$("#formAddReservation").parsley();



    $('#formAddReservation').submit(async function (event) {
        event.preventDefault();

        if ($('#formAddReservation').parsley().isValid()) {
            await addReservation();
            await fetchReservationList();

        }
    });
    $('#formUpdateReservation').submit(async function (event) {
        event.preventDefault();
        
        if ($('#formUpdateReservation').parsley().isValid()) {
            await updateReservation();
            await fetchReservationList();

        }
    });

    $('#formUpdateReservationStatus').submit(async function (event) {
        event.preventDefault();
        
        if ($('#formUpdateReservationStatus').parsley().isValid()) {
            await updateReservationStatus();
            await fetchReservationList();

        }
    });

});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [
    {
        headerName: '#',
        field: 'reservationId',
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
        headerName: 'Reservation Date ',
        field: 'reservationDateTime',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Advance Amount ',
        field: 'advanceAmount',
        filter: "agTextColumnFilter",
    },
  
    {
        headerName: 'Status',
        field: 'reservationStatus',
        filter: "agTextColumnFilter",
        cellRenderer: (data) => {
            
            if (data.data !== undefined) {
                switch (data.data.reservationStatus) {
                    case 'Accepted':
                        return '<span class="badge badge-outline-success">Accepted</span>';
                    case 'Cancelled':
                        return '<span class="badge badge-outline-danger">Cancelled</span>';
                    case 'Waiting':
                        return '<span class="badge badge-outline-warning">Waiting</span>';
                }
            }
            return "";
        }
    },
    {
        headerName: 'Created Date Time ',
        field: 'createdDateTime',
        filter: "agTextColumnFilter"
    }
   
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
                action: function () {

                    $('#formAddReservation')[0].reset();
                    
                    
                   
                   
                    $("#formAddReservation").parsley().destroy();
                    $("#formAddReservation").parsley();


                    $('#addReservationModal').modal('show');
                },
                icon: '<img src="/images/icon_new_rate.png" alt="">',
            },
            {
                name: 'Update',
                action: async function () {

                    $('#formUpdateReservation')[0].reset();
                    $("#formUpdateReservation").parsley().destroy();
                    $("#formUpdateReservation").parsley();



                    const guestId = params.node.data.guestId;
                    await GetReservationById(guestId);

                },
                icon: '<i class="bi bi-pencil-fill"></i>'
            },
            {
                name: 'Edit Status',
                action: async function () {
                    debugger;
                    $('#formUpdateReservationStatus')[0].reset();
                    $("#formUpdateReservationStatus").parsley().destroy();
                    $("#formUpdateReservationStatus").parsley();



                    const guestId = params.node.data.guestId;
                    await GetReservationStatusById(guestId);

                },
                icon: '<i class="bi bi-pencil-fill"></i>'
            },
            {
                name: 'Check Inventory',
                action: function () {

                    window.location.href = '/checkInventory';
                    

                },
                icon: '<i class="bi bi-house"></i>'
            },
           
        ];
    },
    onGridReady: function (params) {

        params.api.sizeColumnsToFit();
       
        fetchReservationList(); // Fetch data when grid is ready
    }
};

// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#ReservationGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

// Function to fetch employee list and set it to the grid


async function fetchReservationList() {
    
    var response = await ajaxHelpers.ajaxCall("GET", '/Reservation/GetReservationList');
  
    if (response.isSuccess) {
        gridOptions.api.setRowData(response.result);
    }
}


async function addReservation() {
    

   formData = $('#formAddReservation').serialize();
    
    

    
    var response = await ajaxHelpers.ajaxCall("POST", '/Reservation/AddReservation', formData);
    
    if (response.isSuccess) {
        if (response.result) {

            $('#addReservationModal').modal('hide');
            
            
            Swal.fire({
                icon: "success",
                text: "Reservation Added Successfully.",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });

            if (favouriteNo == 7) {

                favouriteNo = 0;
                localStorage.setItem('Umaid', '7');
                location.href = '/CheckIn'
                

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
        }
        await fetchReservationList();
    }

}



async function GetReservationStatusById(guestId) {
    
    const jsonData = { GuestId: guestId };
    var response = await ajaxHelpers.ajaxCall("GET", '/Reservation/GetReservationById', jsonData);
    
    let data = response.result;
    
    if (response.isSuccess) {

        

          $('#updatedGuestId').val(data.guestId);
          $('#ReservationStatus').val(data.reservationStatus).trigger('change');



       $('#updateReservationStatusModal').modal('show');
    }

}



async function GetReservationById(guestId) {
    
    const jsonData = { GuestId: guestId };
    var response = await ajaxHelpers.ajaxCall("GET", '/Reservation/GetReservationById', jsonData);
    
    let data = response.result;
   
    if (response.isSuccess) {

        debugger;
        
        $('#GuestId').val(data.guestId);
        $('#updatedFirstName').val(data.firstName);
        $('#updatedLastName').val(data.lastName);
        $('#updatedEmail').val(data.email);
        $('#updatedAddress').val(data.address);

        $('#updatedPhoneNo').val(data.phoneNo);
        $('#updatedAdvanceAmount').val(data.advanceAmount);
        

        UpdatedflatPicker.setDate(data.reservationDateTime, true);
        $('#updateReservationModal').modal('show');

    }

}


async function updateReservation() {
    

   formData =  $('#formUpdateReservation').serialize();
     

    
    window.$("#formUpdateReservation").parsley();

    var response = await ajaxHelpers.ajaxCall("POST", '/Reservation/UpdateReservation', formData);
    
    if (response.isSuccess) {

        if (response.result) {
            $('#updateReservationModal').modal('hide');

            Swal.fire({
                icon: "success",
                text: "Reservation Updated Successfully.",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
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


async function updateReservationStatus() {
    

    formData = $('#formUpdateReservationStatus').serialize();


    
    window.$("#formUpdateReservationStatus").parsley();

    var response = await ajaxHelpers.ajaxCall("POST", '/Reservation/UpdateReservationStatus', formData);
    
    if (response.isSuccess) {

        if (response.result) {
            $('#updateReservationStatusModal').modal('hide');

            Swal.fire({
                icon: "success",
                text: "Status Updated Successfully.",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
        }
        else {
            Swal.fire({
                icon: "error",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
        };

    }

}



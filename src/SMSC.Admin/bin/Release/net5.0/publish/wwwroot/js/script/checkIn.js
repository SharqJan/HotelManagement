
var gridOptions, flatPicker, UpdatedflatPicker;


$(document).ready(async function () {

    favNumber = localStorage.getItem('Umaid');
    if (favNumber == 7) {
        // localStorage.removeItem('FavouriteNo');
        localStorage.removeItem('Umaid');
    }
    

    
    flatPicker = flatpickr("#ReservationDateTime", {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
        defaultDate: new Date(),
        minDate: new Date(),
        onChange: function (selectedDates, dateStr, instance) {

            $("#ReservationDateTime input").val(dateStr);
        }
    });
    UpdatedflatPicker = flatpickr("#updatedReservationDateTime", {
        enableTime: true,
        dateFormat: "Y-m-d H:i",
        defaultDate: new Date(),
        minDate: new Date(),
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
    window.$("#formCheckReservation").parsley();


    //imp
    $('#formCheckReservation').submit(async function (event) {
        event.preventDefault();

        firstName = $('#FirstName').val();
        lastName = $('#LastName').val();
        email = $('#Email').val();
        phoneNo = $('#PhoneNo').val();



        debugger;

        if (firstName != '' || lastName != '' || email != '' || phoneNo != '') {
            if ($('#formCheckReservation').parsley().isValid()) {
                await checkReservation();
                // await fetchReservationList();

            }
        }
        else {
            Swal.fire({
                icon: "error",
                title:"Form Field Required",
                text: "Please Enter Atleast 1 Form Field",
               
            });
        }
    });
    $('#formUpdateReservation').submit(async function (event) {
        event.preventDefault();

        if ($('#formUpdateReservation').parsley().isValid()) {
          //  await updateReservation();
            //await fetchReservationList();

        }
    });

    $('#formUpdateReservationStatus').submit(async function (event) {
        event.preventDefault();

        if ($('#formUpdateReservationStatus').parsley().isValid()) {
            await updateReservationStatus();
           // await fetchReservationList();

        }
    });

});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [
    {
        headerName: 'Actions',
        field: 'actions',
        cellRenderer: (params) => {
            // Create the button element
            const button = document.createElement('button');
            button.innerText = 'View Rooms';
            button.style.cssText = `
            background-color: #007bff; 
            color: white; 
            border: none; 
            padding: 0.375rem 0.75rem; 
            font-size: 0.875rem; 
            border-radius: 0.25rem; 
            cursor: pointer; 
            display: flex; 
            align-items: center;
            transition: background-color 0.3s;
        `;
            button.title = 'View available rooms';

            // Add an icon to the button for visual appeal
            const icon = document.createElement('i');
            icon.className = 'bi bi-eye'; // Bootstrap Icons class for eye icon
            icon.style.marginRight = '0.5rem'; // Space between icon and text
            button.prepend(icon);

            // Add click event listener
            button.addEventListener('click', () => {
                const guestId = params.data.guestId;
                localStorage.setItem('GuestId', guestId);
                window.location.href = '/CheckInventory';
            });

            // Add hover effect for better user experience
            button.addEventListener('mouseover', () => {
                button.style.backgroundColor = '#0056b3'; // Darker blue on hover
            });
            button.addEventListener('mouseout', () => {
                button.style.backgroundColor = '#007bff'; // Original color
            });

            // Return the button element
            return button;
        },
        width: 200, // Adjusted width for better button display
        suppressSizeToFit: true
    },

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
    /* {
         headerName: 'Status ',
         field: 'reservationStatus',
         filter: "agTextColumnFilter",
     },*/
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
        toolPanels: ["columns","filters"],
    },
    pivotPanelShow: "always",
    columnDefs: columnDefs,
    getContextMenuItems: function (params) {
        return [
            {
                name: 'Check Reservation',
                action: function () {

                    $('#formCheckReservation')[0].reset();




                    $("#formCheckReservation").parsley().destroy();
                    $("#formCheckReservation").parsley();


                    $('#checkReservationModal').modal('show');
                },
                icon: '<i class="bi bi-search"></i>',
            },
        
        ];
    },
    onGridReady: function (params) {

        params.api.sizeColumnsToFit();

       // fetchReservationList(); // Fetch data when grid is ready
    }
};

// Initialize the grid
 function initializeGrid() {
    const gridDiv = document.querySelector('#CheckInGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}






async function checkReservation() {


    formData = $('#formCheckReservation').serialize();


    debugger;

    var response = await ajaxHelpers.ajaxCall("GET", '/CheckIn/GetCheckReservationList', formData);
    debugger;
    if (response.isSuccess) {
        if (response.result.length) {
            gridOptions.api.setRowData(response.result);
            $('#checkReservationModal').modal('hide');
            localStorage.setItem('CheckInButton', true);

            Swal.fire({
                icon: "success",
                text: "Reservation Available",
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: "Custom image"
            });
           
        }
        else {
            $('#checkReservationModal').modal('hide');


            
            Swal.fire({
                title: "No Reservation Found",
                text: "Please Add a New Reservation !",
                icon: "error",
                showCancelButton: true,
                confirmButtonColor: "#800080", 
                cancelButtonColor: "#d33",
                confirmButtonText: "Add New Reservation"
            }).then((result) => {
                if (result.isConfirmed) {
                    localStorage.setItem('FavouriteNo', '7');
                    location.href = '/Reservation';
                }
            });



           /* Swal.fire({
                title: 'Reservation Not Found',
                icon: 'error',
                text: 'No Reservation Found. Please add a new reservation.',
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: 'Custom image',
                confirmButtonText: 'Add New Reservation',
                confirmButtonColor: '#3085d6',
                cancelButtonText: 'Cancel',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                background: '#f8f9fa', // Light background color
                titleText: 'No Reservation', // Make title bold
                customClass: {
                    title: 'swal-title', // Custom class for title
                    content: 'swal-content', // Custom class for content
                    confirmButton: 'swal-confirm-btn', // Custom class for confirm button
                    cancelButton: 'swal-cancel-btn' // Custom class for cancel button
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    localStorage.setItem('FavouriteNo', '7');
                    location.href = '/Reservation';
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                    // Handle cancel
                }
            });
*/


        

      
        }
    }

}

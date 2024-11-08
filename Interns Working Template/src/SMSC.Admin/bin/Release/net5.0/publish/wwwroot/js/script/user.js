var gridOptions;
$(document).ready(async function () {

      fetchUserList();
     await BindRoles();

    $('#addUserModal').modal({
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
    window.$("#formAddUser").parsley();

    

    $('#formAddUser').submit(async function (event) {
        event.preventDefault();

        if ($('#formAddUser').parsley().isValid()) {
            await addUser();
           await fetchUserList();
            
        }
    });
    $('#formUpdateUser').submit(async function (event) {
        event.preventDefault();
        debugger;
        if ($('#formUpdateUser').parsley().isValid()) {
            await updateUser();
            await fetchUserList();
            
        }
    });


 
});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [
    {
        headerName: '#',
        field: 'userId',
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
        headerName: 'Role',
        field: 'roleName',
        filter: "agTextColumnFilter",
    },
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
        toolPanels: ["columns","filters"],
    },
    pivotPanelShow: "always",
    columnDefs: columnDefs,
    getContextMenuItems: function (params) {
        return [
            {
                name: 'Add',
                action: function () {

                   
                    $('#formAddUser')[0].reset();

                    $('#addUserModal').modal('toggle');
                },
                icon: '<img src="/images/icon_new_rate.png" alt="">',
            },
            {
                name: 'Update',
                action: async function () {
                    $('#formUpdateUser')[0].reset();
                   

                    const userId = params.node.data.userId;
                    await GetUserById(userId);

                    //$('#updateRoleModal').modal('show');
                },
                icon: '<i class="bi bi-pencil-fill"></i>'
            },
            {
                name: 'Delete',
                action: async function () {
                    console.log('params' + params.node.data);
                    const row = params.node.data;

                    if (row.employeeId !== null) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Do you want to delete this User?",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, delete it!',
                            cancelButtonText: 'No, cancel!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                deleteUser(row.userId);
                                Swal.fire(
                                    'Deleted!',
                                    'The User has been deleted.',
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
        fetchUserList(); // Fetch data when grid is ready
    }
};

// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#UserGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}



async function fetchUserList() {

    var response = await ajaxHelpers.ajaxCall("GET", '/User/GetUserList');
    debugger;
    if (response.isSuccess) {
        gridOptions.api.setRowData(response.result);
    }
}



async function addUser() {
    debugger;

    var formData = new FormData();
    $('#formAddUser').serializeArray().forEach(function (item) {
        formData.append(item.name, item.value);
    });
    debugger;

    var fileInput = $('#ProfileImage')[0];
    if (fileInput.files.length > 0) {
        formData.append('ProfileImage', fileInput.files[0]);
    }


    var response = await ajaxHelpers.fileUploadAjax('POST', '/User/AddUser', formData, false, 'json',false)
    debugger;
    if (response.isSuccess) {
        if (response.result) {
            $('#addUserModal').modal('hide');
            await fetchUserList();
            Swal.fire({
                icon: "success",
                text: "User Added Successfully.",
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
        }
    }
  
}



// Function to delete selected rows

async function deleteUser(userId) {
    const data = { userId: userId };
    debugger;
    var response = await ajaxHelpers.ajaxCall("POST", '/User/DeleteUserById', data);


    if (response.isSuccess) {

        if (response.result) {


            Swal.fire({


                icon: "success",
                title: "Success!",
                text: "User deleted successfully.",

            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Oops...",
            });
        }

        await fetchUserList();

    }

}


async function GetUserById(userId) {
    debugger;
    const jsonData = { UserId: userId };

    var response = await ajaxHelpers.ajaxCall("GET", '/User/GetUserById', jsonData);
    
    let data = response.result;

    if (response.isSuccess) {
        $('#updatedRoleId').val(data.roleId).trigger('change');
        $('#updatedUserId').val(data.userId);
        $('#updatedFirstName').val(data.firstName);
        $('#updatedLastName').val(data.lastName);
        $('#updatedEmail').val(data.email);
        $('#updatedPassword').val(data.password);
        $('#updatedPhoneNo').val(data.phoneNo);

        if (data.isActive) {
            $('#updatedIsActiveYes').prop('checked', true);
        } else {
            $('#updatedIsActiveNo').prop('checked', true);
        }
        debugger;
        if (data.profileImage != null) {

            if (data.profileImage.length) {

                $('#uploadedImage').show();

            }
        }
        else {
            $('#uploadedImage').hide();
        }


        $('#updateUserModal').modal('toggle');

    }

}

async function updateUser() {
    debugger;

    var formData = new FormData();
    $('#formUpdateUser').serializeArray().forEach(function (item) {
        formData.append(item.name, item.value);
    });
    debugger;

    var fileInput = $('#updatedProfileImage')[0];
    if (fileInput.files.length > 0) {
        formData.append('ProfileImage', fileInput.files[0]);
    }
    window.$("#formUpdateUser").parsley();

    var response = await ajaxHelpers.fileUploadAjax('POST', '/User/UpdateUser', formData, false, 'json', false)
    debugger;
    if (response.isSuccess) {

        if (response.result) {
            $('#updateUserModal').modal('hide');
            
            Swal.fire({
                icon: "success",
                text: "User Updated Successfully.",
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

 async function BindRoles() {

     var response = await ajaxHelpers.ajaxCall("GET", '/User/GetRoleList');
     debugger;
     if (response.isSuccess) {
         var activeRoles = response.result.filter(role => role.isActive == true);
         debugger;

         var options = '<option value="">Select a role</option>';
         $.each(activeRoles, function (index, role) {
             options += `<option value="${role.roleId}">${role.roleName}</option>`;
         });
         $('#RoleId,#updatedRoleId').empty().append(options);
     } 
  

}


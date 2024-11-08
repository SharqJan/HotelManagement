
$(document).ready(async function () {
   // alert(userName);  //Global
    $('#addRoleModal').modal({
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
    window.$("#formAddRole").parsley();

    $('#formAddRole').parsley();

    $('#formAddRole').submit(async function (event) {
        event.preventDefault();

        if ($('#formAddRole').parsley().isValid()) {
            await addRole();
            await fetchRoleList();
           
        }
    });
    $('#formUpdateRole').submit(async function (event) {
        event.preventDefault();
        debugger;
        if ($('#formUpdateRole').parsley().isValid()) {
            await updateRole();
            await fetchRoleList();
            
        }
    });


  
});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [
    {
        headerName: '#',
        field: 'roleId',
        filter: false,
        suppressSizeToFit: false,
        resizable: false,
        menuTabs: [],
    },
    {
        headerName: 'Name',
        field: 'roleName',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'IsActive',
        field: 'isActive',
        filter: "agTextColumnFilter",
        cellRenderer: (data) => {
            debugger;
            if (data.data !== undefined) {
                switch (data.data.isActive) {
                    case true:
                        return '<span class="badge badge-outline-success">Yes</span>';
                    case false:
                        return '<span class="badge badge-outline-danger">No</span>';
                }
            }
        }
           
    },
    {
        headerName: 'Created Date Time',
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
                    $('#formAddRole')[0].reset();
                    $('#addRoleModal').modal('show');
                },
                icon: '<img src="/images/icon_new_rate.png" alt="">',
            },
            {
                name: 'update',
                action: function () {

                    const roleId = params.node.data.roleId;
                    GetRoleById(roleId);

                },
                icon: '<i class="bi bi-pencil-fill"></i>'

            },
            {
                name: 'Export',
                action: function () {
                    gridOptions.api.exportDataAsExcel({
                        fileName: 'Roles.xlsx' 

                    });
                },
                icon: '<i class="bi bi-send"></i>'
            },

            {
                name: 'Delete',
                action:  function () {
                    console.log('params' + params.node.data);
                    const RoleId = params.node.data.roleId;

                    if (RoleId !== null) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Do you want to delete this role?",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, delete it!',
                            cancelButtonText: 'No, cancel!',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                debugger;
                                deleteRole(RoleId);
                               
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
                }, icon: '<img src="/images/icondelete.png" alt="">',
            }
        ];
    },
    onGridReady: async function (params) {
        params.api.sizeColumnsToFit();
        await fetchRoleList(); 
    }
};

// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#RolesGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

// Function to fetch roles list and set it to the grid
async function fetchRoleList() {

    var response = await ajaxHelpers.ajaxCall("GET", '/Role/GetRoleList');
    debugger;
    if (response.isSuccess) {
        gridOptions.api.setRowData(response.result);
    } 
}


//Add Role
async function addRole() {

    var formData = $('#formAddRole').serialize();
    debugger;

    var response = await ajaxHelpers.ajaxCall("POST", '/Role/AddRole', formData);
    debugger;
   
    if (response.isSuccess) {

        if (response.result) {

            $('#addRoleModal').modal('hide');

            Swal.fire({
                icon:"success",
                title: "Success",
                text: "Role Added Successfully.",
                imageWidth: 400,
                imageHeight: 200,
            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Role already Exists",
            });
        }
        

       
    } 

}






async function GetRoleById(roleId) {
    const jsonData = { RoleId: roleId };

    var response = await ajaxHelpers.ajaxCall("GET", '/Role/GetRoleById', jsonData);
    debugger;
    let data = response.result;

    if (response.isSuccess) {
        $('#updatedRoleId').val(data.roleId);
        $('#updatedRoleName').val(data.roleName);

        if (data.isActive) {
            $('#updatedIsActiveYes').prop('checked', true);
        } else {
            $('#updatedIsActiveNo').prop('checked', true);
        }

        $('#updateRoleModal').modal('show');

    }
        
}



async function updateRole() {
    debugger;
    formData = $('#formUpdateRole').serialize();
    debugger;
    var response = await ajaxHelpers.ajaxCall("POST", '/Role/UpdateRole', formData);
    debugger;

    if (response.isSuccess) {

        if (response.result) {

            $('#updateRoleModal').modal('hide');
            Swal.fire({
                icon:"success",
                title: "Success",
                text: "Role Updated Successfully.",
                imageWidth: 400,
                imageHeight: 200,
            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Role already Exists",
            });
        }



    } 

}

async function deleteRole(roleId) {
    const data = { RoleId: roleId };
    debugger;
    var response = await ajaxHelpers.ajaxCall("POST", '/Role/DeleteRoleById', data);


    if (response.isSuccess) {

        if (response.result) {


            Swal.fire({
                icon: "success",
                title: "Success!",
                text: "Role deleted successfully.",

            });
        }
        if (response.result === 0) {

            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: 'User Exists with this Role'
            });
        }

        await fetchRoleList();

    }

}
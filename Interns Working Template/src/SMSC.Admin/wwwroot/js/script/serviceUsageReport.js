


var startDate, endDate, ServiceType;
$(document).ready(async function () {

   
    await initializeGrid();
    await BindServices();
    await getStartEndDate();

    $('#CustomStatsToolFilter #dateRange').daterangepicker({
        opens: 'left'
    });

   



    $("#CustomStatsToolFilter ").submit(async function (e) {
        e.preventDefault();

        await getStartEndDate();
        await GetReportByServiceType();


    });


});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [

   

    {
        headerName: 'Service Name',
        field: 'serviceName',
        filter: "agTextColumnFilter",
    },

    {
        headerName: 'No of Guests ',
        field: 'noOfGuests',
        filter: "agTextColumnFilter",
    },
 
    {
        headerName: 'Service Revenue',
        field: 'serviceRevenue',
        filter: "agTextColumnFilter",
    },

    {
        headerName: 'Average Service Revenue',
        field: 'averageSpent',
        filter: "agTextColumnFilter",
    },

    {
        headerName: 'Checkin Date',
        field: 'checkInDateTime',
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Checkout Date',
        field: 'checkOutDateTime',
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
        toolPanels: ["columns", "filters", {
            id: 'customFilter',
            labelDefault: 'Custom Filter',
            labelKey: 'customFilter',
            iconKey: 'customFilter',
            toolPanel: 'customToolPanel'
        }

        ],
    },
    components: {
        customToolPanel: CustomStatsToolPanel
    },

    pivotPanelShow: "always",
    columnDefs: columnDefs,
    onGridReady: async function (params) {
        window.$('#CustomStatsToolFilter').closest('.ag-tool-panel-wrapper').css("width", "350px");

        params.api.sizeColumnsToFit();
    }
};
function CustomStatsToolPanel() { }
CustomStatsToolPanel.prototype.init = function () {
    this.eGui = document.createElement('div');
    this.eGui.id = "CustomStatsToolFilter";
    this.eGui.style.width = "100%";
    this.eGui.innerHTML = `${window.$('#serviceUsageReportFilterDiv').html()}`;
};
CustomStatsToolPanel.prototype.getGui = function () {
    return this.eGui;
};
// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#ReportGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}



async function GetReportByServiceType() {

    ServiceName = $('#ServiceType').val();

    var formData = { ServiceName: ServiceName, StartDate: startDate, EndDate: endDate }

    var response = await ajaxHelpers.ajaxCall("GET", '/ServiceUsageReport/GetListByServiceType', formData);

    if (response.isSuccess) {
        if (response.result) {

            if (response.isSuccess) {
             
                //$('#invoiceReportModal').modal('toggle');
                gridOptions.api.setRowData(response.result);
                if (response.result.length > 0) {
                    $('#graphDiv').show();
                    await createChart(response);
                }

                else {
                    $('#graphDiv').hide();
                }
            }
         
        }
        if (response.result.length === 0) {

            Swal.fire({
                icon: "error",
                title: "Error",
                text: "No Data",
            });
        }



    }

}


async function getStartEndDate() {
    ;
    dateRange = $('#CustomStatsToolFilter #dateRange').val();

    if (dateRange != undefined &&  dateRange != '') {
        startDate  = dateRange.split('-')[0];
        endDate    = dateRange.split('-')[1];
    }


   
    if (dateRange == undefined || dateRange == '') {
        
        // startDate = moment().startOf('day').format('YYYY-MM-DD');
        startDate = moment().subtract(7, 'days').startOf('day').format('YYYY-MM-DD');
        endDate   = moment().endOf('day').format('YYYY-MM-DD');

        await GetReportByServiceType();
    }

}





async function BindServices() {

    var response = await ajaxHelpers.ajaxCall("GET", '/ServiceUsageReport/GetServiceList');
    if (response.isSuccess) {
        data = response.result;

        activeService = data.filter(service => service.isActive == true);

        var options = `<option value = '' selected> Choose Service </option>`;
        $.each(activeService, function (index, service) {
            options += `<option value="${service.serviceName}">${service.serviceName}  </option>`;

        });
        $('#ServiceType').empty().append(options);
    }


}




//Create Charts Function
async function createChart(response) {

    const { AgCharts } = agCharts;

    const chartContainerBar = document.getElementById("barAgChart");
    const chartContainerPie = document.getElementById("pieAgChart");
    const chartContainerLine = document.getElementById("lineAgChart");
   // chartContainerLine.style.padding = '20px'; 
    //chartContainerLine.style.backgroundColor = 'aliceblue';

    if (chartContainerBar) {
        // Bar Chart Options
        const barOptions = {
            container: chartContainerBar,
            data: response.result,
            series: [{
                type: "bar",
                xKey: "serviceName",
                yKey: "serviceRevenue",
                stacked: true,
                cornerRadius: 10
            }],
            axes: [
                {
                    type: "category",
                    position: "bottom",
                    title: {
                        text: "Services",
                        font: {
                            weight: 'bold'
                        }
                    },
                },
                {
                    type: "number",
                    position: "left",
                    title: {
                        text: "Service Revenue",
                        font: {
                            weight: 'bold'
                        }
                    },
                },
            ],
            background: {
                fill: "aliceblue",
            },
        };

        // Create Bar Chart
        $('#barAgChart').empty();
        AgCharts.create(barOptions);
    } else {
        console.error("Bar chart container not found");
    }

    if (chartContainerPie) {
        // Pie Chart Options
        const pieOptions = {
            container: chartContainerPie,
            data: response.result,
            series: [{
                type: "pie",
                angleKey: "serviceRevenue",
                legendItemKey: 'serviceName',
                calloutLabelKey: 'serviceName',
                sectorLabelKey: 'serviceRevenue',
                sectorLabel: {
                    color: 'white',
                    fontWeight: 'bold',
                },
            }],
            background: {
                fill: "aliceblue",
            },
        };

        // Create Pie Chart
        $('#pieAgChart').empty();
        AgCharts.create(pieOptions);
    } else {
        console.error("Pie chart container not found");
    }

    if (chartContainerLine) {
        // Line Chart Options
        const lineOptions = {
            container: chartContainerLine,
            data: response.result,
            series: [{
                type: "line",
                xKey: "serviceName",
                yKey: "serviceRevenue",
                label: {
                    enabled: true,
                    fontWeight: 'bold',
                },
                marker: {
                    fill: 'orange',
                    size: 10,
                    stroke: 'black',
                    strokeWidth: 3,
                    shape: 'diamond',
                },
            }],
            axes: [
                {
                    type: "category",
                    position: "bottom",
                    title: {
                        text: "Services",
                        font: {
                            weight: 'bold'
                        }
                    },
                },
                {
                    type: "number",
                    position: "left",
                    title: {
                        text: "Service Revenue",
                        font: {
                            weight: 'bold'
                        }
                    },
                },
            ],
            background: {
                fill: "aliceblue",
            },
          
        };

        // Create Line Chart
        $('#lineAgChart').empty();
        AgCharts.create(lineOptions);
    } else {
        console.error("Line chart container not found");
    }
}



















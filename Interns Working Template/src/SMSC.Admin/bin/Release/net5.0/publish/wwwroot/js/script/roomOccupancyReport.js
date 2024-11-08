

var startDate, endDate,ReportType;
$(document).ready(async function () {

    await initializeGrid();
    await getStartEndDate();


    $('#CustomStatsToolFilter #dateRange').daterangepicker({
        opens: 'left'
    });

 
    $("#CustomStatsToolFilter #DetailsGridMonthPicker").datepicker({
        format: "mm-yyyy",
        viewMode: "months",
        multidate: true,
        minViewMode: "months",
        clearBtn: true,
        beforeShow: function () {
            // Reset the picker on show
            $(this).datepicker('setDate', null);
        }
    }).on('changeDate', function (e) {
        const dates = $(this).datepicker('getDates');

        
        if (dates.length > 2) {
            dates.shift(); 
            $(this).datepicker('setDates', dates);
        }

        // Sort the selected months
        dates.sort((a, b) => a - b);

        // Ensure the first month is always before the second month
        if (dates.length === 2 && dates[0] >= dates[1]) {
            // Reset to the first month if the order is wrong
            $(this).datepicker('setDate', dates[0]);
            $(this).datepicker('setDates', [dates[0]]);
        }
    });


  

    $('#CustomStatsToolFilter #dateRangeDiv').hide();
    $('#CustomStatsToolFilter #DetailsGridMonthPickerDiv').hide();

 
    $("#CustomStatsToolFilter ").submit(async function (e) {
        debugger;
        e.preventDefault();

        await getStartEndDate();
        await DailyInvoiceReport();


    });
    $('#CustomStatsToolFilter #ReportType').on('change', function () {
        debugger;
        ReportType = $('#CustomStatsToolFilter #ReportType').val();
        if (ReportType == 'daily') {
            $('#CustomStatsToolFilter #dateRangeDiv').show();
            $('#CustomStatsToolFilter #DetailsGridMonthPickerDiv').hide();
        }
        if (ReportType == 'monthly') {
            $('#CustomStatsToolFilter #DetailsGridMonthPickerDiv').show();
            $('#CustomStatsToolFilter #dateRangeDiv').hide();
        }

    });

});

// Column definitions for the grid
// Column definitions with filters
const columnDefs = [

    {
        headerName: 'Date',
        field: 'checkInDate',
        filter: "agTextColumnFilter",
        valueFormatter: (params) => {
            if (params.value) {
                const date = moment(params.value);
                debugger;
                console.log(ReportType);
                if (date.isValid()) {
                    const year = date.year();
                    const month = date.month();
                    const day = date.date();

                    if (day !== 0 && month !== 0 && ReportType =='daily') {
                        // If it has day and month
                        return date.format('D MMMM YYYY'); // e.g., '25 September 2024'
                    }
                   else if (day !== 1 && month !== 0 && ReportType == 'monthly') {
                        // If it has day and month
                        return date.format('MMMM YYYY'); // e.g., '25 September 2024'
                    }
                    else if (month !== 0) {
                        // If it has month and year only
                        return date.format('MMMM YYYY'); // e.g., 'September 2024'
                    } else {
                        // If it's just the year
                        return year.toString(); // e.g., '2024'
                    }
                }
            }
            return '';
        }
    },

    {
        headerName: 'Occupied Rooms',
        field: 'occupiedRooms',
        filter: "agTextColumnFilter",
    },
   
    {
        headerName: 'Total Rooms',
        field: 'totalRooms',
        filter: "agTextColumnFilter",
    },
   
    {
        headerName: 'Available Rooms',
        valueGetter: params => params.data.totalRooms - params.data.occupiedRooms,
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'Occupied Percent',
        valueGetter: params => {
            const occupied = params.data.occupiedRooms || 0;
            const total = params.data.totalRooms || 1; 
            return `${((occupied / total) * 100).toFixed(2)}%`; 
        },
        filter: "agTextColumnFilter",
    },
    {
        headerName: 'No of CheckIns',
        field: 'noOfCheckIns',
        filter: "agTextColumnFilter",
    }


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
    this.eGui.innerHTML = `${window.$('#roomOccupancyReportFilterDiv').html()}`;
};
CustomStatsToolPanel.prototype.getGui = function () {
    return this.eGui;
};
// Initialize the grid
function initializeGrid() {
    const gridDiv = document.querySelector('#ReportGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

// Function to fetch roles list and set it to the grid



//Add Role
async function DailyInvoiceReport() {

    minPercent = $('#CustomStatsToolFilter #minPercent').val();
    maxPercent = $('#CustomStatsToolFilter #maxPercent').val();

    var formData = { ReportType: ReportType, StartDate: startDate, EndDate: endDate, MinPercent: minPercent, MaxPercent: maxPercent }


    var response = await ajaxHelpers.ajaxCall("GET", '/RoomOccupancyReport/RoomOccupancyInvoiceReport', formData);

    if (response.isSuccess) {
        debugger;
        if (response.result) {

            if (response.isSuccess) {

                gridOptions.api.setRowData(response.result);
                await createChart(response);

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

    ReportType = $('#CustomStatsToolFilter #ReportType').val()

    //By Default
    if (ReportType == '') {
        ReportType = 'daily';
        startDate = moment().subtract(7, 'days').startOf('day').format('YYYY-MM-DD');
       // startDate = moment().startOf('day').format('YYYY-MM-DD');
        endDate = moment().endOf('day').format('YYYY-MM-DD');

        await DailyInvoiceReport();
    }



    if (ReportType != '') {
        if (ReportType == 'daily') {
            dateRange = $('#CustomStatsToolFilter #dateRange').val();
            startDate = dateRange.split('-')[0];
            endDate = dateRange.split('-')[1];
        }

        if (ReportType == 'monthly') {
            // Get the value from the input
            monthPicker = $('#CustomStatsToolFilter #DetailsGridMonthPicker').val();

            // Split the input into start and end dates
            startDateInput = monthPicker.split(',')[0];
            endDateInput = monthPicker.split(',')[1];

            //If Start date is after end date 
            if (startDateInput.split('-')[0] > endDateInput.split('-')[0]) {   //comparing month nos umaid

                let temp = startDateInput;
                startDateInput = endDateInput;
                endDateInput = temp;
            }


            // Parse the month and year for the start date
            const [startMonth, startYear] = startDateInput.split('-').map(Number);
            startDate = moment({ year: startYear, month: startMonth - 1, date: 1 }).format('YYYY-MM-DD');


            const [endMonth, endYear] = endDateInput.split('-').map(Number);
            endDate = moment({ year: endYear, month: endMonth - 1, date: 1 }).endOf('month').format('YYYY-MM-DD');

        }
    }

   

}


async function createChart(response) {

    if (ReportType == 'daily') {
        response.result.forEach(item => {

            //item.checkInDate = moment(item.checkInDate).format('YYYY-MM-DD');
             item.checkInDate = moment(item.checkInDate).format('D MMM');
        });
    }
    if (ReportType == 'monthly') {
        response.result.forEach(item => {
            item.checkInDate = moment(item.checkInDate).format('MMM');
        });
    }
 
    response.result.forEach(item => {
        item.occupiedPercent = parseFloat(((item.occupiedRooms / item.totalRooms) * 100).toFixed(2));
    });


    const { AgCharts } = agCharts;


    const chartContainerBar = document.getElementById("barAgChart");
    const chartContainerPie = document.getElementById("pieAgChart");
    const chartContainerLine = document.getElementById("lineAgChart");

    if (chartContainerBar) {
        // Bar Chart Options
        const barOptions = {
            container: chartContainerBar, // Container: HTML Element to hold the chart
            data: response.result, // Data to be displayed in the chart
            series: [{
                type: "bar", // Specify bar chart type
                xKey: "checkInDate", // Key for the x-axis
                yKey: "occupiedPercent",
                stacked: true,
                cornerRadius: 10// Key for the y-axis
            }],
            axes: [
                {
                    type: "category",
                    position: "bottom",
                    title: {
                        text: "Date",
                    },


                },
                {
                    type: "number",
                    position: "left",
                    title: {
                        text: "Occupancy Rate (%)",
                    },
                },
            ],
            // Optional background
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
            container: chartContainerPie, // Container: HTML Element to hold the chart
            data: response.result, // Data to be displayed in the chart
            series: [{
                type: "pie", // Specify pie chart type
                angleKey: "occupiedPercent", // Key for the pie chart angles
                legendItemKey: 'checkInDate',
                calloutLabelKey: 'checkInDate',
                sectorLabelKey: 'occupiedPercent',
                sectorLabel: {
                    color: 'white',
                    fontWeight: 'bold',
                },// Key for the legend
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
        // Bar Chart Options
        const lineOptions = {
            container: chartContainerLine,
            data: response.result,
            series: [{
                type: "line",
                xKey: "checkInDate",
                yKey: "occupiedPercent",
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
                        text: "Date",
                    },
                },
                {
                    type: "number",
                    position: "left",
                    title: {
                        text: "Occupancy Rate (%)", 
                    },
                },
            ],
            // Optional background
            background: {
                fill: "aliceblue",
            },
        };

        // Create Bar Chart
        $('#lineAgChart').empty();
        AgCharts.create(lineOptions);
    } else {
        console.error("Line chart container not found");
    }


}
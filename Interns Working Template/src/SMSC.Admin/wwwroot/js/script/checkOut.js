var filter= false,  RoomsByGuestNameRoomNo = [];


$(document).ready(async function () {

    FetchRooms();
    await BindGuest();

    $('#filterCheckOut').submit(async function (event) {
        event.preventDefault();
        await getOccupiedRooms();
    });
});


//For Test Purpose
$('#cancelButton,#cancelBottomButton').click(function () {
    $('#invoiceModal').modal('hide');
    window.location.reload();

});



async function FetchRooms() {

    if (!filter) {

        var response = await ajaxHelpers.ajaxCall("GET", '/CheckInventory/GetRoomList');
        rooms = response.result;
        AvailableRooms = rooms.filter(room => room.isRoomAvailable === false);
    }

       
   
   
    if (filter) {
        AvailableRooms = [];
        $(RoomsByGuestNameRoomNo).each(function (index, item) {
            AvailableRooms.push(item);
        }); 
        
    }

    if (response?.isSuccess || filter) {
        filter = false;
        var images;
        AvailableRooms.forEach(room => {
            images = [room.roomImage1, room.roomImage2, room.roomImage3, room.roomImage4, room.roomImage5];
            const carouselItems = images.map((img, index) => `
            <div class="carousel-item ${index === 0 ? 'active' : ''}">
                <img src="data:image/jpeg;base64,${img}" class="d-block w-100" alt="Room Image ${index + 1}">
            </div>
        `).join('');

            // Create the card HTML
            const cardHtml = `
            <div class="col-md-3 mb-4">
                <div class="card">
                    <div id="carousel-${room.roomId}" class="carousel slide" data-bs-ride="carousel">
                        <div class="carousel-inner">
                            ${carouselItems}
                        </div>
                        <a class="carousel-control-prev" href="#carousel-${room.roomId}" role="button" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </a>
                        <a class="carousel-control-next" href="#carousel-${room.roomId}" role="button" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </a>
                    </div>

                    <div class="card-body">
                     <h5 class="card-title">
                      <i class="bi bi-house-door-fill"></i> Room No : ${room.roomNo}
                        </h5>
                       <p class="card-text">
                       <i class="bi bi-building"></i> Floor No : ${room.floorNo}
                       </p>
                          <p class="card-text">
                          <i class="bi bi-info-circle"></i> Status : ${room.roomStatus}
                       </p>
                     <p class="card-text">
                        <i class="bi bi-cash"></i> Price : ₹${room.roomDefaultPrice.toFixed(2)}
                    </p>
                <p class="card-text">
                 <i class="bi bi-calculator"></i> Additional Tax : ₹${room.roomAdditionalTax.toFixed(2)}
                </p>
              <p class="card-text"><i class="bi bi-calendar-check"></i> Availability :
    <span class="badge ${room.isRoomAvailable ? 'badge-success' : 'badge-danger'}">
    ${room.isRoomAvailable ? 'Available' : 'Not Available'} </span>
    </p>

                   <button class="btn btn-primary view-details me-2" onclick="CheckOutInvoice(${room.roomId})" data-room-id="${room.roomId}">
                <i class="bi bi-check-circle"></i> Checkout
               </button>
                </div>
                   
                       

                 
                </div>
            </div>
        `;

            $('#room-cards-container').append(cardHtml);
        });



    }

}






//These must be Guests who have occupied atleast one room 
async function BindGuest() {

    var response = await ajaxHelpers.ajaxCall("GET", '/CheckOut/GetOccupiedRoomsGuestList');
    debugger;
    if (response.isSuccess) {
        data = response.result;

        var options = '<option value="null" selected >Filter by Guest Name </option>';
        $.each(data, function (index, guest) {
            options += `<option value="${guest.guestId}">${guest.firstName} ${guest.lastName}  </option>`;

        });
        $('#GuestId').empty().append(options);
    }


}




async function getOccupiedRooms() {
    debugger;
    guestId = $('#GuestId').val();
    roomNo = $('#RoomNo').val();

    
    if(guestId != "null" || roomNo != '') {
        var formData = $('#filterCheckOut').serialize();
        debugger;

        var response = await ajaxHelpers.ajaxCall("GET", '/CheckOut/GetOccupiedRooms', formData);
        debugger;



        if (response.isSuccess) {
            debugger;
            if (response.result.length) {

                data = response.result;
                RoomsByGuestNameRoomNo = [];
                $(data).each(function (index, item) {
                    RoomsByGuestNameRoomNo.push(item);
                });
                filter = true;
                $('#room-cards-container').empty();

                await FetchRooms();


            }

            else {
                $('#room-cards-container').empty();
                Swal.fire({
                    icon: "error",
                    text: "No Room Found With These Details",
                    title: "Room Not Found",
                });
            }

        }
    }
    else {
       // $('#room-cards-container').empty();
        Swal.fire({
            icon: "error",
            text: "Please Atleast Select Guest Name or Enter RoomNo",
            title: "Error",
        });

    }
    

}




async function CheckOutInvoice(roomId) {
    const jsonData = { RoomId: roomId };

    var response = await ajaxHelpers.ajaxCall("GET", '/CheckOut/GetInvoiceById', jsonData);
    debugger;
    let data = response.result;

    if (response.isSuccess) {
        $('#FirstName').val(data.firstName);
        $('#LastName').val(data.lastName);
        $('#invoiceNumber').val(data.invoiceNumber);
        $('#amountPaidInAdvance').val(data.amountPaidInAdvance);
        $('#checkinDateTime').val(data.checkinDateTime);
        $('#checkoutDateTime').val(data.checkoutDateTime);
        $('#invoiceDate').val(data.invoiceDate);
        $('#invoiceDueDate').val(data.invoiceDueDate);
        $('#subTotal').val(data.subTotal);
        $('#taxName1').val(data.taxName1);
        $('#taxName2').val(data.taxName2);
        $('#taxName3').val(data.taxName3);
        $('#taxPercentage1').val(data.taxPercentage1);
        $('#taxPercentage2').val(data.taxPercentage2);
        $('#taxPercentage3').val(data.taxPercentage3);
        $('#totalAmount').val(data.totalAmount);


        $('#invoiceModal').modal('show');

    }
}




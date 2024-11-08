var CheckInButton;
$(document).ready(function () {

    FetchRooms();
    CheckInButton = localStorage.getItem('CheckInButton');
    //CheckInButton = true;
    debugger;
    localStorage.removeItem('CheckInButton');
  
});


async function FetchRooms() {
    var response = await ajaxHelpers.ajaxCall("GET", '/CheckInventory/GetRoomList');
    rooms = response.result;
    if (CheckInButton) {
        AvailableRooms = rooms.filter(room => room.isRoomAvailable);
    }

    else {
        AvailableRooms = rooms.filter(room => room.isRoomAvailable || (!room.isAvailable))
    }


    if (response.isSuccess) {
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
    <h5 class="card-title"><i class="bi bi-house-door"></i> Room No : ${room.roomNo}</h5>
    <p class="card-text"><i class="bi bi-building"></i> Floor No : ${room.floorNo}</p>
    <p class="card-text"><i class="bi bi-flag"></i> Status : ${room.roomStatus}</p>
    <p class="card-text"><i class="bi bi-currency-rupee"></i> Price : ₹${room.roomDefaultPrice.toFixed(2)}</p>
    <p class="card-text"><i class="bi bi-currency-exchange"></i> Additional Tax : ₹${room.roomAdditionalTax.toFixed(2)}</p>

    <p class="card-text"><i class="bi bi-calendar-check"></i> Availability : 
    <span class="badge ${room.isRoomAvailable ? 'badge-success' : 'badge-danger'}">
    ${room.isRoomAvailable ? 'Available' : 'Not Available'} </span>
    </p>

    <button class="bi bi-binoculars btn btn-primary view-details me-2" data-room-id="${room.roomId}"> View Details</button>
    ${CheckInButton ? `<button class="bi bi-check-circle btn btn-success check-in" onclick="CallGuest(${room.roomId})"> Check In</button>` : ''}
</div>



                   
                       
                   
                 
                </div>
            </div>
        `;
            // ${room.canCheckIn ? `<button class="btn btn-success check-in" data-room-id="${room.roomId}" onclick="Test(${room.roomId})">Check In</button>` : ''}
            // Append the card to the container
            $('#room-cards-container').append(cardHtml);
        });

        // Event listener for "View Details" buttons
  
    
        $('.view-details').on('click', function () {
            const roomId = $(this).data('room-id');

            const room = AvailableRooms.find(r => r.roomId === roomId);
            debugger;
            images = [room.roomImage1, room.roomImage2, room.roomImage3, room.roomImage4, room.roomImage5];
            // Update modal with room details
            const modalTitle = `Room No: ${room.roomNo}`;
            const carouselItems = images.map((img, index) => `
            <div class="carousel-item ${index === 0 ? 'active' : ''}">
                <img src="data:image/jpeg;base64,${img}" class="d-block w-100" alt="Room Image ${index + 1}">
            </div>
        `).join('');

            $('#roomDetailsModalLabel').text(modalTitle);
            $('#roomsCarousel .carousel-inner-inner').html(carouselItems);

            $('#roomDetails').html(`


                                        <div class="card-body">
    <h5 class="card-title"><i class="bi bi-house-door"></i> Room No : ${room.roomNo}</h5>
    <p class="card-text"><i class="bi bi-building"></i> Floor No : ${room.floorNo}</p>
    <p class="card-text"><i class="bi bi-flag"></i> Status : ${room.roomStatus}</p>
    <p class="card-text"><i class="bi bi-currency-rupee"></i> Price : ₹${room.roomDefaultPrice.toFixed(2)}</p>
    <p class="card-text"><i class="bi bi-currency-exchange"></i> Additional Tax : ₹${room.roomAdditionalTax.toFixed(2)}</p>

    <p class="card-text"><i class="bi bi-calendar-check"></i> Availability : 
    <span class="badge ${room.isRoomAvailable ? 'badge-success' : 'badge-danger'}">
    ${room.isRoomAvailable ? 'Available' : 'Not Available'} </span>
    </p>


</div>
                        
                       
        `);

            // Show the modal
            $('#roomDetailsModal').modal('show');
        });
    }

}

/*
async function FetchRooms() {


    var response = await ajaxHelpers.ajaxCall("GET", '/CheckInventory/GetRoomList');
    debugger;
    rooms = response.result;
    AvailableRooms = rooms.filter(room => room.isRoomAvailable=true)
    if (response.isSuccess) {
        AvailableRooms.forEach(room => { 

            const images = [room.roomImage1, room.roomImage2, room.roomImage3, room.roomImage4, room.roomImage5];
            const carouselItems = images.map((img, index) => `
            <div class="carousel-item ${index === 0 ? 'active' : ''}">
                <img src="data:image/jpeg;base64,${img}" class="d-block w-100" alt="Room Image ${index + 1}">
            </div>
        `).join('');

            // Create the card HTML
            const cardHtml = `
            <div class="col-md-3 mb-4">
                <div class="card">
                    <div id="carousel-${room.roomId}" class="carousel slide" data-ride="carousel">
                        <div class="carousel-inner">
                            ${carouselItems}
                        </div>
                        <a class="carousel-control-prev" href="#carousel-${room.roomId}" role="button" data-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="carousel-control-next" href="#carousel-${room.roomId}" role="button" data-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">Room No: ${room.roomNo}</h5>
                        <p class="card-text">Floor No: ${room.floorNo}</p>
                        <p class="card-text">Status: ${room.roomStatus}</p>
                        <p class="card-text">Price: $${room.roomDefaultPrice.toFixed(2)}</p>
                        <p class="card-text">Additional Tax: $${room.roomAdditionalTax.toFixed(2)}</p>
                        <p class="card-text">Availability: ${room.isRoomAvailable ? "Available" : "Not Available"}</p>
                    </div>
                </div>
            </div>
        `;

            // Append the card to the container
            $('#room-cards-container').append(cardHtml);
        });
    }



 
}
*/

function CallGuest(RoomId) {
    debugger;
    localStorage.setItem('RoomId', RoomId);
    location.href = '/Guest';
   
}


    /*<div class="card-body">
                        <h5 class="card-title">Room No: ${room.roomNo}</h5>
                        <p class="card-text">Floor No: ${room.floorNo}</p>
                        <p class="card-text">Status: ${room.roomStatus}</p>
                        <p class="card-text">Price: $${room.roomDefaultPrice.toFixed(2)}</p>
                        <p class="card-text">Additional Tax: $${room.roomAdditionalTax.toFixed(2)}</p>
                        <p class="card-text">Availability: ${room.isRoomAvailable ? "Available" : "Not Available"}</p>
                        <button class="btn btn-primary view-details" data-room-id="${room.roomId}">View Details</button>
                       <button class="btn btn-success check-in" onclick="CallGuest( ${room.roomId})">Check In</button>


                 
                         <button class="btn btn-primary view-details" data-room-id="${room.roomId}">View Details</button>
                     ${CheckInButton ? `<button class="btn btn-success check-in" onclick="CallGuest(${room.roomId})">Check In</button>` : ''}
                 
                     
                     
                     </div >*/


/* <div class="card-body">
 <h5 class="card-title">Room No : ${room.roomNo}</h5>
 <p class="card-text">Floor No : ${room.floorNo}</p>
 <p class="card-text">Status : ${room.roomStatus}</p>
 <p class="card-text">Price : ₹${room.roomDefaultPrice.toFixed(2)}</p>
 <p class="card-text">Additional Tax : ₹${room.roomAdditionalTax.toFixed(2)}</p>
 
 <p class="card-text">Availability : 
 <span class="badge ${room.isRoomAvailable ? 'badge-success' : 'badge-danger'}">
 ${room.isRoomAvailable ? 'Available' : 'Not Available'} </span>
 </p>
   
<button class="bi bi-binoculars btn btn-primary view-details me-2" data-room-id="${room.roomId}"> View Details</button>
${CheckInButton ? `<button class="btn btn-success check-in" onclick="CallGuest(${room.roomId})">Check In</button>` : ''}

 
 
</div >*/
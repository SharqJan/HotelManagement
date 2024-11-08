
$(document).ready(async function () {
    debugger;
        $('#loginForm').submit(async function (event) {
            event.preventDefault();
            const formData = $(this).serialize();
            var response = await ajaxHelpers.ajaxCall("POST", '/Login/LoginEmail', formData);

            if (response.isSuccess) {

                window.location.href = '/home';


            }
            else {
               /* window.Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message
                });*/
                Swal.fire({
                    icon: "error",
                    title: "Login Failed",
                    text: response.message,
                    width: '250px', 
                    customClass: {
                        popup: 'swal-popup'
                    },
                    position: 'top',
                    backdrop: true,
                    heightAuto: false,
                    maxHeight: '200px' 
                });



            }
     
        });

    });






/*async function Login(){

 
}*/


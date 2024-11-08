
$(document).ready(function() {
    $.ajax({
        url: '/Enum/GetStatuses',
        method: 'GET',
        success: function (data) {
            debugger;
            var $select = $('#statusSelect');
            $.each(data, function(index, item) {
                $select.append($('<option>', {
                    value: item.id,
                    text: item.name
                }));
            });
        },
        error: function() {
            alert('Failed to load statuses');
        }
    });
});

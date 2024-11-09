/* <!--SignalR Integration for Notifications-- >*/




var notificationCount = $("#notificationCount");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7062/notificationHub")
    .withAutomaticReconnect()
    .build();

connection.on("ReceiveNotification", (notification) => {
    console.log(notification);

    const notificationId = `notification-${new Date().getTime()}`;

    const notificationElement = `
                                   <div id="${notificationId}" class="alert alert-primary alert-dismissible fade show" role="alert" style="position: fixed; top: 0; left: 50%; transform: translateX(-50%); width: 50%; z-index: 2000;">
                                          <strong style="font-size: 18px;">${notification.titre}</strong>
                                           <p>${notification.contenu}</p>
                                    </div>
                                          `;

    $("body").append(notificationElement);

    const notificationItem = $('<a>', {
        href: '#',
        class: 'list-group-item list-group-item-action'
    }).append(
        $('<strong>').text(notification.titre),
        notification.contenu,
        $("<small>").text(moment(notification.dateNotif).fromNow())
    );
    $('#notificationList').prepend(notificationItem);

    let currentCount = parseInt(notificationCount.text(), 10) || 0;
    currentCount++;
    notificationCount.text(currentCount);

    setTimeout(function () {
        $(`#${notificationId}`).remove();
    }, 4000);
});

connection.start().then(() => {
    connection.invoke("JoinGroup", '@HttpContextAccessor.HttpContext.Session.GetString("UserType")');
});


$(document).ready(function () {
    var notificationCount = $("#notificationCount");
    function updateNotificationList(data) {
        $("#notificationList").empty(); // Clear the notification list
        console.log("Adding notification:");
        // Loop through each notification and append it to the notification list
        $.each(data, function (index, notification) {
            $("#notificationList").append(`

                <a href="#" class="list-group-item list-group-item-action d-flex justify-content-between align-items-start">
                            <div class="flex-grow-1 text-truncate">
                        <strong>${notification.titre || 'No title'}</strong>
                        <p class="mb-1">${notification.contenu || 'No message'}</p>
                    </div>
                    <small class="text-muted" style="position: absolute; bottom: 10px; right: 10px;">
                        ${moment(notification.dateNotif).fromNow()}
                    </small>
                </a>

                                        `);
        });
    }
    const getLastNotificationsUrl = 'https://localhost:7062/Notification/GetLastNotifications';

    // Fetch the last 5 notifications
    $.getJSON(getLastNotificationsUrl)
        .done(function (data) {
            updateNotificationList(data);
        })
        .fail(function (jqxhr, textStatus, error) {
            console.error("Error fetching notifications:", textStatus, error);
        });



    // When the notification dropdown is clicked, reset the count and reload notifications
    $("#notificationDropdown").click(function () {
        notificationCount.text(0); // Reset the notification count

        $.getJSON(getLastNotificationsUrl)
            .done(function (data) {
                updateNotificationList(data);
            })
            .fail(function (jqxhr, textStatus, error) {
                console.error("Error fetching notifications:", textStatus, error);
            });
    });

    // Redirect to the notification list when "View all notifications" is clicked
    $("#viewAllNotifications").click(function (e) {
        e.preventDefault();
        window.location.href = "/Notification/List";
    });


});

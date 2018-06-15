function readNotifications(user) {
    $.ajax({
        url: "/Users/DeleteNotifications",
        data: { userId: user },
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}

(function() {
    var listOfElements = document.getElementById("displayNotifications");
    var elements = listOfElements.getElementsByTagName("li");

    if (elements.length > 1) {
        var newNotification = document.getElementById("newNotification");
        newNotification.style.display = "inline-block";
    }
})();
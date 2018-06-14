function searchContent() {
    var searchedText = document.getElementById("searchOption").value;
    if (searchedText.length > 2) {
        var displayDivWithSearchOptions = document.getElementById("displayDivWithSearchOptions");
        var listOfElements = document.getElementById("searchOptions");
        var noResult = document.getElementById("noResult");
        var elements = listOfElements.getElementsByTagName("li");
        var count = 0;

        displayDivWithSearchOptions.style.display = "";
        for (var index = 0; index < elements.length; ++index) {
            var link = elements[index].getElementsByTagName("a")[0].innerHTML;
           
            if (link.toLowerCase().indexOf(searchedText.toLowerCase()) > -1) {
                elements[index].style.display = "block";
            } else {
                elements[index].style.display = "none";
                ++count;
            }
        }

        if (count == elements.length) {
            noResult.style.display = "block";
        } else {
            noResult.style.display = "none";
        }
    }
}

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
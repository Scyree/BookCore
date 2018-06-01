function displayFollowers() {
    var followers = document.getElementById("followers");
    var followed = document.getElementById("followed");

    if (followed.style.display === "none") {
        followed.style.display = "block";
        followers.style.display = "none";
    } else {
        followers.style.display = "none";
    }
}

function displayFollowed() {
    var followers = document.getElementById("followers");
    var followed = document.getElementById("followed");

    if (followers.style.display === "none") {
        followers.style.display = "block";
        followed.style.display = "none";
    } else {
        followed.style.display = "none";
    }
}

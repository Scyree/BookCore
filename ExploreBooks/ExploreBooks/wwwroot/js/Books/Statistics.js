function displayRating() {
    var ratings = document.getElementById("rating");
    var chapters = document.getElementById("chapters");

    if (ratings.style.display === "none") {
        ratings.style.display = "block";
        chapters.style.display = "none";
    } else {
        chapters.style.display = "none";
        ratings.style.display = "block";
    }
}

function displayChapters() {
    var ratings = document.getElementById("rating");
    var chapters = document.getElementById("chapters");

    if (chapters.style.display === "none") {
        chapters.style.display = "block";
        ratings.style.display = "none";
    } else {
        ratings.style.display = "none";
        chapters.style.display = "block";
    }
}

function DisplayChaptersModify() {
    var chapters = document.getElementById("displayedChapters");
    var modifyChapters = document.getElementById("modifyChapters");
    
    if (chapters.style.display === "none") {
        chapters.style.display = "block";
        modifyChapters.style.display = "none";
    } else {
        modifyChapters.style.display = "block";
        chapters.style.display = "none";
    }
}

function DisplaySaveOption() {
    var acceptButton = document.getElementById("acceptButton");
    acceptButton.style.display = "block";
}

function RateBook(book, user) {
    var rate = document.getElementById("ratingValue").value;
    $.ajax({
        url: "/Users/RateBook",
        data: { bookId: book, userId: user, value: rate },
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}
function displayAll() {
    var allBooks = document.getElementById("allBooks");
    var planToRead = document.getElementById("planToRead");
    var reading = document.getElementById("reading");
    var read = document.getElementById("read");
    
    if (allBooks.style.display === "none") {
        allBooks.style.display = "block";
        planToRead.style.display = "none";
        reading.style.display = "none";
        read.style.display = "none";
    } else {
        planToRead.style.display = "none";
        reading.style.display = "none";
        read.style.display = "none";
        allBooks.style.display = "block";
    }
}

function displayPlanToRead() {
    var allBooks = document.getElementById("allBooks");
    var planToRead = document.getElementById("planToRead");
    var reading = document.getElementById("reading");
    var read = document.getElementById("read");

    if (planToRead.style.display === "none") {
        planToRead.style.display = "block";
        allBooks.style.display = "none";
        reading.style.display = "none";
        read.style.display = "none";
    } else {
        allBooks.style.display = "none";
        reading.style.display = "none";
        read.style.display = "none";
        planToRead.style.display = "block";
    }
}

function displayReading() {
    var allBooks = document.getElementById("allBooks");
    var planToRead = document.getElementById("planToRead");
    var reading = document.getElementById("reading");
    var read = document.getElementById("read");

    if (reading.style.display === "none") {
        reading.style.display = "block";
        planToRead.style.display = "none";
        allBooks.style.display = "none";
        read.style.display = "none";
    } else {
        planToRead.style.display = "none";
        allBooks.style.display = "none";
        read.style.display = "none";
        reading.style.display = "block";
    }
}

function displayRead() {
    var allBooks = document.getElementById("allBooks");
    var planToRead = document.getElementById("planToRead");
    var reading = document.getElementById("reading");
    var read = document.getElementById("read");

    if (read.style.display === "none") {
        read.style.display = "block";
        planToRead.style.display = "none";
        reading.style.display = "none";
        allBooks.style.display = "none";
    } else {
        planToRead.style.display = "none";
        reading.style.display = "none";
        allBooks.style.display = "none";
        read.style.display = "block";
    }
}
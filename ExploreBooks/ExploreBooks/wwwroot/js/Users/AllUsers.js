function displayCountry() {
    var country = document.getElementById("country");
    var world = document.getElementById("world");

    if (country.style.display === "none") {
        country.style.display = "block";
        world.style.display = "none";
    } else {
        world.style.display = "none";
        country.style.display = "block";
    }
}

function displayWorld() {
    var country = document.getElementById("country");
    var world = document.getElementById("world");

    if (world.style.display === "none") {
        world.style.display = "block";
        country.style.display = "none";
    } else {
        country.style.display = "none";
        world.style.display = "block";
    }
}
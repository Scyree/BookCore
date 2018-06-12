function DisplayProgressModify() {
    var currentPages = document.getElementById("currentPages");
    var modifyPages = document.getElementById("modifyPages");

    if (currentPages.style.display === "none") {
        currentPages.style.display = "block";
        modifyPages.style.display = "none";
    } else {
        modifyPages.style.display = "block";
        currentPages.style.display = "none";
    }
}
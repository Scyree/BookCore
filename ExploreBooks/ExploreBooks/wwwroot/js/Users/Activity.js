function displayComments(target) {
    var posts = document.getElementById(target);

    if (posts.style.display === "none") {
        posts.style.display = "block";
    } else {
        posts.style.display = "none";
    }
}
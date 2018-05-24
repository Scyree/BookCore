function ReadAction(book, user, action) {
    $.ajax({
        url: "/Users/Create",
        data: { bookId: book, userId: user, actionName: action },
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}

function AddFavorite(book, user) {
    $.ajax({
        url: "/Users/AddToFavorites",
        data: { bookId: book, userId: user },
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}

function RemoveFavorite(book, user) {
    $.ajax({
        url: "/Users/RemoveFromFavorites",
        data: { bookId: book, userId: user },
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}
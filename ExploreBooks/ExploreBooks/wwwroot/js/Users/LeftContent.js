function FollowAction(user, followed) {
    $.ajax({
        url: "/Users/FollowUser",
        data: { userId: user, followedId: followed },
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}

function UnfollowAction(user, followed) {
    $.ajax({
        url: "/Users/UnfollowUser",
        data: { userId: user, followedId: followed },
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}
function displayComment(element) {
    var target = document.getElementById(element);

    if (target.style.display === "none") {
        target.style.display = "block";
    } else {
        target.style.display = "none";
    }
}

function Upvote(target, user) {
    $.ajax({
        url: "/Like/Upvote",
        data: { targetId: target, userId: user },
        type: "POST",
        success: function () {
            $.ajax({
                url: "/Like/GetNumberOfLikes",
                data: { targetId: target },
                type: "GET",
                success: function (response) {
                    var targetInput = "#likes" + target;
                    $(targetInput).val(response);
                }
            });
        }
    });
}

function Downvote(target, user) {
    $.ajax({
        url: "/Like/Downvote",
        data: { targetId: target, userId: user },
        type: "POST",
        success: function () {
            $.ajax({
                url: "/Like/GetNumberOfLikes",
                data: { targetId: target },
                type: "GET",
                success: function (response) {
                    var targetInput = "#likes" + target;
                    $(targetInput).val(response);
                }
            });
        }
    });
}
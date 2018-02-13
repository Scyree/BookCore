$(window).on('load', function () {
    $('#externalLoginModal').modal('show');
});

$(".modal").on("hidden.bs.modal", function () {
    window.location = "../Home";
});
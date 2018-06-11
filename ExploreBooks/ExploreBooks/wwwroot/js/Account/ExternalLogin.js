$(window).on('load', function () {
    $('#externalLoginModal').modal('show');
});

$(".modal").on("hidden.bs.modal", function () {
    window.location.href = "/Home/Index";
});
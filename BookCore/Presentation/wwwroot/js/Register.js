$(window).on('load', function () {
    $('#registerModal').modal('show');
});

$(".modal").on("hidden.bs.modal", function () {
    window.location = "../Home";
});
$(window).on('load', function () {
    $('#createBook').modal('show');
    $('#editBook').modal('show');
});

$(".editModal").on("hidden.bs.modal", function () {
    window.location.replace(document.referrer);
});

$(".createModal").on("hidden.bs.modal", function () {
    window.location.replace(document.referrer);
});
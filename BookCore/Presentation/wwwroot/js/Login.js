$(window).on('load',function(){
    $('#loginModal').modal('show');
});

$(".modal").on("hidden.bs.modal", function () {
    window.location = "../Home";
});
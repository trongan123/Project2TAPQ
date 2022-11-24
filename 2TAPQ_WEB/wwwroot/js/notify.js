$(document).ready(function () {
    $(".profile .icon_wrap").click(function () {
        $(this).parent().toggleClass("active");
        $(".notifications").removeClass("active");
       
    });

    $(".notifications .icon_wrap").click(function () {
        $(this).parent().toggleClass("active");
        $(".profile").removeClass("active");
        var a = $("#notify").text();
        if (a > 0) {
           
            $.ajax({
                url: "https://localhost:7126/Pond/readed",
                contentType: "application/json",
                success: function (result, status, xhr) {
                    $("#notify").text(0);
                    console.log("success");
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                }
            });
        }

    });

    $(".show_all .link").click(function () {
        $(".notifications").removeClass("active");
        $(".popup").show();
    });

    $(".close").click(function () {
        $(".popup").hide();
    });

});
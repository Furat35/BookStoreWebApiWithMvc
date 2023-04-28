//AUTHORS
$(document).ready(function () {
    $("body").on("click", "#authors button", function () {
        var page = $(this).attr('value');
        console.log(page);
        $.get("/Admin/Book/GetAuthors?Page=" + page,
            function (data, status) {
                $("#books").html(data);
            });
    });
});


// BOOKS
$(document).ready(function () {
    $("body").on("click", "#books button", function () {
        var page = $(this).attr('value');
        console.log(page);
        $.get("/Admin/Book/GetBooks?Page=" + page,
            function (data, status) {
                $("#books").html(data);
            });
    });
});

// GENRES
$(document).ready(function () {
    $("body").on("click", "#genres button", function () {
        var page = $(this).attr('value');
        console.log(page);
        $.get("/Admin/Book/GetGenres?Page=" + page,
            function (data, status) {
                $("#genres").html(data);
            });
    });
});

// Publishers
$(document).ready(function () {
    $("body").on("click", "#publishers button", function () {
        var page = $(this).attr('value');
        console.log(page);
        $.get("/Admin/Book/GetPublishers?Page=" + page,
            function (data, status) {
                $("#publishers").html(data);
            });
    });
});

// Roles
$(document).ready(function () {
    $("body").on("click", "#roles button", function () {
        var page = $(this).attr('value');
        console.log(page);
        $.get("/Admin/Book/GetRoles?Page=" + page,
            function (data, status) {
                $("#roles").html(data);
            });
    });
});


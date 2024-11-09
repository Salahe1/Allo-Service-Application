// Detect current action (replace with your logic)
$(document).ready(function () {
    // Check if there's an active state stored in session storage
    var activeState = sessionStorage.getItem("activeState");
    if (!activeState) {
        // Set the active state to the first nav item if it's the first page load
        activeState = $(".nav-link:first").attr("id");
        sessionStorage.setItem("activeState", activeState);
    }
    // Set the active class on the corresponding nav item
    $("#" + activeState).parent().addClass("active");

    $(".nav-link").click(function (e) {
        // Remove active class from all nav items
        $(".nav-item").removeClass("active");
        // Add active class to the parent nav item
        $(this).parent().addClass("active");
        // Store the active state in session storage
        sessionStorage.setItem("activeState", $(this).attr("id"));
    });
});

$(function () {
    $('.navbar-toggler').click(function () {
        $('body').toggleClass('noscroll');
    })
});
   
   /* < !--/MENU-JS-->*/

    $(window).on("scroll", function () {
        var scroll = $(window).scrollTop();

        if (scroll >= 80) {
            $("#site-header").addClass("nav-fixed");
        } else {
            $("#site-header").addClass("nav-fixed");/* .removeClass("nav-fixed"); */
        }
    });

//Main navigation Active Class Add Remove
$(".navbar-toggler").on("click", function () {
    $("header").remove("active");

    $("header").toggleClass("active");
});



// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
    scrollFunction()
};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("movetop").style.display = "block";
    } else {
        document.getElementById("movetop").style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}



$(document).ready(function () {
    $('.owl-one').owlCarousel({
        loop: true,
        margin: 0,
        nav: true,
        responsiveClass: true,
        autoplay: false,
        autoplayTimeout: 5000,
        autoplaySpeed: 1000,
        autoplayHoverPause: false,
        responsive: {
            0: {
                items: 1,
                nav: false
            },
            480: {
                items: 1,
                nav: true
            },
            667: {
                items: 1,
                nav: true
            },
            1000: {
                items: 1,
                nav: true
            }
        }
    })
})
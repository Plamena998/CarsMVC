$(document).ready(function () {
    $(document).on("click", ".more-btn", function () {
        var carId = $(this).data("car-id");
        console.log("Button clicked, car id:", carId);

        // Премахва стария модал, ако съществува
        var oldModal = document.getElementById("carModal");
        if (oldModal) {
            var oldInstance = bootstrap.Modal.getInstance(oldModal);
            if (oldInstance) oldInstance.dispose();
            oldModal.remove();
        }

        $.get("/Cars/GetCarModal", { id: carId }, function (data) {
            $("#modalContainer").html(data);

            var modalEl = document.getElementById("carModal");
            if (!modalEl) return;

            var modal = new bootstrap.Modal(modalEl);
            modal.show();

            $(modalEl).one("hidden.bs.modal", function () {
                var modalInstance = bootstrap.Modal.getInstance(modalEl);
                if (modalInstance) modalInstance.dispose();
                modalEl.remove();
            });

        }).fail(function () {
            alert("Failed to load car details.");
        });
    });
});



//helper toast (Bootstrap alert-like) 
function showSuccessMessage(message) {

    //Създава alert елемент (Bootstrap alert класове)
    var $alert = $(
        '<div class="alert alert-success alert-dismissible fade show custom-toast" role="alert">' +
        message +
        '</div>'
    );

    $("body").append($alert);

    //Auto close след 2 секунди
    setTimeout(function () {
        $alert.alert('close');
    }, 2000);

    //Премахва напълно от DOM когато е скрит
    $alert.on('closed.bs.alert', function () {
        $alert.remove();
    });
}
//стар код за like-бутона
////Toggle like (delegated binding)
//$(document).on("click", ".like-btn", function (e) {
//    e.preventDefault();

//    var $btn = $(this);
//    var carId = $btn.data("car-id");

//    //Toggle class
//    $btn.toggleClass("liked");

//    //Toggle aria-pressed for accessibility
//    var pressed = $btn.hasClass("liked");
//    $btn.attr("aria-pressed", pressed ? "true" : "false");

//    //Toggle icon classes (outline <-> fill)
//    var $icon = $btn.find("i");
//    if ($btn.hasClass("liked")) {
//        $icon.removeClass("bi-hand-thumbs-up").addClass("bi-hand-thumbs-up-fill");
//        showSuccessMessage("Колата е добавена към любими!");
//    } else {
//        $icon.removeClass("bi-hand-thumbs-up-fill").addClass("bi-hand-thumbs-up");
//        showSuccessMessage("Колата е премахната от любими.");
//    }

//    try {
//        var key = "favoriteCars";
//        var favorites = JSON.parse(localStorage.getItem(key) || "[]");
//        if ($btn.hasClass("liked")) {
//            // добавя ако не съществува
//            if (favorites.indexOf(carId) === -1) favorites.push(carId);
//        } else {
//            // премахва
//            favorites = favorites.filter(function (id) { return id != carId; });
//        }
//        localStorage.setItem(key, JSON.stringify(favorites));
//    } catch (err) {
//        //ignore localStorage errors in private mode
//        console.warn("localStorage error:", err);
//    }

//    //AJAX запис в бекенда
//    /*
//    $.post("/Favorites/Toggle", { id: carId })
//        .done(function(resp) { /* обработи резултат ако трябва *\/ })
//        .fail(function() { showSuccessMessage("Възникна грешка при добавяне."); });
//    */
//});

// Like button click
$(document).on("click", ".like-btn", function (e) {
    var $btn = $(this);

    // Ако бутонът е за нелогнат потребител -> модалът се показва от Bootstrap, не правим нищо
    if ($btn.attr("data-bs-toggle") === "modal") {
        return;
    }

    e.preventDefault();
    var carId = $btn.data("car-id");

    // Toggle class
    $btn.toggleClass("liked");

    // Toggle aria-pressed
    var pressed = $btn.hasClass("liked");
    $btn.attr("aria-pressed", pressed ? "true" : "false");

    // Toggle icon
    var $icon = $btn.find("i");
    if ($btn.hasClass("liked")) {
        $icon.removeClass("bi-hand-thumbs-up").addClass("bi-hand-thumbs-up-fill");
        showSuccessMessage("Колата е добавена към любими!");
    } else {
        $icon.removeClass("bi-hand-thumbs-up-fill").addClass("bi-hand-thumbs-up");
        showSuccessMessage("Колата е премахната от любими.");
    }

    // LocalStorage
    try {
        var key = "favoriteCars";
        var favorites = JSON.parse(localStorage.getItem(key) || "[]");
        if ($btn.hasClass("liked")) {
            if (!favorites.includes(carId)) favorites.push(carId);
        } else {
            favorites = favorites.filter(id => id != carId);
        }
        localStorage.setItem(key, JSON.stringify(favorites));
    } catch (err) {
        console.warn("localStorage error:", err);
    }

    // AJAX към бекенда (ако имаш endpoint)
    /*
    $.post("/Favorites/Toggle", { id: carId })
        .done(function(resp) { /* обработи резултат *\/ })
        .fail(function() { showSuccessMessage("Възникна грешка при добавяне."); });
    */
});

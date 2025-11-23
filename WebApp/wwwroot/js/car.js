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

// Helper toast (Bootstrap alert-like)
function showSuccessMessage(message) {
    var $alert = $(
        '<div class="alert alert-success alert-dismissible fade show custom-toast" role="alert">' +
        message +
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
        '</div>'
    );

    $("body").append($alert);

    setTimeout(function () {
        $alert.alert('close');
    }, 2000);

    $alert.on('closed.bs.alert', function () {
        $alert.remove();
    });
}

// Like button click
$(document).on("click", ".like-btn", function (e) {
    var $btn = $(this);

    // Ако бутонът е за гост, не правим toggle, Bootstrap модалът се показва автоматично
    if ($btn.attr("data-bs-toggle") === "modal") {
        return; // оставяме Bootstrap да отвори модала
    }

    e.preventDefault();

    var carId = $btn.data("car-id");

    // Toggle визуално състояние
    $btn.toggleClass("liked");
    var pressed = $btn.hasClass("liked");
    $btn.attr("aria-pressed", pressed ? "true" : "false");

    var $icon = $btn.find("i");
    if (pressed) {
        $icon.removeClass("bi-hand-thumbs-up").addClass("bi-hand-thumbs-up-fill");
        showSuccessMessage("Колата е добавена към любими!");
    } else {
        $icon.removeClass("bi-hand-thumbs-up-fill").addClass("bi-hand-thumbs-up");
        showSuccessMessage("Колата е премахната от любими.");
    }

    // AJAX заявка към бекенда за логнати потребители
    $.ajax({
        type: "POST",
        url: "/Favorite/Toggle",
        data: { carId: carId },
        success: function (resp) {
            // resp.liked съдържа новото състояние
            if (!resp.liked && $btn.hasClass("liked")) {
                $btn.removeClass("liked");
                $icon.removeClass("bi-hand-thumbs-up-fill").addClass("bi-hand-thumbs-up");
            }
        },
        error: function () {
            showSuccessMessage("Възникна грешка при добавяне към любими.");
            // Връщаме бутона в предишното състояние
            $btn.toggleClass("liked");
            $icon.toggleClass("bi-hand-thumbs-up bi-hand-thumbs-up-fill");
        }
    });
});

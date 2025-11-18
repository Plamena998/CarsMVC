$(document).ready(function () {
    $(document).on("click", ".more-btn", function () {
        var carId = $(this).data("car-id");
        console.log("Button clicked, car id:", carId);

        // Премахваме стария модал, ако съществува
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

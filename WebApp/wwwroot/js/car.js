$(document).ready(function () {
    // Показва на модала при клик на More
    $(document).on('click', '.more-btn', function () {
        var make = $(this).data('car-make');
        var model = $(this).data('car-model');
        var year = $(this).data('car-year');

        $.ajax({
            url: '/Cars/GetCarModal',
            type: 'GET',
            data: { make: make, model: model, year: year },
            success: function (result) {
                // Вкарва partial view в контейнера
                $('#modalContainer').html(result);

                // Показва модала чрез добавяне на класа 'show'
                $('#carModal').addClass('show');
            },
            error: function () {
                alert('Error loading car modal.');
            }
        });
    });

    // Затваряне при клик на X
    $(document).on('click', '.close-btn', function () {
        const modal = $('#carModal');
        modal.removeClass('show');

        // Премахва елемента след анимацията (0.25s)
        setTimeout(() => modal.remove(), 250);
    });

    // Затваря при клик извън модала
    $(document).on('click', '#carModal', function (e) {
        if ($(e.target).is('#carModal')) {
            const modal = $(this);
            modal.removeClass('show');
            setTimeout(() => modal.remove(), 250);
        }
    });
});

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    const hoursInput = document.getElementById('hoursInput');
    const formGroup = hoursInput.closest('.form-group');

    hoursInput.addEventListener('input', function () {
        const hours = parseInt(hoursInput.value, 10);

        if (isNaN(hours)) {
            formGroup.style.backgroundColor = '';
            return;
        }

        if (hours >= 1 && hours <= 7) {
            formGroup.style.backgroundColor = 'yellow';
        } else if (hours === 8) {
            formGroup.style.backgroundColor = 'green';
        } else if (hours >= 9 && hours <= 24) {
            formGroup.style.backgroundColor = 'red';
        } else {
            formGroup.style.backgroundColor = '';
        }
    });
});
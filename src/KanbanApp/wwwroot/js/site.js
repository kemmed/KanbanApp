window.onload = function () {

    (function () {
        const inputText = document.querySelectorAll('.add-board-label');

        inputText.forEach(function (input) {
            input.addEventListener('focus', function () {
                this.classList.add('focus');
                this.parentElement.querySelector('.add-board-placeholder').classList.add('focus');
            });
            input.addEventListener('blur', function () {
                this.classList.remove('focus');
                if (!this.value) {
                    this.parentElement.querySelector('.add-board-placeholder').classList.remove('focus');
                }
            });
        });
    })();

    (function () {
        const inputText = document.querySelectorAll('.auth-form__input');

        inputText.forEach(function (input) {
            input.addEventListener('focus', function () {
                this.classList.add('focus');
                this.parentElement.querySelector('.auth-form__placeholder').classList.add('focus');
            });
            input.addEventListener('blur', function () {
                this.classList.remove('focus');
                if (!this.value) {
                    this.parentElement.querySelector('.auth-form__placeholder').classList.remove('focus');
                }
            });
        });
    })();

    (function () {
        const togglers = document.querySelectorAll('.password-toggler');

        togglers.forEach(function (checkbox) {
            checkbox.addEventListener('change', function () {

                const toggler = this.parentElement,
                    input = toggler.parentElement.querySelector('.input-password'),
                    icon = toggler.querySelector('.auth-form__icon');

                if (checkbox.checked) {
                    input.type = 'text';
                    icon.classList.remove('la-eye')
                    icon.classList.add('la-eye-slash');
                }

                else {
                    input.type = 'password';
                    icon.classList.remove('la-eye-slash')
                    icon.classList.add('la-eye');
                }
            });
        });
    })();
};
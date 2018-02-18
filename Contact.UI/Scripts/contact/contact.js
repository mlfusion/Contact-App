(function () {
    'use strict';

    // form validation
    // validation Advance elements
    var contact = $('#contactForm').validate({
        ignore: '.ignore',
        rules: {
            firstname: { required: true },
            lastname: { required: true },
            email: { required: true }
        },
        messages: {

        },
        errorElement: 'div',
        errorClass: 'text-danger',
        errorPlacement: function (error, element) {
            var $formGroup = element.closest('.form-group');
            error.appendTo($formGroup);
        },
        highlight: function (element) {
            var $formGroup = $(element).closest('.form-group');

            $formGroup.removeClass('has-info')
                .addClass('has-error');
        },
        unhighlight: function (element) {
            var $formGroup = $(element).closest('.form-group');

            $formGroup.removeClass('has-error')
                .addClass('has-info');
        },
        showErrors: function () {
            var $form = $(this.currentForm),
                errors = this.numberOfInvalids();

            this.defaultShowErrors();

            if (errors) {
                // disable submit button
                $form.find('[type="submit"]').attr('disabled', true);
            } else {
                // enable submit button
                $form.find('[type="submit"]').attr('disabled', false);
            }
        }
    });
    // form validation end

    $('[data-mask="phone_us"]').mask('(000) 000-0000');

    $('#datepicker').datepicker({
        uiLibrary: 'bootstrap4'
    });

    // DATE RANGE PICKER DEMO
    var moment = window.moment;

})(window);
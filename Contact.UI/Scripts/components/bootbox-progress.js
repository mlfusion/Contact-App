

function showLoader() {
    $('.progress_modal').modal('show');
}

function hideLoader() {
    setTimeout(function () {
        $('.progress_modal').modal('hide');
    },1000);      
}

function showSuccessMessage(data, status) {
    try {
        if (data.indexOf('error') >= 0) {
            showErrorMessage(data, status);
        } else {
            showToast(data, 'success');
        }
    } catch (e) {
        showToast(data, 'success');
    }
}

function showErrorMessage(data) {
    console.log(data)
    var d

    if (data !== null) {
        d = data;
    } else {
        d = 'An error occured. Please review console log.';
    }

    showToast(d, 'error');
    }

    function showToast(msg, type) {

        switch (type) {
            case 'success':
                toastr.success(msg);
                break;
            case 'error':
                toastr.error(msg);
                break;
            default:
        }

        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    }


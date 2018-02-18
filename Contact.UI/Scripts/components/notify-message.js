
function successMessage (data) {
    $.notify(data, {
        //position: 'right',
        globalPosition: 'top right',
        className: 'success',
        autoHideDelay: 10000
    });
}

function errorMessage(data) {
    $.notify(data, {
        // position: 'top',
        globalPosition: 'top right',
        className: 'error',
        autoHideDelay: 10000
    });
}

function warnMessage(data) {
    $.notify(data, {
        //position: 'right',
        globalPosition: 'top right',
        className: 'warn',
        autoHideDelay: 10000
    });
}

function infoMessage(data) {
    $.notify(data, {
        //position: 'right',
        globalPosition: 'top right',
        className: 'info',
        autoHideDelay: 10000
    });
}
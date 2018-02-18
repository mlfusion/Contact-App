app.controller('contactController', [
    '$scope', '$window', 'contactService', '$timeout', '$routeParams', '$location', function (scope, window, contactService, timeout, params, location) {

        scope.contacts = [];
        scope.contact = {};
        scope.params = params.id;

        scope.getContacts = function () {

            // show loader
             showLoader();
            
            contactService.getContacts()
                .then(function (data, status) {

                    scope.contacts = data.data;
                    //console.log(scope.contacts)

                    // hide loader
                   hideLoader();

                }), (function (data, status) {

                // log error 
                console.log(data)

                // show notification
                showErrorMessage(null)

                // hide loader
                hideLoader(loader);
                });
        }

        // intit
        scope.getContacts()

        scope.deleteContact = function (id) {
  
            bootbox.confirm('Your you sure you want to delete this contact?',
                function (result) {
                    if (result) {
                        contactService.deleteContact(id)
                            .then(function (data, status) {
                                scope.search = '';
                                showSuccessMessage('Contact has been deleted successfully');
                                // resfresh grid
                                scope.getContacts()
                            }).catch (function (data, status) {

                            // log error
                            console.log(data)

                            // show notification
                            showErrorMessage(null)
                            })
                    }
                });
        }

        scope.addNewContact = function () {
            location.path('contact/add')
        }
    }
]);


app.directive('contactCard', function ($routeParams, contactService) {
    return {
        restrict: 'A',
        templateUrl: 'app/contact/views/contact-card.html',
        scope: {
            contacts: '=',
            addContact: '='
        },
        link: function (scope) {

            scope.search = '';

            scope.addNewContact = function () {
                scope.addContact();
            }

        }
    }
});
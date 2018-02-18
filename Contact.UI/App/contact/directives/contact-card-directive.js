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
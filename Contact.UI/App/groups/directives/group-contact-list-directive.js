
app.directive('groupContactList', function ($routeParams, groupService, $location) {
    return {
        restrict: 'A',
        templateUrl: '../app/groups/views/group-contact-list.html',
        scope: {
            groups: '=',
            contacts: '=',
            groupId: '=',
            getGroups: '&'
        },
        link: function (scope) {

            scope.goToContact = function (id) {
                $location.url('contact/edit/' + id + '/?group=' + scope.groupId)
            }

            scope.deleteGroupContact = function (contactId) {

                bootbox.confirm('Your you sure you want to delete this contact from group?',
                    function (result) {
                        if (result) {
                            groupService.deleteGroupContact(contactId, scope.groupId)
                                .then(function (data, status) {
                                    //console.log(data)
                                    showSuccessMessage(data.data, status);

                                    angular.forEach(scope.contacts, function (item, index) {
                                        if (contactId === item.Id)
                                            scope.contacts.splice(index, 1)
                                    });

                                    // resfresh grid
                                    //scope.getGroups()
                                }).catch(function (data, status) {

                                    // log error
                                    console.log(data)

                                    // show notification
                                    showErrorMessage(null)
                                })
                        }
                    });

            }

        }
    }
});
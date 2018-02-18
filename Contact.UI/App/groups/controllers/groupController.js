app.controller('groupController', [
    '$scope', '$window', 'groupService', '$timeout', '$routeParams', '$location',
    function (scope, window, groupService, timeout, params, location) {

        scope.group = {};
        scope.groups = [];
        scope.contacts = [];
        scope.name = '';
        scope.groupId = ''

        scope.getGroups = function () {

            // show loader
            showLoader();

            groupService.getGroups()
                .then(function (data, status) {
                    
                    scope.groups = data.data;
                    //console.log(data);

                    // hide loader
                    hideLoader();
                }), (function (data, status) {

                    // log error
                    console.log(data)

                    // show error message
                    showErrorMessage(null);

                    // hide loader
                    hideLoader();
                });

        }

        // init scope
        scope.getGroups();

        scope.openEditModal = function (group) {
            scope.group = group;
            $('#edit-modal').modal('show');
        }

        scope.addGroup = function () {

            scope.group = {
                GroupName: scope.name
            }

            groupService.addGroup(scope.group)
                .then(function (data, status) {

                    // display success notification
                    showSuccessMessage('New group has been added successfully.');

                    scope.group = {}
                    scope.name = ''

                    // get groups
                    scope.getGroups();
                }).catch(function (data, status) {

                    // log error
                    console.log(data);

                    // display error notification
                    errorMessage(null);
                })

        }

        scope.getGroup = function (id) {

            scope.groupId = id;
            //console.log('groupid: ' + scope.groupId)

            groupService.getGroup(id)
                .then(function (data, status) {

                   // console.log(data)
                    scope.contacts = data.data;

                }).catch(function (data, status) {

                    // log error
                    console.log(data);

                    // display error notification
                    errorMessage(null);
                });
        }

        scope.getGroupInModal = function (id) {
            location.path('group/contacts/' + id)
        }

        scope.deleteGroup = function (id) {

            bootbox.confirm('Your you sure you want to delete this group?',
                function (result) {
                    if (result) {
                        groupService.deleteGroup(id)
                            .then(function (data, status) {
                             
                                showSuccessMessage(data.data, status);

                                // resfresh grid
                                scope.getGroups()
                            }).catch(function (data, status) {

                                // log error
                                console.log(data.data.Message)

                                // show notification
                                showErrorMessage(data.data.Message)
                            })
                    }
                });
        }


    }]);

app.directive('groupEdit', function ($routeParams, groupService) {
    return {
        restrict: 'A',
        templateUrl: '../app/groups/views/group-edit.html',
        link: function (scope) {

           // console.log(scope.group)

            scope.updateGroup = function () {

                groupService.updateGroup(scope.group)
                    .then(function (data, status) {
                        // display success notification
                        showSuccessMessage('Group name has been updated successfully.');

                        // close modal
                        $('#edit-modal').modal('hide')

                        // get groups
                        scope.getGroups();
                }).catch(function (data, status) {

                    // log error
                    console.log(data);

                    // display error notification
                    showErrorMessage(null);
                })

            }

        }
    }
});

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
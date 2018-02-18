app.controller('groupContactsController', [
    '$scope', '$window', 'groupService', '$timeout', '$routeParams', '$location',
    function (scope, window, groupService, timeout, params, location) {

        scope.id = params.id;
        scope.groupId = ''
        
        scope.getGroupContact = function () {

            // show loader
            showLoader();
            groupService.getGroup(scope.id)
                .then(function (data, status) {

                    //console.log(data)
                    scope.contacts = data.data;

                    // hide loader
                    hideLoader();

                }).catch(function (data, status) {

                    // log error
                    console.log(data);

                    // display error notification
                    showErrorMessage(null);

                    // hide loader
                    hideLoader()
                });
        }

        // init
        scope.getGroupContact();


        scope.goToGroupList = function () {
            location.path('groups')
        }

        scope.goToContact = function (id) {
           // console.log(scope.groupId)
            location.url('contact/edit/' + id + '/?contact=' + scope.id)
        }

        scope.deleteGroupContact = function (contactId) {
            //console.log('cccontactid: ' + contactId + ', ccgroupid 2: ' + scope.id)

            bootbox.confirm('Your you sure you want to delete this contact from group?',
                function (result) {
                    if (result) {
                        groupService.deleteGroupContact(contactId, scope.id)
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

    }]);
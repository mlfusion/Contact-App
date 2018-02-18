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
app.factory('contactService', function ($http) {

    var api = 'Contact/';
    var url = '';

    return {

        getContacts: function () {
            url = api + 'GetContacts';
            return $http.get(url);
        },

        getContact: function (id) {
            url = api + 'GetContact/' + id;
            return $http.get(url);
        },

        addContact: function (data) {
            url = api + 'AddContact';
            return $http.put(url, data);
        },

        updateContact: function (data) {
            url = api + 'UpdateContact';
            return $http.post(url, data);
        },      

        deleteContact: function (id) {
            url = api + 'DeleteContact/' + id;
            return $http.delete(url);
        },

        uploadContactPhoto: function (file) {
            var fd = new FormData();
            var url = 'profilePhotoHandler.ashx';
            fd.append('file', file);

            return $http.post(url, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
        }
    }
})

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);
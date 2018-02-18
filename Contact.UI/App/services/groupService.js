app.factory('groupService', function ($http) {

    var api = 'api/v2/group/';
    var url = '';

    return {

        getGroups: function () {
            url = 'api/v2/groups';
            return $http.get(url);
        },

        getGroup: function (id) {
            url = api + id;
            return $http.get(url);
        },

        addGroup: function (data) {
            url = api + 'add';
            return $http.put(url, data);
        },

        updateGroup: function (data) {
            url = api + 'update';
            return $http.post(url, data);
        },

        deleteGroup: function (id) {
            url = api + 'delete/' + id;
            return $http.delete(url);
        },

        deleteGroupContact: function (contactId, groupId) {
            url = api + 'delete/' + contactId + '/' + groupId;
            return $http.delete(url);
        }

    }
})
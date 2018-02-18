// edit controller
app.controller('editContactController', [
    '$scope', '$window', 'contactService', 'groupService', '$timeout', '$routeParams', '$location', function (scope, window, contactService, groupService, timeout, params, location) {

        scope.contact = {};
        scope.params = params.id;
        scope.groups = [];
        scope.list = []
        scope.contactSelectItems = []
        scope.title = 'Edit Contact'
        scope.type = 'Update Contact'

        if (scope.params === undefined) {
            scope.contact.Photo = 'default_avatar.png'
            scope.title = 'Add New Contact'
            scope.type = 'Add Contact'
        }

        // safe param if not null
        scope.groupQueryString = location.search()['group']
        scope.contactQueryString = location.search()['contact']

        //console.log('group: ' + scope.group)
        //console.log('contact: ' + scope.contact)

        if (scope.params !== undefined)
        // get contact
        contactService.getContact(scope.params)
            .then(function (data, status) {
                scope.contact = data.data;
                //console.log(scope.contact.Birthdate)
                scope.contact.Birthdate = scope.contact.Birthdate === '/Date(-2208970800000)/' ? null : moment(scope.contact.Birthdate).format('L');

                // loop though contact selected groups
                angular.forEach(scope.contact.Groups, function (value, key) {
                    scope.contactSelectItems.push(value.Id);
                    //console.log(scope.contactSelectItems)
                });

            }).catch (function (data, status) {
                console.log(data)
            });

        // get group list
        groupService.getGroups()
            .then(function (data, status) {

                // loop though group list
                angular.forEach(data.data, function (value, key) {
                    scope.list.push({
                        Id: value.Id,
                        Name: value.GroupName
                    });
                })

            }).catch (function (data, status) {
                console.log(data)
            });

        // edit contact
        scope.editContact = function () {

            if (!scope.contactForm.$valid) {
                return;
            }

            // save selected group to arraylist
            scope.contact.Groups = []
            angular.forEach(scope.contactSelectItems, function (value, item) {
                //console.log(value)
                scope.contact.Groups.push({
                    Id: value
                })
            })

            // get photo file name
            var file = scope.myFile;

           // console.log(file);
            //console.log(scope.contact.Photo);

            if (file !== undefined) {

                if (!fileValidation(file)) {
                    return false;
                }

                contactService.uploadContactPhoto(file)
                    .then(function (data, status) {

                        if (data.status === 200) {
                            scope.contact.Photo = data.data;

                            if (scope.contact.Id > 0) {
                                scope.updateContact();
                            }
                            else {
                                scope.addContact();
                            }
                        }
                        else {
                            bootbox.alert(data.data)
                            return false;
                        }

                    }), (function (data, status) {
                        console.log(' error: ' +data)
                        return false;
                    });
            }
            else {
                if (scope.contact.Id > 0) {
                     scope.updateContact();
                }      
                else {
                    scope.addContact();
                }
            }
        }

        scope.addContact = function () {
            contactService.addContact(scope.contact)
                .then(function (data, status) {
                    showSuccessMessage('Contact has been added successfully.')
                    location.path('/')
                }).catch (function (data, status) {
                console.log(data)
                showErrorMessage();
                });
        }

        scope.updateContact = function () {
            contactService.updateContact(scope.contact)
                .then(function (data, status) {
                    showSuccessMessage('Contact has been updated successfully.')
                    scope.gotoMain()
                }).catch (function (data, status) {
                console.log(data)
                showErrorMessage();
                });
        }

        scope.gotoMain = function () {

            if (scope.groupQueryString !== undefined) {
                location.path('groups')
            }
            else if (scope.contactQueryString !== undefined) {
                location.path('group/contacts/' + scope.contactQueryString)
            }
            else {
                location.path('/')
            }
        }

        var fileValidation = function (file) {
            if (file.size > 375000) {
                bootbox.alert('Photo execeed the limit. Please select a file < 3 mb.')
                return false
            }
           // else if (!fileVaidateFileType(file)) {
           //     bootbox.alert('Only photo images can by uploaded.')
           //     return false
           // }
            else {
                return true;
            }
        }

        var fileVaidateFileType = function (fileName) {

            var ext = ['gif', 'GIF', 'jpg', 'JPG']

            angular.forEach(ext, function (value, item) {
                if (fileName.includes(value)) {
                    console.log('value: ' + value + ', item' + item);
                }
            });

            console.log(ext)
           
           // var ext = fileName.indexOf() <= 0;
           // console.log(ext)//ext == "gif" || ext == "GIF" || ext == "JPEG" || ext == "jpeg" || ext == "jpg" || ext == "JPG" || ext == "doc" || 
            if (fileName.indexOf('jpg') != -1) {
                return true;
            } else {
                return false;
            }
        }

    }]);
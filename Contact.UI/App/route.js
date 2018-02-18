app.config(function ($routeProvider) {

    $routeProvider
        .when('/',
        {
            templateUrl: '../app/contact/views/contact-list.html',
            controller: 'contactController'
        })
        .when('/contact/edit/:id',
        {
            templateUrl: '../app/contact/views/contact-edit.html',
            controller: 'editContactController'
        })
        .when('/contact/add',
        {
            templateUrl: '../app/contact/views/contact-edit.html',
            controller: 'editContactController'
        })
        .when('/groups',
        {
            templateUrl: '../app/groups/views/group-list.html',
            controller: 'groupController'
        })
        .when('/group/contacts/:id',
        {
            templateUrl: '../app/groups/views/group-contact-modal-list.html',
            controller: 'groupContactsController'
        })
        .otherwise(
        {
            redirectTo: '/'
        }   
    )

});
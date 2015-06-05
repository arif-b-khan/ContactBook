(function () {
    'use strict';

    var mainViewConfiguration = function ($routeProvider) {
        $routeProvider.when('/test', {
            templateUrl: "views/Contacts/Test.html",
            controller: 'testController'
        })
        .when('/newContact', {
            templateUrl: "views/Contacts/NewContact.html",
            controller: "newContactCntrl"
        });

    };

    mainViewConfiguration.$inject = ['$routeProvider'];

    angular.module("account.view.contacts", ['ngRoute', 'ui.validate'])
    .config(mainViewConfiguration);
})();
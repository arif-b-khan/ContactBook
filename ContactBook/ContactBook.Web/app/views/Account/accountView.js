
(function () {
    'use strict';

    var loginConfiguration = function ($routeProvider) {
        $routeProvider.when('/login', {
            templateUrl: "views/Account/login.html",
            controller: 'loginController'
        }).when('/register', {
            templateUrl: "views/Account/Register.html",
            controller: 'registerController'
        });
    };

    loginConfiguration.$inject = ['$routeProvider'];

    angular.module("account.view", ['ngRoute'])
    .config(loginConfiguration);
})();

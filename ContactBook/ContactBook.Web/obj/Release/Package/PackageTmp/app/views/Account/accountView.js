
(function () {
    'use strict';

    var loginConfiguration = function ($routeProvider) {
        $routeProvider.when('/login', {
            templateUrl: "views/Account/Login.html",
            controller: 'loginController',
            access:{
                hideWhenLoggedin: true
            }
        })
        .when('/register', {
            templateUrl: "views/Account/Register.html",
            controller: 'registerController',
            access:{
                hideWhenLoggedin: true
            }
        });
    };

    loginConfiguration.$inject = ['$routeProvider'];

    angular.module("account.view.account", ['ngRoute', 'ui.validate', 'angularSpinner'])
    .config(loginConfiguration);
})();

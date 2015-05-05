
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
        }).when('/changePassword', {
           templateUrl: 'views/Account/ChangePassword.html',
           controller: 'changePasswordCntrl',
           access:{
               hideWhenLoggedin: false
           }
        });
    };

    loginConfiguration.$inject = ['$routeProvider'];

    angular.module("account.view.account", ['ngRoute', 'ui.validate', 'angularSpinner'])
    .config(loginConfiguration);
})();

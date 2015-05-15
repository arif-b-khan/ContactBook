
(function () {
    'use strict';

    var loginConfiguration = function ($routeProvider) {
        $routeProvider.when('/login', {
            templateUrl: "views/Account/Login.html",
            controller: 'loginController',
            access: {
                hideWhenLoggedin: true
            }
        })
        .when('/register', {
            templateUrl: "views/Account/Register.html",
            controller: 'registerController',
            access: {
                hideWhenLoggedin: true
            }
        }).when('/changePassword', {
            templateUrl: 'views/Account/ChangePassword.html',
            controller: 'changePasswordCntrl',
            access: {
                hideWhenLoggedin: false,
                isRequired: true
            }
        }).when('/confirmEmail', {
            templateUrl: 'views/Account/ConfirmEmail.html',
            controller: 'confirmEmailCntrl',
            access: {
                hideWhenLoggedin: true
            }
        }).when('/registerSuccess', {
            templateUrl: 'views/Account/RegisterSuccess.html',
            controller: 'registerSuccessCntrl',
            access: {
                hideWhenLoggedin: true
            }
        }).when('/retrievePassword', {
            templateUrl: 'views/Account/RetrievePassword.html',
            controller: 'retrievePasswordCntrl',
            access: {
                hideWhenLoggedin: true
            }
        }).when('/resetPassword', {
            templateUrl: 'views/Account/ResetPassword.html',
            controller: 'resetPasswordCntrl',
            access: {
                hideWhenLoggedin: true
            }
        });
    };

    loginConfiguration.$inject = ['$routeProvider'];

    angular.module("account.view.account", ['ngRoute', 'ui.validate', 'ui.keypress', 'angularSpinner'])
    .config(loginConfiguration);
})();

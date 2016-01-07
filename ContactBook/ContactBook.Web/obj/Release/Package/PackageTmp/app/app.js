'use strict';

//var baseSvc = 'http://contactbookapi.azurewebsites.net';
var baseSvc = "http://contactbook.com/Contactbook.webapi";


// Declare app level module which depends on filters, and services
var cbApp = angular.module('contactbook', [
    'ngAnimate',
    'ui.bootstrap',
    'pasvaz.bindonce',
    'ngRoute',
    'LocalStorageModule',
    'contactbook.controllers',
    'contactbook.services',
    'contactbook.filters',
    "account.view.account",
    "account.view.main",
    "account.view.contacts"
])
.config(['$routeProvider', 'localStorageServiceProvider', function($routeProvider, localStorageServiceProvider) {
    $routeProvider.otherwise({
        redirectTo: '/login'
    });

    localStorageServiceProvider
        .setPrefix('contactbook')
        .setStorageType('localStorage')
        .setStorageCookie(7, '/')
        .setStorageCookieDomain(baseSvc);
}]);

cbApp.constant('cbSettings', {
    serviceBase: baseSvc,
    clientId: 'ng-contactbook'
});

cbApp.config(function($httpProvider) {
    $httpProvider.interceptors.push("httpInterceptorSvc");
});

cbApp.constant('storageSettings', {
        USERINFO_KEY: 'contactbook.userinfo'
    })
    .run(function($rootScope, $location, localStorageService, storageSettings) {
        $rootScope.returnUrl = "";
        $rootScope.userInfo = localStorageService.get(storageSettings.USERINFO_KEY);
        if ($rootScope.userInfo == null) {
            $rootScope.isAuthenticated = false;
        }
        else {
            $rootScope.isAuthenticated = true;
        }

        $rootScope.$on("$routeChangeStart", function(event, next) {
            if (angular.isDefined(next.access) && next.access.isRequired && !$rootScope.isAuthenticated) {
                $location.path("/login");
                $rootScope.returnUrl = next.$$route.originalPath;
            }

            if ($rootScope.isAuthenticated && (angular.isDefined(next.access) && next.access.hideWhenLoggedin)) {
               $location.path("/main");
            }
        });

    });

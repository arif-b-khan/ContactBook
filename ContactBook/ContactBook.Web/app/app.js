'use strict';

var baseSvc = 'http://contactbookapi.azurewebsites.net';

// Declare app level module which depends on filters, and services
var cbApp = angular.module('contactbook', [
    'pasvaz.bindonce',
    'ngRoute',
    'contactbook.controllers',
    'contactbook.services',
    "account.view"
]).
config(['$routeProvider', function ($routeProvider) {
    $routeProvider.otherwise({ redirectTo: '/login' });
}]);


cbApp.constant('cbSettings', {
    serviceBase: baseSvc,
    clientId: 'ng-contactbook'
});

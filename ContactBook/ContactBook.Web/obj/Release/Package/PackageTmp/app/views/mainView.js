(function () {
    'use strict';

    var mainViewConfiguration = function ($routeProvider) {
        $routeProvider.when('/main', {
            templateUrl: "views/Home.html",
            controller: 'homeController', 
            access:{
                isRequired: true
            }
        })

    };

    mainViewConfiguration.$inject = ['$routeProvider'];

    angular.module("account.view.main", ['ngRoute', 'ui.validate'])
    .config(mainViewConfiguration);
})();
(function () {
    'use strict';

    angular.module('contactbook.controllers').controller("homeController",
    ['$scope', '$location', 'authenticationSvc',
        function ($scope, $location, authenticationSvc) {
            $scope.homeLogout = function () {
                authenticationSvc.logout();
            };
        }
    ]);

})();

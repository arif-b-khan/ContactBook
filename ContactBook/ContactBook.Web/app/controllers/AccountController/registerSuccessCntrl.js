(function () {
    'use strict';
    angular.module('contactbook.controllers').controller("registerSuccessCntrl", ['$scope', '$location', function ($scope, $location) {
        $scope.username = $location.search().username;
        $scope.email = $location.search().email;
    }]);
})();
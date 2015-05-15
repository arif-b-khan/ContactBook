(function () {
    'use strict';
    cbControllers.controller("registerSuccessCntrl", ['$scope', '$location', function ($scope, $location) {
        $scope.username = $location.search().username;
        $scope.email = $location.search().email;
    }]);
})();
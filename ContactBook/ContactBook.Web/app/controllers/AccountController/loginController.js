(function () {
    "use strict";

    cbControllers.controller('loginController', ['$scope', 'accountSvc', function ($scope, accountSvc) {
        $scope.Register = function () {
            accountSvc.Register();
        };
    }])
})();
(function () {
    "use strict";

    cbController.controller('loginController', ['$scope', 'accountSvc', function ($scope, accountSvc) {
        $scope.Register = function () {
            accountSvc.Register();
        };
    }])
})();
(function () {
    'use strict';
    cbControllers.controller('confirmEmailCntrl', ['$scope', '$location', 'contactBookSpinner', 'accountSvc', function ($scope, $location, contactBookSpinner, accountSvc) {
        $scope.confirmSuccess = false;
        $scope.confirmFailed = false;
        $scope.invalidUrl = false;
        $scope.confirmEmailOnLoad = function () {
            var userId = $location.search().userId;
            var code = $location.search().code;

            if (angular.isDefined(userId) && angular.isDefined(code)) {
                var resultPromise = accountSvc.ConfirmEmail(userId, code);

                contactBookSpinner.spin("confirm-email-spinner");

                resultPromise.then(function (result) {
                    $scope.confirmSuccess = true;
                    contactBookSpinner.stop();
                }, function (err) {
                    $scope.confirmFailed = true;
                    contactBookSpinner.stop();
                });
            } else {
                $scope.invalidUrl = true;
            }
        };

    }
    ]);
})();
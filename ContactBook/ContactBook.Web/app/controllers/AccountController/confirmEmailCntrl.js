(function () {
    'use strict';
    angular.module('contactbook.controllers').controller('confirmEmailCntrl', ['$scope', '$location', 'contactBookSpinner', 'accountSvc', function ($scope, $location, contactBookSpinner, accountSvc) {
        $scope.confirmSuccess = false;
        $scope.confirmFailed = false;
        $scope.invalidUrl = false;
        $scope.confirmEmailOnLoad = function () {
            var identity = $location.search().identifier;
            contactBookSpinner.spin("confirm-email-spinner");

            if (angular.isDefined(identity)) {
                var resultPromise = accountSvc.ConfirmEmail(identity);

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
(function () {
    'use strict';

    angular.module('contactbook.controllers').controller('changePasswordCntrl', ['$scope', '$location', '$timeout', 'contactBookSpinner', 'accountSvc', function ($scope, $location, $timeout, contactBookSpinner, accountSvc) {

        $scope.user = {};
        $scope.changePwdError = false;
        $scope.changePwdSuccess = false;

        $scope.changeUserPassword = function (passwordModel, chPasswordForm) {
            var changePwdPromise = accountSvc.ChangePassword(passwordModel);
            $scope.errorMessages = new Array();
            contactBookSpinner.spin("cPassword-spinner");

            changePwdPromise.then(function (data) {
                contactBookSpinner.stop();
                $scope.changePwdSuccess = true;

                $timeout(function () {
                    $scope.homeLogout();
                    $location.path('#/login').replace();
                }, 2000);

            },
            function (err) {
                contactBookSpinner.stop();
                if (err.message === "The request is invalid.") {
                   
                    if (angular.isDefined(err.modelState)) {
                        angular.forEach(err.modelState, function (data, idx) {
                            angular.forEach(data, function (data1, idx1) {
                                    $scope.errorMessages.push(data1);
                            });
                        });
                    }
                }
                $scope.changePwdError = true;
                $scope.reset(chPasswordForm);
            });
        };

        $scope.reset = function (frm) {
            changePasswordInternal.resetFields();
            frm.$setPristine();
            frm.$setUntouched();
            $scope.errorMessages = new Array();
        };

        var changePasswordInternal = {
            resetFields: function () {
                $scope.user.OldPassword = "";
                $scope.user.NewPassword = "";
                $scope.user.ConfirmPassword = "";
            }
        };
    }]);

})();
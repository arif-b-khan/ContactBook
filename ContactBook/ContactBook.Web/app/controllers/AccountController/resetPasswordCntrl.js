(function () {
    'use strict';

    angular.module('contactbook.controllers').controller('resetPasswordCntrl', ['$scope', '$location', '$timeout', 'contactBookSpinner', 'accountSvc', function ($scope, $location, $timeout, contactBookSpinner, accountSvc) {

        $scope.user = {};
        $scope.setPwdError = false;
        $scope.setPwdSuccess = false;

        $scope.setUserPassword = function (passwordModel, setPasswordForm) {
            passwordModel.Identifier = $location.search().identifier;
            var setPwdPromise = accountSvc.SetPassword(passwordModel);

            $scope.errorMessages = new Array();

            contactBookSpinner.spin("setpassword-spinner");

            setPwdPromise.then(function (data) {
                contactBookSpinner.stop();
                $scope.setPwdSuccess = true;

                $timeout(function () {
                    $scope.homeLogout();
                    $location.path('#/login').replace();
                }, 3000);

            },
            function (err) {
                contactBookSpinner.stop();
                if (err.data.message === "The request is invalid.") {

                    if (angular.isDefined(err.modelState)) {
                        angular.forEach(err.modelState, function (data, idx) {
                            angular.forEach(data, function (data1, idx1) {
                                $scope.errorMessages.push(data1);
                            });
                        });
                    }
                }
                $scope.setPwdError = true;
                $scope.reset(setPasswordForm);
            });
        };

        $scope.reset = function (frm) {
            setPasswordInternal.resetFields();
            frm.$setPristine();
            frm.$setUntouched();
            $scope.errorMessages = new Array();
        };

        var setPasswordInternal = {
            resetFields: function () {
                $scope.user.NewPassword = "";
                $scope.user.ConfirmPassword = "";
            }
        };
    }]);

})();
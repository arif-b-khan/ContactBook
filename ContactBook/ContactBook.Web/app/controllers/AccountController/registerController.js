(function () {
    "use strict";
    var registerController = function ($scope, $location, $timeout, contactBookSpinner, accountSvc) {
        var spinnerName = "register-spinner";
        $scope.user = {};
        $scope.registrationFailed = false;

        $scope.usernameExists = function (username) {
            return accountSvc.UserExists(username);
        };

        $scope.userEmailExists = function (email) {
            return accountSvc.EmailExists(email);
        };

        $scope.register = function (registerInfo, frm) {
            if (!frm.$invalid) {
                contactBookSpinner.spin(spinnerName);

                registerInfo.ConfirmUrl = $location.$$protocol + '://' + $location.$$host + '/app#/confirmEmail';

                var result = accountSvc.Register(registerInfo);

                result.then(function (data) {
                    $location.path("/registerSuccess").search("username", data.userName).search("email", data.email).replace();
                    contactBookSpinner.stop(spinnerName);
                },
                    function (error) {
                        $scope.registrationFailed = true;
                        $scope.errorMessage = error;
                        contactBookSpinner.stop(spinnerName);
                    });
            }
        };

        $scope.reset = function (frm) {
            $scope.user.pwd = "";
            $scope.user.cpwd = "";
            $scope.user.username = "";
            frm.$setPristine();
            frm.$setUntouched();
        };

    };

    registerController.$inject = ['$scope', '$location', '$timeout', 'contactBookSpinner', 'accountSvc'];

    angular.module('contactbook.controllers').controller("registerController", registerController);

})();
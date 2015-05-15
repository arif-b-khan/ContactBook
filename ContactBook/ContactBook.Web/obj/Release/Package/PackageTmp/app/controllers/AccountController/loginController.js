(function() {
    "use strict";

    cbControllers.controller('loginController', ['$scope', '$location', '$rootScope', 'contactBookSpinner', 'authenticationSvc', function($scope, $location, $rootScope, contactBookSpinner, authenticationSvc) {
        $scope.signinDisable = false;
        $scope.registerHref = "#/register";
        $scope.forgotPasswordHref = "#/retrievePassword";
        $scope.loginError = {};
        $scope.loginError.success = false;

        $scope.loginUser = function(user, frm) {
            if (!frm.$invalid) {
                beforeLoginCall();
                authenticationSvc.login(user.username, user.password).then(function(res) {
                        if ($rootScope.returnUrl !== null) {
                            $location.path(angular.copy($rootScope.returnUrl)).replace();
                            $rootScope.returnUrl = null;
                        }
                        else {
                            $location.path("/main").replace();
                        }
                        afterLoginCall();
                    },
                    function (error) {
                        $scope.loginError.success = true;

                        if (angular.isDefined(error.error_description)) {
                            $scope.loginError.message = error.error_description;
                        } else {
                            $scope.loginError.message = "Invalid Credentials";
                        }
                        afterLoginCall();
                    }
                );
            }
        };
        var afterLoginCall = function() {
            contactBookSpinner.stop();
            $scope.signinDisable = false;
            $scope.registerHref = "#/register";
        };

        var beforeLoginCall = function() {
            $scope.loginError.success = false;
            contactBookSpinner.spin("login-spinner");
            $scope.signinDisable = true;
            $scope.registerHref = "";
            $scope.forgotPasswordHref = "";
        };
    }])
})();
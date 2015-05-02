(function() {
    "use strict";

    cbControllers.controller('loginController', ['$scope', '$location', '$rootScope', 'usSpinnerService', 'authenticationSvc', function($scope, $location, $rootScope, usSpinnerService, authenticationSvc) {
        $scope.signinDisable = false;
        $scope.registerHref = "#/register";
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
                    function(error) {
                        $scope.loginError.success = true;
                        $scope.loginError.message = "Invalid credentials";
                        afterLoginCall();
                    }
                );
            }
        };
        var afterLoginCall = function() {
            usSpinnerService.stop("login-spinner");
            $scope.signinDisable = false;
            $scope.registerHref = "#/register";
        };

        var beforeLoginCall = function() {
            $scope.loginError.success = false;
            usSpinnerService.spin("login-spinner");
            $scope.signinDisable = true;
            $scope.registerHref = "";

        };
    }])
})();
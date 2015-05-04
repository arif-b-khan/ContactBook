(function() {
    "use strict";
    var registerController = function($scope, $location, accountSvc) {
        $scope.user = {};
        $scope.registrationFailed = false;

        $scope.usernameExists = function(username) {
            return accountSvc.UserExists(username);
        };

        $scope.userEmailExists = function (email) {
            return accountSvc.EmailExists(email);
        };

        $scope.register = function(registerInfo, frm) {
            if (!frm.$invalid) {
                var result = accountSvc.Register(registerInfo);

                result.then(function (data) {
                    $location.path("/login").replace();
                },
                function (error) {
                    $scope.registrationFailed = true;
                    $scope.errorMessage = error;
                });
            }
        };

        $scope.reset = function(frm) {
            $scope.user.pwd = "";
            $scope.user.cpwd = "";
            $scope.user.username = "";
            frm.$setPristine();
            frm.$setUntouched();
        };
        
    };

    registerController.$inject = ['$scope', '$location', 'accountSvc'];

    cbControllers.controller("registerController", registerController);

})();
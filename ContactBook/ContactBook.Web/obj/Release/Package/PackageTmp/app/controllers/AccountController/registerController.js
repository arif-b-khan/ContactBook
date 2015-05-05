(function() {
    "use strict";
    var registerController = function($scope, $location, $timeout, usSpinnerService, accountSvc) {
       var spinnerName = "register-spinner";
        $scope.user = {};
        $scope.registrationFailed = false;
       
        $scope.usernameExists = function(username) {
            return accountSvc.UserExists(username);
        };

        $scope.userEmailExists = function(email) {
            return accountSvc.EmailExists(email);
        };

        $scope.register = function(registerInfo, frm) {
            if (!frm.$invalid) {
                usSpinnerService.spin(spinnerName);
                var result = accountSvc.Register(registerInfo);
                var spinStopped = false;
                result.then(function(data) {
                        $location.path("/login").replace();
                        usSpinnerService.stop(spinnerName);
                    },
                    function(error) {
                        $scope.registrationFailed = true;
                        $scope.errorMessage = error;
                        usSpinnerService.stop(spinnerName);
                    });
                    $timeout(function(){
                        if(!spinStopped){
                            usSpinnerService.stop(spinnerName);
                        }
                    }, 60000);
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

    registerController.$inject = ['$scope', '$location', '$timeout', 'usSpinnerService', 'accountSvc'];

    cbControllers.controller("registerController", registerController);

})();
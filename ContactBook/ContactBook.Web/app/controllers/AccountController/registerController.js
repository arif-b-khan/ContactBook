(function() {
    "use strict";
    var registerController = function($scope, accountSvc, $window) {
        $scope.user = {};
        
        $scope.usernameExists = function(username) {
            return accountSvc.UserExists(username);
        };

        $scope.register = function(registerInfo, frm) {
            if (frm.$invalid) {
                $window.alert("Correct validations");
            }
            else {
                $window.alert(angular.toJson(registerInfo));
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

    registerController.$inject = ['$scope', 'accountSvc', '$window'];

    cbControllers.controller("registerController", registerController);

})();
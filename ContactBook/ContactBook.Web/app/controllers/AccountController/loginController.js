(function() {
    "use strict";

    cbControllers.controller('loginController', ['$scope', 'accountSvc', function($scope, accountSvc) {
        $scope.loginUser = function(user, frm) {
            if (!frm.$invalid) {
                accountSvc.Login(user.username, user.password);
            }
        };
    }])
})();
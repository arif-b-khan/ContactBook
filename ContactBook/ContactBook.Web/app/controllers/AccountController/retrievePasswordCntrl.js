(function () {
    'use strict';

    cbControllers.controller('retrievePasswordCntrl', ['$scope', '$location', '$timeout', 'accountSvc', function ($scope, $location, $timeout, accountSvc) {
        
        $scope.emailSuccess = false;
        $scope.emailFailed = false;
        $scope.hideForm = false;

        $scope.userEmailExists = function (email) {
            return accountSvc.ForgotEmailExists(email);
        };

        $scope.sendLink = function (userInfo, frm) {
            userInfo.Link = $location.$$protocol + '://' + $location.$$host + '/app#/resetPassword';

            var result = accountSvc.ForgotPassword(userInfo);

            result.then(function (data) {
                $scope.emailSuccess = true;
                $scope.hideForm = true;
            },
            function (err) {
                $scope.emailFailed = true;

                $timeout(3000, function () {
                    $scope.emailFailed = false;
                    $scope.hideForm = false;
                    frm.$setPristine();
                    frm.$setUntouched();
                });
            });
        };
    }]);
})();
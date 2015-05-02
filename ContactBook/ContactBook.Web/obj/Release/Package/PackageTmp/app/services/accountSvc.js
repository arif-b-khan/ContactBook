(function() {
    'use stricts';

    cbServices.factory("accountSvc", ['$http', '$q', 'cbSettings', '$window', function($http, $q, cbSettings, $window) {
        var userList = [];

        var init = function() {
            for (var i = 1; i < 10; i++) {
                userList.push("axkhan" + i);
            }
        };

        var url = cbSettings.serviceBase;

        var accountUrl = {
            Login: url + '\Token',
            Register: url + '\Register',
            UserExists: url + '\UserExists'
        };

        var register = function() {

        };

        var login = function(username, password) {
            var data = "grant_type=password&username=" + username + "&password=" + password;
            
            var deferred = $q.defer();
            $http.post(accountUrl.Login, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .success(function(response){
                
            })
            .error(function(error){
                
            });
            return deferred.promise;
        };

        var userExists = function(username) {
            if (angular.isUndefined(username)) {
                return true;
            }
            var result = true;

            angular.forEach(userList, function(data, indx) {
                if (data == username) {
                    result = false;
                    return;
                }
            });

            return result;
        };

        init();

        return {
            Register: register,
            Login: login,
            UserExists: userExists
        };
    }]);

})();
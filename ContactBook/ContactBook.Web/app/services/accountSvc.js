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
            var obj = {};
            obj.usr = username;
            obj.pwd = password;
            $window.alert(angular.toJson(obj));
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
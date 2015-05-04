(function () {
    'use stricts';

    cbServices.factory("accountSvc", ['$http', '$q', 'cbSettings', '$window', function ($http, $q, cbSettings, $window) {
        var userList = [];

        var init = function () {
            for (var i = 1; i < 10; i++) {
                userList.push("axkhan" + i);
            }
        };

        var url = cbSettings.serviceBase;

        var accountUrl = {
            Login: url + '/Token',
            Register: url + '/api/Account/Register',
            UserExists: url + '/api/Account/UserExists?username=',
            EmailExists: url + '/api/Account/EmailExists?email='
        };

        var register = function (registerModel) {
            var deferred = $q.defer();

            $http.post(accountUrl.Register, registerModel)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        };

        var login = function (username, password) {
            var data = "grant_type=password&username=" + username + "&password=" + password;

            var deferred = $q.defer();

            $http.post(accountUrl.Login, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .success(function (response) {
                deferred.resolve(response);
            })
            .error(function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        };

        var userExists = function (username) {
            if (angular.isUndefined(username)) {
                return true;
            }
            //var deferred = $q.defer();

            //$http.get(accountUrl.UserExists + username)
            //.success(function (data) {
            //    deferred.resolve(true);
            //})
            //.error(function (error) {
            //    deferred.reject(false);
            //});

            return $http.get(accountUrl.UserExists+username);
        };

        var emailExists = function (email) {
            if (angular.isUndefined(email)) {
                return true;
            }

            var deferred = $q.defer();

            $http.get(accountUrl.EmailExists + email)
            .success(function (data) {
                deferred.resolve(true);
            })
            .error(function (error) {
                deferred.reject(false);
            });

            return deferred.promise;

        };

        init();

        return {
            Register: register,
            Login: login,
            UserExists: userExists,
            EmailExists: emailExists
        };
    }]);

})();
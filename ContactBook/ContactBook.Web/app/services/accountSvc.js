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
            EmailExists: url + '/api/Account/EmailExists?email=',
            ChangePassword: url + '/api/Account/ChangePassword',
            ConfirmEmail: url + '/api/Account/ConfirmEmail',
            ForgotPassword: url + '/api/Account/ForgotPassword',
            ForgotEmailExists: url + '/api/Account/ForgotEmailExists?email=',
            SetPassword: url + '/api/Account/ResetPassword'
        };

        var forgotPassword = function (userInfo) {
            var deferred = $q.defer();

            $http.post(accountUrl.ForgotPassword, userInfo)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        };

        var confirmEmail = function (identifier) {
            return $http.get(accountUrl.ConfirmEmail + '?identifier=' + identifier);
        };

        var changePassword = function (passwordModel) {
            var deferred = $q.defer();

            $http.post(accountUrl.ChangePassword, passwordModel)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
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

            $http.post(accountUrl.Login, data, {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            })
                .success(function (response) {
                    deferred.resolve(response);
                })
                .error(function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        };

        var setPassword = function (passwordModel) {
            return $http.post(accountUrl.SetPassword, passwordModel);
        };

        var userExists = function (username) {
            if (angular.isUndefined(username)) {
                return true;
            }
            return $http.get(accountUrl.UserExists + username + '&rnd=' + new Date().getTime());
        };

        var emailExists = function (email) {
            if (angular.isUndefined(email)) {
                return true;
            }
            return $http.get(accountUrl.EmailExists + email);
        };

        var forgotEmailExists = function (email) {
            if (angular.isUndefined(email)) {
                return true;
            }

            return $http.get(accountUrl.ForgotEmailExists + email + '&rnd=' + new Date().getTime());
        };

        init();

        return {
            Register: register,
            Login: login,
            UserExists: userExists,
            EmailExists: emailExists,
            ChangePassword: changePassword,
            ConfirmEmail: confirmEmail,
            ForgotPassword: forgotPassword,
            ForgotEmailExists: forgotEmailExists,
            SetPassword: setPassword
        };
    }]);

})();
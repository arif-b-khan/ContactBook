(function() {
    "use strict";
    cbServices.factory("authenticationSvc", ['$http', '$q', '$rootScope', 'localStorageService', 'cbSettings', 'storageSettings', function($http, $q, $rootScope, localStorageService, cbSettings, storageSettings) {

        var url = cbSettings.serviceBase;

        var authUrls = {
            loginUrl: url + "\token",
            registUrl: url + "\register"
        };

        var _login = function(username, pwd) {
            var data = "gran_type=password&Username=" + username + "&Password=" + pwd;
            var deferred = $q.defer();

            $http.post(authUrls.loginUrl, data, {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                })
                .success(function(pUserinfo) {

                    $rootScope.userInfo = {
                        Token: pUserinfo.access_token,
                        Username: pUserinfo.userName
                    };

                    localStorageService.set(storageSettings.USERINFO_KEY, angular.copy($rootScope.userInfo));
                    $rootScope.isAuthenticated = true;
                    deferred.resolve(true);
                })
                .error(function(err, status) {
                    _logout();
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        var _logout = function() {
            delete $rootScope.Userinfo;
            localStorageService.remove(storageSettings.USERINFO_KEY);
            $rootScope.isAuthenticated = false;
        }

        return {
            login: _login,
            logout: _logout
        };
    }]);
})();
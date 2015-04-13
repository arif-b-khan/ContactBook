
(function () {
    'use stricts';

    cbServices.factory("accountSvc", ['$http', '$q', '$window', function ($http, $q, cbSettings, $window) {
        var url = cbSettings.serviceBase;

        var accountUrl = {
            Login: url + '\Token',
            Register: url + '\Register'
        };

        var register = function () {
          
        };

        var login = function (username, password) {

        };

        return {
            Register: register,
            Login: login
        };
    }]);

})();
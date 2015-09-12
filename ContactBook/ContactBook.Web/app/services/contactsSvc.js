(function () {
    'use stricts';

    cbServices.factory("contactsSvc", ['$http', '$q', 'cbSettings', function ($http, $q, cbSettings) {
        var getContacts = function () {
            var deferred = $q.defer();

            $http.get("http://contactbook.com/contactbook.web/app/data/contactslist.json").then(function (data) {
                deferred.resolve(data);
            });
            return deferred.promise;
        };

        return {
            GetContacts: getContacts
        };
    }]);

})();
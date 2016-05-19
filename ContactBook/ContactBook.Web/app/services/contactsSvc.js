(function () {
    'use stricts';

    angular.module('contactbook.services').factory("contactsSvc", ['$http', '$q', '$rootScope', 'cbSettings', function ($http, $q, $rootScope, cbSettings) {
        var url = cbSettings.serviceBase;

        var contactUrl = {
            GetContact: url + "/api/ApiContact"
        }

        var getContacts = function () {
            var deferred = $q.defer();

            $http.get(contactUrl.GetContact+'/'+$rootScope.userInfo.BookId).then(function (data) {
                deferred.resolve(data);
            });
            return deferred.promise;
        };
        
        var deleteContact = function(contactId){
                // var deferred = $q.defer();
                // getContacts().then(function(data){
                    
                // },
                // function(err){
                    
                // }
                // );
                // angular.forEach()
                // return deferred.promise;
        };
        
        return {
            GetContacts: getContacts,
            DeleteContact: deleteContact
        };
    }]);

})();
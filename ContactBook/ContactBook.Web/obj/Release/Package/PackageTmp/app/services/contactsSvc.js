(function () {
    'use stricts';

    cbServices.factory("contactsSvc", ['$http', '$q', 'cbSettings', function ($http, $q, cbSettings) {
        
        var getContacts = function () {
            var deferred = $q.defer();

            $http.get("http://contactbook-arif-bannehasan1.c9.io/ContactBook/ContactBook.Web/app/data/contactslist.json?_c9_id=livepreview0&_c9_host=https://ide.c9.io#/newContact").then(function (data) {
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
(function(){
'use strict';
angular.module('contactbook.services').factory("httpInterceptorSvc", ['$q', '$location', '$injector', '$rootScope', 'localStorageService', 'storageSettings', function ($q, $location, $injector, $rootScope, localStorageService, storageSettings) {
    
    var _request = function(config){
        
        if($rootScope.userInfo != null)
        {
            config.headers.Authorization = "Bearer " + $rootScope.userInfo.Token;
        }else{
            var _userInfo = localStorageService.get(storageSettings.USERINFO_KEY);
            if(_userInfo){
                config.headers.Authorization = "Bearer "+ _userInfo.Token;
            }
        }
    
        return config;
    };
    
    var _responseError = function(error){
      if(error.status == 401){
          var authSvc = $injector.get("authenticationSvc");
          authSvc.logout();
          $location.path('/login');
      }  
      return $q.reject(error);
    };
    return {
        request: _request,
        responseError: _responseError
    };
}]);
})();

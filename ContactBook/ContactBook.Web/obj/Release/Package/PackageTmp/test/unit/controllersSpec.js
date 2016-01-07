'use strict';

/* jasmine specs for controllers go here */

describe('controllers', function(){
    beforeEach(module('contactbook.controllers'));
    beforeEach(module('contactbook.services'));

    var scope;

    beforeEach(angular.mock.inject(function ($rootScope, $controller, $location, $timemout, contactBookSpinner, accountSvc) {
        scope = $rootScope.$new();
        var location = $location;
        var timeout = $timeout;
        var spinner = contactBookSpinner;
        var account = accountSvc;

        $controller('registerController', { $scope: scope, $location: location, $timeout: timeout, contactBookSpinner: spinner, accountSvc: account });

    }));

    it('should ', inject(function () {
        scope.user = {};
        scope.user.pwd = "1234";
        scope.user.cpwd = "1234";
        scope.user.username = "axkhan2";
        scope.reset();
  }));

  it('should ....', inject(function() {
    //spec body
  }));
});

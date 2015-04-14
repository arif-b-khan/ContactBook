'use strict';

var cbServices = angular.module('contactbook.services', [])
.run(function($rootScope, cbPubSubScopeFactory) {
        cbPubSubScopeFactory($rootScope);
    });
(function() {
    "use strict";

    angular.module('contactbook.services').service('cbPubSubScopeFactory', function($log) {

        return function(scope) {

            $log.debug(scope.$id + ': initializing pub sub');

            // Establish a new publish scope on this scope
            // (overwriting any existing prototypically-inherited one)
            scope.__publishScope = {};


            // We should already have scope.publish and scope.subscribe from $rootScope, 
            // but not if this is a new scope chain, so check for them and add if missing

            if (!scope.publish) {
                scope.publish = function(evnt, args) {
                    var target = this,
                        handlers = target.__publishScope[evnt];
                    var handlerResult = null;
                    if (!handlers)
                        return;

                    angular.forEach(handlers, function(handlerObj) {
                        handlerResult = handlerObj.handler.call(target, args);
                    });

                    return handlerResult;
                }
            }

            if (!scope.subscribe) {
                scope.subscribe = function(evnt, handler) {
                    var target = this,
                        handlers = getOrCreateHandlers(target, evnt);

                    handlers.push({
                        $id: target.$id,
                        handler: handler
                    });
                }
            }

            if (!scope.subscribeOnce) {
                scope.subscribeOnce = function(evnt, handler) {
                    var target = this,
                        handlers = getOrCreateHandlers(target, evnt);
                    if (target.__publishScope[evnt].length > 0) {
                        delete target.__publishScope[evnt];
                        handlers = target.__publishScope[evnt] = [];
                    }
                    handlers.push({
                        $id: target.$id,
                        handler: handler
                    });
                }
            }

            if (!scope.unsubscribe) {
                scope.unsubscribe = function(evnt, handler) {
                    var target = this,
                        handlers = getOrCreateHandlers(target, evnt);
                    for (var i = 0; i < handlers.length; i++) {
                        if (handlers[i].handler == handler) {
                            $log.debug(target.$id + ': unsubscribing/destroying handlers for "' + evnt + '"');
                            handlers.splice(i, 1);
                            break;
                        }
                    }
                }
            }

            function getOrCreateHandlers(target, evnt) {
                var handlers = target.__publishScope[evnt];

                if (angular.isUndefined(handlers)) {
                    handlers = target.__publishScope[evnt] = [];
                }

                // Cleanup on scope $destroy
                target.$on('$destroy', function() {
                    $log.debug(target.$id + ': destroying handlers for "' + evnt + '"');
                    handlers.forEach(function(handler, i) {
                        if (handler.$id === target.$id) {
                            handlers.splice(i, 1);
                        }
                    });
                });

                return handlers;
            }
        }
    })

    // Pubsub directive
    .directive('cbPubSub', function($log, testPubSub) {
        return {
            priority: 999999, // Run before anything else ever
            restrict: 'ACE',
            scope: false,
            controller: function($scope) {
                cbPubSubScopeFactory($scope);
            }
        };
    }); 
})();

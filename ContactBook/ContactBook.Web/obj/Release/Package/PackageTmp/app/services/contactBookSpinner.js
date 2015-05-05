(function() {
    'use strict';
    cbServices.factory("contactBookSpinner", ['$timeout', 'usSpinnerService',
        function($timeout, usSpinnerService) {
            var defaultName = "tempSpinner";
            var name = '';
            var timeOut = 120000;

            var spinnerState = {
                started: false,
                stopped: true
            };

            var cbSpin = function(spinnerName) {

                if (angular.isDefined(spinnerName)) {
                    name = spinnerName;
                    usSpinnerService.spin(spinnerName);
                    spinnerState.started = true;
                    spinnerState.stopped = false;
                }
                else {
                    console.log("Must define spinner name");
                }

                $timeout(function() {
                    if (!spinnerState.stopped) {
                        if (angular.isDefined(spinnerName)) {
                            usSpinnerService.stop(spinnerName);
                        }
                    }
                }, timeOut);
            }

            var cbStop = function() {
                usSpinnerService.stop(name);
                spinnerState.started = false;

                spinnerState.stopped = true;
            };
            
            return {
                spin: cbSpin,
                stop: cbStop
            }
        }
    ]);
})();
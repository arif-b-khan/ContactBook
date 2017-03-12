'use strict';

/* Filters */

angular.module('contactbook.filters', []).filter('bytes', function() {
    return function(bytes, precision) {
        if (isNaN(parseFloat(bytes)) || !isFinite(bytes)) return '-';
        if (typeof precision === 'undefined') precision = 1;
        var units = ['bytes', 'kB', 'MB', 'GB', 'TB', 'PB'],
            number = Math.floor(Math.log(bytes) / Math.log(1024));
        return (bytes / Math.pow(1024, Math.floor(number))).toFixed(precision) + ' ' + units[number];
    }
}).filter('letterFilter', function() {
    return function(input, letter) {
        input = input || [];
        var out = [];
        if (letter === '*') {
            out = input;
        }
        else {
            input.forEach(function(item) {
                if (item.firstname.charAt(0).toUpperCase() == letter) {
                    out.push(item);
                }
            });
        }

        return out;
    }
});

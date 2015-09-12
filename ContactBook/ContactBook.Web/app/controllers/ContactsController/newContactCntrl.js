(function () {
    'use strict';
    cbControllers.controller('newContactCntrl', ['$scope', '$filter', '$log', 'contactsSvc', function ($scope, $filter, $log, contactsSvc) {
        $scope.serverObject = null;
        var contactPromise = contactsSvc.GetContacts();
        contactPromise.then(function (data) {
            $scope.contactList = data.data;
             //init($scope.serverObject);
        }, function (err) {
            $log.error('error had occoured');
        });

        $scope.search = '';

        $scope.contactList = new Array();

        var init = function (objs) {
            //var groupSize = 3;
            //$scope.contactList = _.map(objs, function (item, index) {
            //    return index % groupSize === 0 ? objs.slice(index, index + groupSize) : null;
            //}).filter(function (item) {
            //    return item;
            //});
        };

        $scope.changeObj = function (search) {
            var newArray = $filter('filter')($scope.serverObject, search);
            init(newArray);
        };

    }]);
})();
(function() {
    'use strict';
    cbControllers.controller('newContactCntrl', ['$scope', '$filter', '$log', 'contactsSvc', function($scope, $filter, $log, contactsSvc) {
        var serverObjects = [];

        $scope.search = '';
        $scope.letterList = [];
        $scope.selectedLetter = '*';

        //Pagination variables
        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.maxSize = 10;
        $scope.itemPerPage = 12;
        
        /*
         * init method make service call to get all the contacts
         */
        var init = function() {
            populateLetters();
            contactsSvc.GetContacts().then(onSuccess, onError);
        };

        var onSuccess = function(data) {
            serverObjects = angular.copy(data.data);
            
            if (angular.isDefined(serverObjects)) {
                populateContacts(serverObjects);
            }
        };

        var onError = function(err) {
            $log.error('error had occoured');
        };

        var populateContacts = function(objs) {
            $scope.contactList = [];
            $scope.pagedContactList = [];
            
            angular.forEach(objs, function(value, key) {
                $scope.contactList.push(value);
            });
            
            $scope.totalItems = $scope.contactList.length;
            $scope.currentPage = 1;
            $scope.pagedContactList = _.chunk($scope.contactList, $scope.itemPerPage)[$scope.currentPage - 1];
        };

        var populateLetters = function() {
            $scope.letterList.push('*');
            for (var i = 65; i < 91; i++) {
                $scope.letterList.push(String.fromCharCode(i));
            }
        };

        $scope.searchContact = function(search) {
            var newArray = $filter('filter')(serverObjects, search);
            populateContacts(newArray);
            $scope.selectedLetter = $scope.letterList[0];
        };

        $scope.searchByLetter = function(letter) {
            var newArray = $filter('letterFilter')(serverObjects, letter);
            populateContacts(newArray);
            $scope.selectedLetter = letter;
        }

        $scope.pageChanged = function() {
            $scope.pagedContactList = _.chunk($scope.contactList, $scope.itemPerPage)[$scope.currentPage - 1];
        };

        init();
    }]);
})();
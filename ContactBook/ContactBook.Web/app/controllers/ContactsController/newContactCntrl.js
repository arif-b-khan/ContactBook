(function() {
    'use strict';

    angular.module('contactbook.controllers').controller('newContactCntrl', ['$scope', '$filter', '$modal', '$log', 'contactsSvc', function ($scope, $filter, $modal, $log, contactsSvc) {
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
            $scope.onlyNumbers = /^\d+$/;
        };

        var onSuccess = function(data) {
            serverObjects = angular.copy(data.data);

            if (angular.isDefined(serverObjects)) {
                loadContacts(serverObjects);
            }
        };

        var onError = function(err) {
            $log.error('error had occoured');
        };

        var loadContacts = function(objs) {
            $scope.contactList = [];
            $scope.pagedContactList = [];

            angular.forEach(objs, function(value, key) {
                $scope.contactList.push(value);
            });

            $scope.totalItems = $scope.contactList.length;
            $scope.pagedContactList = _.chunk($scope.contactList, $scope.itemPerPage)[$scope.currentPage - 1];

        };

        var populateLetters = function() {
            $scope.letterList.push('*');
            for (var i = 65; i < 91; i++) {
                $scope.letterList.push(String.fromCharCode(i));
            }
        };

        var hasItemForSelectedLetter = function(letter){
             var newArray = $filter('letterFilter')(serverObjects, letter);
             return newArray.length >= 1;
        };
        
        $scope.searchContact = function(search) {
            var newArray = $filter('filter')(serverObjects, search);
            $scope.currentPage = 1;
            loadContacts(newArray);
            $scope.selectedLetter = $scope.letterList[0];
        };

        $scope.searchByLetter = function(letter, setToFristPage) {
            var newArray = $filter('letterFilter')(serverObjects, letter);
            if (setToFristPage) {
                $scope.currentPage = 1;
            }
            loadContacts(newArray);
            $scope.selectedLetter = letter;
        }

        $scope.pageChanged = function() {
            $scope.pagedContactList = _.chunk($scope.contactList, $scope.itemPerPage)[$scope.currentPage - 1];
        };

        $scope.clearSearchBox = function() {
            $scope.search = {
                name: '',
                phone: '',
                email: ''
            };
        };

        $scope.open = function(sz, openContact) {
            var modalInstance = $modal.open({
                templateUrl: 'deleteModalContent.html',
                controller: 'deleteModalCntrl',
                animation: true,
                size: sz,
                backdrop: 'static',
                //backdropClass: 'modal-backdrop h-full',
                resolve: {
                    contact: function() {
                        var Id = openContact.id;
                        return Id;

                    }
                }
            });

            modalInstance.result.then(function(contactId) {
                var tempServerObj = [];
                $log.info('Delete contact with contact id : ' + contactId);

                angular.forEach(serverObjects, function(value, key) {
                    if (contactId !== value.id) {
                        tempServerObj.push(value);
                    }
                });

                serverObjects = tempServerObj;
                
                if ($scope.selectedLetter !== '*') {
                    if(!hasItemForSelectedLetter($scope.selectedLetter)){
                        $scope.selectedLetter = '*';
                        $scope.currentPage = 1;
                        loadContacts(serverObjects);                        
                    }else{
                        $scope.searchByLetter($scope.selectedLetter, false);                        
                    }
                }
                else {
                    loadContacts(serverObjects);
                }
                
            }, function(err) {
                $log.error(err.message);
            });
        };

        init();
    }]);

    angular.module('contactbook.controllers').controller('deleteModalCntrl', ['$scope', '$modalInstance', 'contact', function ($scope, $modalInstance, contact) {

        $scope.contactId = contact;

        $scope.ok = function() {
            $modalInstance.close($scope.contactId);
        };

        $scope.cancel = function() {
            $modalInstance.dismiss('cancelled');
        };
    }]);

})();
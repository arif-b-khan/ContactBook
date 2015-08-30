(function () {
    'use strict';
    cbControllers.controller('newContactCntrl', ['$scope', '$filter', function ($scope, $filter) {
        $scope.serverObject = [
        {
            name: 'arif khan',
            phone: '9768836054',
            email: 'arif788@gmail.com'
        },
        {
            name: 'tarik khan',
            phone: '1111111111',
            email: 'tarik@gmail.com'
        },
                {
                    name: 'afreen khan',
                    phone: '2222222222',
                    email: 'afreen@gmail.com'
                },

                {
                    name: 'asif sayyed',
                    phone: '33333333333',
                    email: 'asif@aol.com'
                },
                        {
                            name: 'sandeep kamat',
                            phone: '44444444444',
                            email: 'sandeep@gmail.com'
                        },
                                {
                                    name: 'kamal relwani',
                                    phone: '5555555555',
                                    email: 'tarik@gmail.com'
                                }
        ]
        $scope.search = '';
        $scope.contactList = new Array();
        var init = function (objs) {
            var groupSize = 3;
            $scope.contactList = _.map(objs, function (item, index) {
                return index % groupSize === 0 ? objs.slice(index, index + groupSize) : null;
            }).filter(function (item) {
                return item;
            });
        };
        $scope.changeObj = function (search) {
            var newArray = $filter('filter')($scope.serverObject, search);
            init(newArray);
        };

        init($scope.serverObject);
    }]);
})();
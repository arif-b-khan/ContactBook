(function () {
    'use strict'
    angular.module('contactbook.controllers').controller('testController', ['$scope', '$http', '$log', '$filter', 'cbUploader', 'cbSettings', function ($scope, $http, $log, $filter, cbUploader, cbSettings) {
        $scope.avatarUrl = cbSettings.serviceBase + '/api/ApiImages/GetImage';
        $scope.avatarUrlList = cbSettings.serviceBase + '/api/ApiImages/GetImageFileNames';
        $scope.uploadUrl = cbSettings.serviceBase + '/api/ApiImages/UploadImage';
        $scope.errorMessage = "";
        $scope.fileUploadPercent = 0;

        $scope.btn_remove = function (file) {
            $log.info('deleting=' + file);
            cbUploader.removeFile(file);
        };

        $scope.btn_clean = function () {
            cbUploader.removeAll();
        };

        $scope.btn_upload = function () {
            $log.info('uploading...');
            cbUploader.startUpload({
                url: $scope.uploadUrl,
                concurrency: 2,
                onProgress: function (file) {
                    $log.info(file.name + '=' + file.humanSize);
                    $scope.fileUploadPercent = (file.loaded / $scope.fileDetails.size) * 100;
                    $scope.$apply();
                },
                onCompleted: function (file, response) {
                    $log.info(file + 'response' + response);
                    init();
                },
                headers: {
                    Authorization: 'Bearer ' + $scope.userInfo.Token
                }
            });
        };

        $scope.uploadedFiles = [];
        $scope.files = [];

        var element = document.getElementById('file1');

        element.addEventListener('change', function (e) {
            var files = e.target.files;
            //var fileSize = $filter("bytes")(e.target.files[0].size);
            $scope.fileDetails = {
                name: e.target.files[0].name,
                size: e.target.files[0].size
            };

            cbUploader.addFiles(files);
            $scope.files = cbUploader.getFiles();
            $scope.$apply();
        });

        var init = function () {
            $http.get($scope.avatarUrlList)
            .success(function (data) {
                $scope.uploadedFiles = data;
            })
            .error(function (err) {
                $scope.errorMessage = err.data.message;
            });
        };

        init();
    }]);
})();
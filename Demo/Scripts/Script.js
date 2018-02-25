/// <reference path=""angular.min.js"">
var testApp = angular
        .module("userModule", [])
        .controller("userController", function($scope, $http) {
        $http.get('http://azureprojectdemo.azurewebsites.net/api/User')
        .then(
        function(response) {
            $scope.users = response.data ;
        },
        function(response) {
            $scope.config = response.config;
        })
        });

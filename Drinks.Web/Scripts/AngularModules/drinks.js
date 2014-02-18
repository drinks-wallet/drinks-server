var drinksApp = angular.module("drinks", [])
    .controller("EditAccountController", ["$scope", "$http", function ($scope, $http) {
        $scope.userId = "";
        $scope.name = "";
        $scope.username = "";
        $scope.badgeId = "";

        $scope.getUser = function () {
            $http.post("/Admin/GetUserData", { userId: $scope.userId })
                .success(function (data, status, headers, config) {
                    $scope.userId = data.userId;
                    $scope.name = data.name;
                    $scope.username = data.username;
                    $scope.badgeId = data.badgeId;
                })
                .error(function (data, status, headers, config) {
                    alert("Ajax Error!");
                });
        };
    }]
);
var drinksApp = angular.module("drinks", [])
    .controller("EditAccountController", ["$http", function ($http) {
        var self = this;

        self.userId = "";
        self.name = "";
        self.username = "";
        self.badgeId = "";

        self.getUser = function () {
            $http.post("/Admin/GetUserData", { userId: self.userId })
                .success(function (data, status, headers, config) {
                    self.userId = data.userId;
                    self.name = data.name;
                    self.username = data.username;
                    self.badgeId = data.badgeId;
                })
                .error(function (data, status, headers, config) {
                    alert("Ajax Error!");
                });
        };
    }]
);
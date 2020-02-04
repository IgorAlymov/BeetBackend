mainApp.service("ngApiService", function ($http) {
    var urlAPI = "http://localhost:64931/api/";
    this.registerUser = function (user) {
        var request = $http({
            method: "post",
            url: urlAPI + "RegisterUser",
            data: user
        });
        return request;
    }

    this.getAvatar = function (id) {
        console.log("запрос аватарки на сервере");
        return $http.get(urlAPI + "GetUserAvatar/" + id,
            { responseType: 'arraybuffer' });
    };

    this.sendAvatar = function (id, file) {
        return $http(
            {
                method: 'POST',
                url: urlAPI + "PostUserPhoto/" + id,
                headers: { 'Content-Type': undefined },
                data:
                {
                    File1: file
                },
                transformRequest: function (data, headersGetter) {
                    var formData = new FormData();
                    angular.forEach(data, function (value, key) {
                        formData.append(key, value);
                    });
                    return formData;
                }
            });
    };
});
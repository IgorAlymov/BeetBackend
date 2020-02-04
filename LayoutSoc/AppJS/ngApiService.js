mainApp.config(function ($httpProvider) {
    $httpProvider.defaults.withCredentials = true;
    console.log('$httpProvider', $httpProvider)
});
mainApp.service("ngApiService", function ($http) {
    var urlAPI = "http://localhost:5001/api/";

    this.registerUser = function (user) {
        var request = $http({
            method: "post",
            url: urlAPI + "Register",
            data: user
        });
        return request;
    }

    this.addGroup = function (groupName, file) {
        console.log(groupName);
        console.log(file);
        var formData = new FormData();
        formData.append('file', file);
        return $http(
            {
                method: 'POST',
                url: urlAPI + "PostAddGroup/" + groupName,
                headers: {
                    'Content-Type': undefined
                },
                data: formData
            });
    };

    this.addPost = function (text, file) {
        console.log(text);
        var formData = new FormData();
        formData.append('file', file);
        return $http(
            {
                method: 'POST',
                url: urlAPI + "postAddPost/" + text,
                headers: {
                    'Content-Type': undefined
                },
                data: formData
            });
    };

    this.addLikePost = function (idPost) {
        var request = $http({
            method: "post",
            url: urlAPI + "addLikePost" + "/" + idPost
        });
        return request;
    };

    this.entryUser = function (user) {
        user.birthday = "10.06.2019 19:00:00";
        console.log(user);
        var request = $http({
            method: "post",
            url: urlAPI + "SignIn",
            data:user
        });
        return request;
    }

    this.getAvatar = function (id) {
        return $http.get(urlAPI + "GetUserAvatar/" + id);
    };

    this.SignOut = function () {
        console.log("Выход");
        return $http.get(urlAPI + "SignOut");
    };

    this.getImageGroup = function (id) {
        return $http.get(urlAPI + "GetImageGroup/" + id);
    };

    this.getImagePosts = function (id) {
        console.log("запрос картинки поста на сервере");
        return $http.get(urlAPI + "GetImagePosts/" + id);
    };

    this.getAllPhotoUser = function (id,idU) {
        console.log("работает");
        return $http.get(urlAPI + "GetAllUserPhoto/" + id + "/" + idU);
    };

    this.sendAvatar = function (id, file) {
        var formData = new FormData();
        formData.append('file', file);
        return $http(
            {
                method: 'POST',
                url: urlAPI + "PostUserPhoto/" + id,   
                headers: {
                    'Content-Type': undefined
                },
                data: formData
            });
    };

    this.getCountPhotos = function (id) {
        return $http.get(urlAPI + "GetCountPhotoUser/" + id);
    };

    this.sendAddPhoto = function (id, file) {
        var formData = new FormData();
        formData.append('file', file);
        return $http(
            {
                method: 'POST',
                url: urlAPI + "PostAddPhoto/" + id,
                headers: {
                    'Content-Type': undefined
                },
                data: formData
            });
    };

    this.getActiveUser = function () {
        console.log("получение активного юзера")
        return $http.get(urlAPI + "GetActiveUser");
    };

    this.getAllUsers = function (id) {
        console.log("идем");
        return $http.get(urlAPI + "GetAllUsers/" + id );
    };

    this.getAllGroups = function () {
        return $http.get(urlAPI + "GetAllGroups");
    }; 

    this.addFriends = function (idU,idF) {
        console.log("далее");
        return $http.get(urlAPI + "GetAddNewFriend/" + idU + "/" +idF);
    };

    this.getFriends = function (id) {
        return $http.get(urlAPI + "GetFriends/" + id);
    };

    this.addGroupUser = function (idU, idG) {
        return $http.get(urlAPI + "GetAddGroupUser/" + idU + "/" + idG);
    };

    this.getMyGroups = function (id) {
        return $http.get(urlAPI + "GetMyGroups/" + id);
    };

    this.getActiveUserPosts = function (id) {
        console.log("посты");
        return $http.get(urlAPI + "GetActiveUserPosts/" + id);
    };

    this.removeFriend = function (id) {
        return $http.get(urlAPI + "GetRemoveFriend/" + id);
    };

    this.getSubscribers = function (name) {
        return $http.get(urlAPI + "getSubscribers/" + name);
    };
    
});
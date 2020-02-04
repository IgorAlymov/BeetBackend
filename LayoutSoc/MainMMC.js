
var model = [
    { name: "Игорь Алымов", path: "Я.jpg" },
    { name: "Лена Кот", path: "Лена.jpg" },
    { name: "Сергей Чеботарёв", path: "Сергей.jpg" },
    { name: "Тимур Пулатов", path: "Тимур.jpg" },
    
    { name: "Просто Егор", path: "Егор.jpg" },
    { name: "Ксюша Завизион", path: "Ксюша.jpg" },
    { name: "Jo Jo", path: "Yo.jpg" }
    
];

var mainApp = angular.module('mainApp', ['ngMaterial', 'ngMessages'])
    .controller("mainController", function ($scope, $mdColorPalette, $mdDialog, $timeout, $http, fileReader, ngApiService) {
    
        $scope.activePage = "Pages/Entry.html";
        $scope.showItemReg = true;
        $scope.showCirc = false;
        $scope.showSimple = false;
        $scope.friends = model;
        $scope.showDetailedInformation = false;

        $scope.btnLoadRegstration = function () {
        $scope.activePage = "Pages/Registration.html";
        };

        $scope.btnLoadMyPage = function () {
            $scope.activePage = "Pages/MyPage.html";
            
        };

        $scope.btnLoadFriend = function () {
            $scope.showAvatarBall = false;
            $scope.showAvatarBallInput = true;
        $scope.activePage = "Pages/Friends.html";
        };

    $scope.btnLoadGroups = function () {
            $scope.activePage = "Pages/Groups.html";
        };

    $scope.btnOpenGroup = function () {
            $scope.activePage = "Pages/Group.html";
        };

        $scope.btnLoadPhotos = function () {
            $scope.activePage = "Pages/Photos.html";
        };

        $scope.btnLoadMessages = function () {
            $scope.activePage = "Pages/Messages.html";
        };

        $scope.btnFindFriendsPage = function () {

            $scope.activePage = "Pages/FindFriends.html";
        };

        $scope.btnFindGroupsPage = function () {
            
            $scope.activePage = "Pages/FindGroups.html";
        };

        $scope.btnLoadPageAddGroup = function () {

            $scope.activePage = "Pages/AddGroupPage.html";
        };

        $scope.btnLoadFriendPage = function () {

            $scope.activePage = "Pages/FriendPage.html";
        };

        $scope.btnLoadMessage = function (friend,friendPh) {
            $scope.friendMes = friend;
            $scope.friendMesPh = friendPh;
            $scope.activePage = "Pages/Message.html";
        };

        $scope.btnEntry = function () {
            $scope.showCirc = false;
            $scope.showSimple = false;
            $scope.showItemReg = true;
            $scope.activePage = "Pages/Entry.html";
        };


        //SignalR
        $scope.messages = [];
        $scope.chatUsers = [];

        var hub = $.connection.chatHub;

        // функция подлючения к Хабу...
        $scope.connect = function () {
            console.log("попытка подключения:");
            // крутилку добавить
            $.connection.hub
                .start()
                .done(function () {
                    
                    console.log("есть соединение!");
                    $scope.$apply();
                    $scope.login();
                })
                .fail(function (error) {
                    console.log("ошибка подключения: "
                        + error)
                });
        };

        // функция входа в чата
        $scope.login = function () {
            hub.server.connectUser($scope.activeUser.firstname);
        }
        $scope.outcomeMessage;

        // функция отправки сообщения
        $scope.sendToServer = function (text) {
            console.log("отправка сообщения "
                + text);

            hub.server.sendPublicMessage(
                $scope.activeUser.firstname,
                text);
        }

        hub.client.addMessage = function (name, msg) {
            $scope.author = name;
            $scope.incomeMessage = msg;
            console.log("пришло сообщение от "
                + $scope.author + ": "
                + $scope.incomeMessage);

            if (name == $scope.activeUser.firstname) {
                $scope.messages.push({
                    name: $scope.author,
                    text: $scope.incomeMessage,
                    image: $scope.activeUserAvatarImage
                });
            }
            else {
                $scope.messages.push({
                    name: $scope.author,
                    text: $scope.incomeMessage,
                    image: $scope.friendMesPh
                });
            }
            
            $scope.$apply();
        }

        hub.client.onConnected = function (id, userName, allUsers) {
            console.log("connected, Id: " + id);
            for (var i = 0; i < allUsers.length; i++) {
                $scope.chatUsers.push(
                    {
                        Id: allUsers[i].ConnectId,
                        Name: allUsers[i].Name
                    }
                );
            }
            $scope.isLogin = true;

            $scope.$apply();
        };

        hub.client.onNewUserConnected = function (id, userName) {
            console.log("New user: Id "
                + id
                + ", name "
                + userName);

            $scope.chatUsers.push(
                {
                    Id: id,
                    Name: userName
                }
            );
            $scope.$apply();
        };
        


        //лайки
        $scope.btnLike = function (idLike, idPost) {
            
            if ($scope.likeIcons[idLike] == "img/icons/heartOne.svg") {
                $scope.likeIcons[idLike] = "img/icons/heartTwo.svg";

                for (var i = 0; i < $scope.myPosts.length; i++) {
                    if ($scope.myPosts[i].postId == idPost) {

                        var count = $scope.myPosts[i].likesCounter;
                        count = count + 1;
                        $scope.myPosts[i].likesCounter = count;
                    }
                }
            }
            else {
                $scope.likeIcons[idLike] == "img/icons/heartTwo.svg";
                $scope.likeIcons[idLike] = "img/icons/heartOne.svg";

                for (var i = 0; i < $scope.myPosts.length; i++) {
                    if ($scope.myPosts[i].postId == idPost) {

                        var count = $scope.myPosts[i].likesCounter;
                        count = count - 1;
                        $scope.myPosts[i].likesCounter = count;
                    }
                }
            }  
        };

        $scope.addLikePost = function (idPost) {
            ngApiService
                .addLikePost(idPost)
                .then(function (res) {

                }, function (err) {
                    console.log("error", err);
                });
        };

        //регистрация
        $scope.regUser = {
            firstName: '',
            lastName: '',
            email: '',
            birthday: '',
            password: '',
            passwordRepeat: '',
            gender: '',
            phoneNumber: '',
            city: '',
            aboutMe:''
        };
        $scope.registerUser = function (user) {
            $scope.showAvatarBall = false;
            $scope.showAvatarBallInput = true;
            $scope.showCirc = true;
            $scope.showItemReg = false;
            ngApiService
                .registerUser(user)
                .then(function (res) {
                    console.log("отправка картинки при регистрации");
                    console.log(res);
                    $scope.showSimple = true;
                    $scope.getActiveUserReg();
                    
                   // $scope.sendImage();
                    $scope.showSimple = true;
                }, function (err) {
                    console.log("error", err);
                });
        }

        $scope.getActiveUserReg = function () {
            ngApiService.getActiveUser()
                .then(function (responce) {
                    $scope.activeUser = responce.data;
                    console.log($scope.activeUser);
                    $scope.sendImage();
                    $scope.connect();
                }, function (error) {
                    console.log("error:", error);
                });
        }

        //добавление группы
        $scope.addGroupName = '';
        $scope.addGroup = function (groupName) {
            $scope.showAvatarBall = false;
            $scope.showAvatarBallInput = true;

            ngApiService
                .addGroup(groupName, $scope.ImageSend)
                .then(function (res) {
                    console.log("Группа добавлена!");
                    $scope.btnLoadGroups();
                }, function (err) {
                    console.log("error", err);
                });
        };

        //вход
        $scope.error = "";
        $scope.entryUser = function (user) {
            $scope.showCirc = true;
            $scope.showItemReg = false;
            ngApiService.entryUser(user).
                then(function (res) {
                    console.log("Вход выполнен");
                    $scope.getActiveUser();
                    $scope.btnLoadMyPage();
                    $scope.showSimple = true;
                }, function (err) {
                    $scope.showCirc = false;
                    $scope.showItemReg = true;
                    console.log("error", err);
                    $scope.error = err.statusText;
                });
        }

        $scope.signOut = function () {
            ngApiService.SignOut().
                then(function () {

                }, function (err) {
                    console.log("error", err);
                });
        }

        //добавление фото
        $scope.imageSrc = "";
        $scope.rememberFile = function (files) {
            console.log("remember:");
            $scope.ImageSend = files[0];
        };
        $scope.sendImage = function () {
            console.log("отправка картинки на сервер");
            ngApiService
                .sendAvatar($scope.activeUser.socialUserId, $scope.ImageSend)
                .then(function (res) {
                    $scope.getActiveUser();
                    $scope.btnLoadMyPage();
                    $scope.showAlert();
                    $scope.ImageSend = "";
                }, function (err) {
                    console.log("error", err);
                });
        };

        $scope.sendAddImage = function () {
            console.log("отправка картинки на сервер");
            $scope.imageSrc = "";
            $scope.showAvatarBall = false;
            $scope.showAvatarBallInput = true;
            ngApiService
                .sendAddPhoto($scope.activeUser.socialUserId, $scope.ImageSend)
                .then(function (res) {
                    console.log("avatar send success");
                    console.log(res);
                    
                    $scope.getAllPhotos(activeUser.socialUserId);
                    // todo: отключаем "крутилку"
                }, function (err) {
                    console.log("error", err);
                    $scope.error = err.textContent;
                });
        };

        //показ подробной информации
        $scope.showDetailedInformations = function () {
            if ($scope.showDetailedInformation == false)
                $scope.showDetailedInformation = true;
            else
                $scope.showDetailedInformation = false;
        };

        $scope.activeUser=null;
        //получение информации о текущем пользователе
        $scope.getActiveUser = function () {
            ngApiService.getActiveUser()
                .then(function (responce) {
                    $scope.activeUser = responce.data;
                    console.log($scope.activeUser);

                   $scope.getFriends($scope.activeUser.socialUserId);
                   $scope.getAllPhotos($scope.activeUser.socialUserId);
                   $scope.getMyGroups($scope.activeUser.socialUserId);
                    $scope.getActiveUserAvatar($scope.activeUser);
                    $scope.getActiveUserPosts($scope.activeUser.socialUserId);
                    $scope.connect();
                }, function (error) {
                    console.log("error:", error);
                });
        }

        //получение информации о друге
        $scope.getFriendPage = function (friend) {
            console.log(friend);
            $scope.activeFriend = friend;
            $scope.getFriends(friend.socialUserId);
            $scope.getAllPhotos(friend.socialUserId);
            $scope.getMyGroups(friend.socialUserId);
            $scope.getActiveUserAvatar(friend);
            $scope.getFriendPosts(friend.socialUserId);
            
        };

        //все пользователи
        $scope.allUsers = [];
        $scope.allUsersAvatarPhoto = [];
        $scope.getallUsers = function (id) {
            ngApiService.getAllUsers(id)
                .then(function (responce) {

                    console.log("Юзеры получен");
                    $scope.allUsers = responce.data;

                    for (var i = 0; i < $scope.allUsers.length; i++) {
                        ngApiService.getAvatar($scope.allUsers[i].socialUserId)
                            .then(function (res) {
                                console.log("аватарки пошли");
                                $scope.allUsersAvatarPhoto.push(res.data);
                            }, function (err) {
                                console.log("error", err);
                            });
                    }
                    console.log($scope.allUsersAvatarPhoto);
                    $scope.btnFindFriendsPage();
                    console.log($scope.allUsers);
                }, function (error) {
                    console.log("error:", error);
                });
        }

        $scope.allGroups = [];
        $scope.allGroupsImage = [];
        $scope.getAllGroups = function () {
            ngApiService.getAllGroups()
                .then(function (responce) {
                    $scope.allGroupsImage = [];
                    console.log("группы получены");
                    $scope.allGroups = responce.data;
                    console.log($scope.allGroups);
                    for (var i = 0; i < $scope.allGroups.length; i++) {

                        ngApiService.getImageGroup($scope.allGroups[i].groupId)
                            .then(function (res) {
                                console.log("аватарки пошли");
                                $scope.allGroupsImage.push(res.data);
                            }, function (err) {
                                console.log("error", err);
                            });
                        console.log($scope.allGroupsImage);
                        $scope.btnFindGroupsPage();
                    }
                }, function (error) {
                    console.log("error:", error);
                });
        };

        //получение аватарки
        $scope.activeUserAvatarImage = "img/YPhoto.png";
        $scope.getActiveUserAvatar = function (u) {
            console.log(u.socialUserId);
            ngApiService
                .getAvatar(u.socialUserId)
                .then(function (res) {
                    console.log("avatar success");
                    $scope.activeUserAvatarImage = res.data.avatarUrl;
                    console.log($scope.activeUserAvatarImage);
                    
                }, function (err) {
                    console.log("error", err);
                });
        };

        $scope.allPhotosUser = [];
        $scope.countPhotos = [];

        $scope.getAllPhotos = function (id) {
            $scope.allPhotosUser = [];
            $scope.getCountPhotos(id);
            console.log($scope.allPhotosUser);
        };

        $scope.getCountPhotos = function (id) {
            console.log("количество получено");
            ngApiService
                .getCountPhotos(id)
                .then(function (res) {

                    console.log(res);
                    $scope.countPhotos = res.data;
                    $scope.allPhotosUser = [];
                    for (var i = 0; i < $scope.countPhotos.length; i++) {
                        ngApiService
                            .getAllPhotoUser($scope.countPhotos[i].photoId, $scope.activeUser.socialUserId)
                            .then(function (res) {

                                $scope.allPhotosUser.push(res.data);

                            }, function (err) {
                                console.log("error", err);
                            });
                    }
                    console.log($scope.allPhotosUser);
                    
                }, function (err) {
                    console.log("error", err);
                });
        };



        //добавление друга
        $scope.addFriends = function (idU,idF) {
            ngApiService
                .addFriends(idU, idF)
                .then(function (res) {
                    console.log("добавление закончилось");
                    $scope.threeFriends[0] = null;
                }, function (err) {
                    console.log("error", err);
                });
        };

        //добавление в группу
        $scope.addGroupUser = function (idU, idG) {
            console.log(idU, idG);
            ngApiService
                .addGroupUser(idU, idG)
                .then(function () {
                    console.log("добавление закончилось");
                }, function (err) {
                    console.log("error", err);
                });
        };

        //получение моих групп
        $scope.myGroups = [];
        $scope.myGroupsImages = [];
        $scope.getMyGroups = function (id) {
            console.log("получение групп");
            ngApiService
                .getMyGroups(id)
                .then(function (res) {
                    console.log("Удалось группы");
                    $scope.myGroupsImages = [];
                    $scope.myGroups = res.data;
                    console.log($scope.myGroups);
                    for (var i = 0; i < $scope.myGroups.length; i++) {
                        ngApiService.getImageGroup($scope.myGroups[i].groupId)
                            .then(function (res) {
                                console.log("аватарки пошли");
                                $scope.myGroupsImages.push(res.data);
                            }, function (err) {
                                console.log("error", err);
                            });
                    }

                    console.log($scope.myGroups);
                    console.log($scope.myGroupsImages);
                }, function (err) {
                    console.log("error", err);
                });
        };

        //получение моих постов
        $scope.likeIcons = [];
        $scope.myPosts = [];
        $scope.myPostsImages = [];

        $scope.getActiveUserPosts = function (id) {
            ngApiService
                .getActiveUserPosts(id)
                .then(function (res) {
                    $scope.myPosts = res.data;
                    console.log($scope.myPosts);
                    $scope.myPostsImages = [];
                    $scope.likeIcons = [];
                    for (var i = 0; i < $scope.myPosts.length; i++) {
                        ngApiService.getImagePosts($scope.myPosts[i].postId)
                            .then(function (res) {
                                $scope.myPostsImages.unshift(res.data);
                                $scope.likeIcons.unshift("img/icons/heartOne.svg");
                            }, function (err) {
                                $scope.myPostsImages.unshift("");
                                $scope.likeIcons.unshift("img/icons/heartOne.svg");
                            });
                    }
                    console.log("RFHNBYRB");
                    console.log($scope.myPostsImages);
                    $scope.btnLoadMyPage();
                }, function (err) {
                    console.log("error", err);
                });
        };

        //посты друзей
        $scope.getFriendPosts = function (id) {
            ngApiService
                .getActiveUserPosts(id)
                .then(function (res) {
                    $scope.myPosts = res.data;
                    console.log($scope.myPosts);
                    $scope.myPostsImages = [];
                    $scope.likeIcons = [];
                    for (var i = 0; i < $scope.myPosts.length; i++) {
                        ngApiService.getImagePosts($scope.myPosts[i].PostId)
                            .then(function (res) {
                                var blob = new Blob([res.data], { type: 'image/png' });
                                $scope.myPostsImages.push((window.URL || window.webkitURL)
                                    .createObjectURL(blob));
                                $scope.likeIcons.push("img/icons/heartOne.svg");
                            }, function (err) {
                                $scope.myPostsImages.push("");
                                $scope.likeIcons.push("img/icons/heartOne.svg");
                            });
                    }
                    $scope.btnLoadFriendPage();
                }, function (err) {
                    console.log("error", err);
                });
        };

        //получение друзей
        $scope.allFriends = [];
        $scope.friendsUsersAvatarPhoto = [];
        $scope.getFriends = function (id) {
            ngApiService
                .getFriends(id)
                .then(function (res) {
                    $scope.allFriends = res.data;
                    console.log($scope.allFriends);
                    $scope.friendsUsersAvatarPhoto = [];
                    for (var i = 0; i < $scope.allFriends.length; i++) {
                        ngApiService.getAvatar($scope.allFriends[i].socialUserId)
                            .then(function (res) {
                                $scope.friendsUsersAvatarPhoto.push (res.data);
                            }, function (err) {
                                console.log("error", err);
                            });
                    }
                    
                    console.log($scope.friendsUsersAvatarPhoto);
                }, function (err) {
                    console.log("error", err);
                });
        };
        
        //аватар при регистрации
        $scope.showAvatarBall = false;
        $scope.showAvatarBallInput = true;
        $scope.inputAvatar = function () {
            $scope.showAvatarBall = true;
            $scope.showAvatarBallInput = false;
        }

        //удаление друга
        $scope.removeFriend = function (id) {
            ngApiService
                .removeFriend(id)
                .then(function (res) {
                    $scope.getFriends($scope.activeUser.socialUserId);
                }, function (err) {
                    console.log("error", err);
                });
        };
        
        //открытие моей группы 
        $scope.subscribers;
        $scope.subscribersAvatar = [];
        $scope.openActiveGroupMyGroups = function (name) {
            for (var i = 0; i < $scope.myGroups.length; i++) {
                if ($scope.myGroups[i].name == name) {
                    $scope.myActiveGroup = $scope.myGroups[i];
                    $scope.myActiveGroupImage = $scope.myGroupsImages[i];

                    ngApiService
                        .getSubscribers(name)
                        .then(function (res) {
                            $scope.subscribers = res.data;
                            console.log($scope.subscribers);
                            //нужно получить все фото юзеров

                            for (var i = 0; i < $scope.subscribers.length; i++) {

                                ngApiService.getAvatar($scope.subscribers[i].socialUserId)
                                    .then(function (res) {
                                        console.log("аватарки пошли");
                                        $scope.subscribersAvatar.push(res.data);
                                    }, function (err) {
                                        console.log("error", err);
                                    });
                            }
                            
                        }, function (err) {
                            console.log("error", err);
                        });
                }
            }
            console.log($scope.subscribersAvatar);
            console.log($scope.subscribers);
            console.log($scope.myActiveGroup);
            console.log($scope.myActiveGroupImage);
        };

        //добавление поста 
        $scope.textPost = '';
        $scope.addPost = function (text) {
            $scope.showAvatarBall = false;
            console.log(text);
            ngApiService
                .addPost(text, $scope.ImageSend)
                .then(function (res) {
                    $scope.ImageSend = '';
                    $scope.getActiveUserPosts($scope.activeUser.socialUserId);
                }, function (err) {
                    console.log("error", err);
                })
        };





        $scope.currentNavItem = 'page1';
        $scope.goto = function (page) {
            $scope.status = "Goto " + page;
        };
    $scope.colors = Object.keys($mdColorPalette);
    $scope.mdURL = 'https://material.google.com/style/color.html#color-color-palette';
    $scope.primary = 'purple';
    $scope.accent = 'green';

    $scope.isPrimary = true;
    $scope.numbers = model;
    $scope.selectTheme = function (color) {
        if ($scope.isPrimary) {
            $scope.primary = color;

            $scope.isPrimary = false;
        }
        else {
            $scope.accent = color;

            $scope.isPrimary = true;
        }
    };

    $scope.status = '  ';
    $scope.customFullscreen = false;

    $scope.showAlert = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        // Modal dialogs should fully cover application
        // to prevent interaction outside of dialog
        $mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title('Регистрация успешно завершена')
                .ariaLabel('Alert Dialog Demo')
                .ok('хорошо')
                .targetEvent(ev)

        );
    };

    $scope.showConfirm = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete your debt?')
            .textContent('All of the banks have agreed to forgive you your debts.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Please do it!')
            .cancel('Sounds like a scam');

        $mdDialog.show(confirm).then(function () {
            $scope.status = 'You decided to get rid of your debt.';
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    $scope.showPrompt = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        var confirm = $mdDialog.prompt()
            .title('What would you name your dog?')
            .textContent('Bowser is a common name.')
            .placeholder('Dog name')
            .ariaLabel('Dog name')
            .initialValue('Buddy')
            .targetEvent(ev)
            .required(true)
            .ok('Okay!')
            .cancel('I\'m a cat person');

        $mdDialog.show(confirm).then(function (result) {
            $scope.status = 'You decided to name your dog ' + result + '.';
        }, function () {
            $scope.status = 'You didn\'t name your dog.';
        });
    };

        $scope.showAdvanced = function (ev) {
            
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'dialog1.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: $scope.customFullscreen
        })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
        
    };

    $scope.showTabDialog = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'tabDialog.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
    };

    $scope.showPrerenderedDialog = function (ev) {
        $mdDialog.show({
            contentElement: '#myDialog',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        });
    };

    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    }
})
    .directive('themePreview', function () {
        return {
            restrict: 'E',
            templateUrl: 'themePreview.tmpl.html',
            scope: {
                primary: '=',
                accent: '='
            },
            controller: function ($scope, $mdColors, $mdColorUtil) {
                $scope.getColor = function (color) {
                    return $mdColorUtil.rgbaToHex($mdColors.getThemeColor(color));
                };
            }
        };
    })
    .directive('mdJustified', function () {
        return {
            restrict: 'A',
            compile: function (element, attrs) {
                var layoutDirection = 'layout-' + (attrs.mdJustified || "row");

                element.removeAttr('md-justified');
                element.addClass(layoutDirection);
                element.addClass("layout-align-space-between-stretch");

                return angular.noop;
            }
        };
    });

mainApp.controller('AppCtrl', ['$interval',
    function ($interval) {
        
        var self = this;

        self.activated = true;
        self.determinateValue = 30;

        // Iterate every 100ms, non-stop and increment
        // the Determinate loader.
        $interval(function () {

            self.determinateValue += 1;
            if (self.determinateValue > 100) {
                self.determinateValue = 30;
            }

        }, 100);
    }
]);

mainApp.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('docs-dark', 'default')
        .primaryPalette('yellow')
        .dark();
});
mainApp.controller('AppCtrl', function ($scope, $mdDialog) {
    $scope.imagePath = 'img/imageFrinds/Я.jpg';
    //меню
    var originatorEv;

    this.openMenu = function ($mdMenu, ev) {
        originatorEv = ev;
        $mdMenu.open(ev);
    };

    this.notificationsEnabled = true;
    this.toggleNotifications = function () {
        this.notificationsEnabled = !this.notificationsEnabled;
    };

    this.redial = function () {
        $mdDialog.show(
            $mdDialog.alert()
                .targetEvent(originatorEv)
                .clickOutsideToClose(true)
                .parent('body')
                .title('Suddenly, a redial')
                .textContent('You just called a friend; who told you the most amazing story. Have a cookie!')
                .ok('That was easy')
        );

        originatorEv = null;
    };
    //диалоги
    $scope.status = '  ';
    $scope.customFullscreen = false;

    $scope.showAlert = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        // Modal dialogs should fully cover application
        // to prevent interaction outside of dialog
        $mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title('This is an alert title')
                .textContent('You can specify some description text in here.')
                .ariaLabel('Alert Dialog Demo')
                .ok('Got it!')
                .targetEvent(ev)
        );
    };

    $scope.showConfirm = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete your debt?')
            .textContent('All of the banks have agreed to forgive you your debts.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Please do it!')
            .cancel('Sounds like a scam');

        $mdDialog.show(confirm).then(function () {
            $scope.status = 'You decided to get rid of your debt.';
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    $scope.showPrompt = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        var confirm = $mdDialog.prompt()
            .title('What would you name your dog?')
            .textContent('Bowser is a common name.')
            .placeholder('Dog name')
            .ariaLabel('Dog name')
            .initialValue('Buddy')
            .targetEvent(ev)
            .required(true)
            .ok('Okay!')
            .cancel('I\'m a cat person');

        $mdDialog.show(confirm).then(function (result) {
            $scope.status = 'You decided to name your dog ' + result + '.';
        }, function () {
            $scope.status = 'You didn\'t name your dog.';
        });
    };

    $scope.showAdvanced = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: '/Pages/Dialogs/tabDialog.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
        })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
    };

    $scope.showTabDialog = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'tabDialog.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        })
            .then(function (answer) {
                $scope.status = 'You said the information was "' + answer + '".';
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
    };

    $scope.showPrerenderedDialog = function (ev) {
        $mdDialog.show({
            contentElement: '#myDialog',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        });
    };

    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    }
})
    .config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('dark-grey').backgroundPalette('grey').dark();
        $mdThemingProvider.theme('dark-orange').backgroundPalette('orange').dark();
        $mdThemingProvider.theme('dark-purple').backgroundPalette('deep-purple').dark();
        $mdThemingProvider.theme('dark-blue').backgroundPalette('blue').dark();
    });
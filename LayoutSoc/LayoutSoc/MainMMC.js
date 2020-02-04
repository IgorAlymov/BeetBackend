
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

    $scope.btnLoadRegstration = function () {
        $scope.activePage = "Pages/Registration.html";
    };
    $scope.btnLoadMyPage = function () {
        $scope.activePage = "Pages/MyPage.html";
        };

    $scope.btnLoadFriend = function () {
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

    $scope.startReg = function () {
        
        $timeout(function () {
            $scope.showCirc = false;
            $scope.showItemReg = true;
            $scope.btnLoadMyPage();
            $timeout(function () {
                $scope.showAlert();
            }, 1000);
        }, 5000);
        
        };

        $scope.friends = model;
        $scope.parametr = "Параметры";

        $scope.likeIcon = "img/icons/heartOne.svg";
        $scope.likeCount = 5;
        $scope.btnLike = function () {
            if ($scope.likeIcon == "img/icons/heartOne.svg") {
                $scope.likeIcon = "img/icons/heartTwo.svg";
                $scope.likeCount++;
            }
            else {
                $scope.likeIcon = "img/icons/heartOne.svg";
                $scope.likeCount--;
            }  
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
            $scope.showCirc = true;
            $scope.showItemReg = false;
            ngApiService
                .registerUser(user)
                .then(function (res) {
                    console.log("Success!");
                    console.log(res);
                    $scope.sendImage();
                    $scope.btnLoadMyPage();
                    $scope.showAlert();
                }, function (err) {
                    console.log("error", err);
                });
        }

        //добавление фото
        $scope.imageSrc = "img/YPhoto.png";

        $scope.rememberFile = function (files) {
            console.log("remember:");
            $scope.ImageSend = files[0];
            console.log($scope.ImageSend);
        };

        $scope.sendImage = function () {
            console.log("отправка картинки на сервер");

            ngApiService
                .sendAvatar(1, $scope.ImageSend)
                .then(function (res) {
                    console.log("avatar send success");
                    console.log(res);
                    $scope.btnLoadMyPage();
                    $scope.showAlert();
                    // todo: отключаем "крутилку"
                }, function (err) {
                    console.log("error", err);
                });
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
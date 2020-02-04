// 1) паттерн фабрика - создавать объект для загрузки файла
mainApp.factory("fileReader", function ($q, $log) {
    // пропишем три функции
    var onLoad = function (reader, deffered, scope) {
        return function () {
            // если файл большой
            // то после его загрузки уведомим 
            // области видимости scope об изменениях
            scope.$apply(function () {
                deffered.resolve(reader.result);
            })
        }
    } // onLoad;

    var onError = function (reader, deffered, scope) {
        return function () {
            // если загрузка не удалась - уведомим scope
            scope.$apply(function () {
                deffered.resolve(reader.result);
            })
        }
    } // onError;

    var onProgress = function (reader, scope) {
        return function (event) {
            // рассылка события о процентах загрузки
            scope.$broadcast("fileProgress", {
                total: event.total,
                loaded: event.loaded
            });
        }
    } // onProgress;

    // создаём загрузчик файла
    var getReader = function (deffered, scope) {
        var reader = new FileReader();
        // прикрепляем обработчики событий
        reader.onload = onLoad(reader, deffered, scope);
        reader.onerror = onError(reader, deffered, scope);
        reader.onprogress = onProgress(reader, scope);

        return reader;
    }

    // загрузка файла и преобразование в ссылку для отображения
    var readAsDataURL = function (file, scope) {

        var deffered = $q.defer();

        var reader = getReader(deffered, scope);

        reader.readAsDataURL(file);

        return deffered.promise;
    }

    // фабрика возвращает результат 
    // - загруженную из файла картинку.
    return {
        readAsDataUrl: readAsDataURL
    };
}); // mainApp.factory;

// 2) директива для "выбора файла"
mainApp.directive("ngFileSelect", function (fileReader, $timeout) {
    return {
        // свойства директивы
        scope: {
            ngModel: '='
        },
        link: function ($scope, element) {
            // загрузка файла
            function getFile(file) {
                fileReader
                    .readAsDataUrl(file, $scope)
                    // then - после загрузки файла
                    .then(function (result) {
                        $timeout(function () {
                            $scope.ngModel = result;
                        });
                    });
            }; // getFile();

            // привязка файла к html
            element.bind("change", function (e) {
                var file = (e.srcElement || e.target).files[0];
                getFile(file);
            });
        } // link
    }
}); // mainApp.directive

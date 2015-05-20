angular.module('afterjackApp')
    .factory('dataFactory', ['$http', '$routeParams', function ($http, $routeParams) {

        var urlAfterjack = '/api/afterjack/';          //local
        var dataFactory = {};

        dataFactory.getAfterjacks = function (entryDate, room) {
            return $http({
                url: urlAfterjack + 'GetAfterjacks?entryDate=' + entryDate + '&room=' + room,
                method: 'GET'
            });
        };

        dataFactory.addAfterjack = function (afterjack) {
            return $http.post(urlAfterjack + 'PostAfterjack/', afterjack);
        };

        dataFactory.saveAfterJacks = function (afterjacks) {
            return $http.put(urlAfterjack + 'PutAfterjacks/', afterjacks);
        };

        dataFactory.deleteAfterJack = function (id, afterjackID) {
            return $http.delete(urlAfterjack + 'DeleteAfterjack?id=' + id + '&afterJackID=' + afterjackID);
        };

        return dataFactory;
    }]);

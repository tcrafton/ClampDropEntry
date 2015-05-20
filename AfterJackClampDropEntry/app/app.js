var afterjackApp = angular.module('afterjackApp', ['ui.bootstrap','ngRoute']);

afterjackApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/afterjackEntry', {
        controller: 'afterjackController',        
        templateUrl: '/app/views/afterjackEntry.html',            //local
    })
    .when('/afterjackEntryL3', {
        controller: 'afterjackControllerL3',
        templateUrl: '/app/views/afterjackEntryL3.html',            //local
    })
    .otherwise({ redirectTo: '/afterjackEntry' });
}]);

afterjackApp.directive('enter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                event.preventDefault();
                var fields = $(this).parents('form:eq(0),body').find('input, textarea, select');
                var index = fields.index(this);
                if (index > -1 && (index + 1) < fields.length)
                    fields.eq(index + 1).focus();
            }
        });
    };
});


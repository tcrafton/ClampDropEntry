var app =  angular.module('afterjackApp')
    .controller('afterjackController', ['$scope', '$filter', 'dataFactory',
        function ($scope, $filter, dataFactory) {
            $scope.crew = {
                1: 'H',
                2: 'I',
                3: 'J',
                4: 'K'
            };

            $scope.room = {
                1: 'A',
                2: 'B',
                3: 'C',
                4: 'D'
            };

            $scope.afterjacks;
            $scope.afterjack;
            $scope.pots = [];

            var ajDetails = [];

            function populatePots() {
                for (var i = 1; i < 88; i++) {
                    $scope.pots.push(i);
                }
            };

            $scope.update = function () {
            }

            // Date Start
            $scope.today = function () {
                $scope.dt = new Date();
            };
            $scope.today();

            $scope.clear = function () {
                $scope.dt = null;
            };

            $scope.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                $scope.opened = true;
            };

            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
            $scope.format = $scope.formats[0];


            $scope.getDayClass = function (date, mode) {
                if (mode === 'day') {
                    var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                    for (var i = 0; i < $scope.events.length; i++) {
                        var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                        if (dayToCheck === currentDay) {
                            return $scope.events[i].status;
                        }
                    }
                }

                return '';
            };
            // Date End

            function init(data) {
                populatePots();
                getAfterJacks();
            }

            function getAfterJacks() {
                var rooms = document.getElementById('ddlRoom');
                $scope.date = $filter('date')($scope.dt, 'dd-MMM-yy');
                var room = (document.getElementById('ddlRoom').value !== '' ? document.getElementById('ddlRoom').options[rooms.selectedIndex].text : '');
                var pot = document.getElementById('ddlPot').value;
                var potSelected = parseInt(pot, 10);

                if (room) {
                    dataFactory.getAfterjacks($scope.date, room)
                        .success(function (data) {
                            $scope.afterjacks = data;
                        })
                        .error(function (error) {
                            console.log('Error getAfterJacks:');
                            console.dir(error);
                        })
                }
            };

            function saveAfterJacks() {
                dataFactory.saveAfterJacks(ajDetails)
                    .success(function (data) {
                        // $scope.afterjacks = data;
                    })
                    .error(function (error) {
                        console.log('Error saveAfterJacks:');
                        console.dir(error);
                    })
                    .finally(function () {
                        getAfterJacks();
                    });
            };

            $scope.deleteAfterJack = function () {
                dataFactory.deleteAfterJack(this.pot.ID, (this.pot.AfterJackID > 0 ? this.pot.AfterJackID : 0))
                    .success(function (data) {
                        // $scope.afterjacks = data;
                    })
                    .error(function (error) {
                        console.log('Error deleteAfterJack:');
                        console.dir(error);
                    })
                    .finally(function () {
                        getAfterJacks();
                    });
            };

            function addAfterJack() {

                if (validateSelections() === true) {

                    // if there's existing afterjacks, save them in case changes have been made
                    if ($scope.afterjacks.length > 0) {
                        saveAllData();
                    }

                    $scope.afterjack.entryDate = $scope.dt;
                    dataFactory.addAfterjack($scope.afterjack)
                         .success(function (data) {
                             $scope.addAfterjack = data;
                         })
                         .error(function (error) {
                             console.log('Error adding After Jack: ' + error);
                         })
                         .finally(function () {
                             getAfterJacks();
                         });
                }

            };

            function validateSelections() {

                var crews = document.getElementById('ddlCrew'),
                    selectedCrew = crews.options[crews.selectedIndex].text,
                    rooms = document.getElementById('ddlRoom'),
                    selectedRoom = rooms.options[rooms.selectedIndex].text,
                    pot = document.getElementById('ddlPot').value,
                    selectedPot = parseInt(pot, 10);

                if (selectedCrew === '') {
                    $('#myModal').modal('show');
                    return false;
                } else if (selectedRoom === '') {
                    $('#myModal').modal('show');
                    return false;
                } else if (isNaN(selectedPot)) {
                    $('#myModal').modal('show');
                    return false;
                }

                return true;
            };

            $scope.UpdateAfterjacks = function () {
                //  setTimeout(function () {
                addAfterJack();
                // }, 1000);

            };

            function AfterJackDetail(id, afterJackId, anodeNum, clampDrop) {
                this.id = id;
                this.afterJackId = afterJackId;
                this.anodeNum = anodeNum;
                this.clampDrop = clampDrop;
            };

            function saveAllData() {
                var aj = $scope.afterjacks,
                    i = 0,
                    x = 1;

                ajDetails = [];
                for (i; i < aj.length; i++) {
                    // loop for each anode
                    x = 1;
                    for (x; x < 19; x++) {
                        if (aj[i]["A" + x]) {
                            var ajDetail = new AfterJackDetail(0, aj[i]["ID"], x, aj[i]["A" + x]);
                            ajDetails.push(ajDetail);
                        }
                    }
                }

                saveAfterJacks();
            };

            $scope.Save = function () {
                saveAllData();
                $('#saveModal').modal('show');
            }

            init();

            $scope.$watch('dt', function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    getAfterJacks();
                }
            });

            $scope.$watch('afterjack.room', function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    getAfterJacks();
                }
            });
        }]);



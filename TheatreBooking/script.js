angular.module('application', [])
.controller('MainController', function ($scope, $http) {

    $scope.seats = [];
    $scope.mySelection = [];

	$scope.getAllSeats = function () {
        $http.get("http://localhost:4854/Seats/GetSeats")
        .then(function (result) {
            console.log(result.data);

            //fillseats
            $scope.seats = [];
            _(result.data).each(function (s) {
                $scope.seats[s.ID] = s;
            });

        }, function (error) {
            console.log(error);
        })
    }

    $scope.getAllSeats();

    $scope.seatSelected = function (event) {
        var elemID = parseInt(event.target.id);

        var currentSeat = $scope.seats[elemID];

        //if was selected by me or is available
        if ($scope.mySelection[elemID] !== undefined || currentSeat.Status === 0) {

            //send select request
            if (elemID !== undefined) {
                $http.get("http://localhost:4854/Seats/Select?id=" + elemID)
                    .then(function (result) {

                        //add to mySelection if it was selected by me
                        if (currentSeat.Status === 0 && result.data.Status === 1) {
                            $scope.mySelection[result.data.ID] = result.data;
                        } else if (currentSeat.Status === 1 && result.data.Status === 0) {
                            $scope.mySelection[result.data.ID] = undefined;
                        }

                        $scope.getAllSeats();

                        console.log(result.data);
                    }, function (error) {
                        console.log(error);
                    });
            };
        }
    }

    $scope.book = function () {

        var ids = _($scope.mySelection).filter(function (s) {
            return s !== undefined
        }).map(function (s) {
            return s.ID
        })

        if (ids !== undefined && ids.length > 0) {
            $http.post("http://localhost:4854/Seats/Book", { ids: ids, FirstName: $scope.firstName, LastName: $scope.lastName, Email: $scope.email, PhoneNumber: $scope.phoneNumber })
                .then(function (result) {
                    $scope.showSuccess = true;
                    console.log(result.data);
                    $scope.getAllSeats();
                    $scope.mySelection = [];
                }, function (error) {
                    console.log(error);
                });
        }
    }
	
	Tipped.create('.seat', function(element) {
		var id = $(element).attr('id');
		return {
           content: "<p class='tooltip-header'>" + $scope.seats[id].AreaDescription + "</p><p>" + $scope.seats[id].RowName + " " + $scope.seats[id].RowNumber + " Место " + $scope.seats[id].SeatNumber + "</p>"
      }
    }, {
		behavior: 'mouse'
	}
	);
      //return "<p class='tooltip-header'>" + $scope.seats[id].AreaDescription + "</p><p>" + $scope.seats[id].RowName + " " + $scope.seats[id].RowNumber + " Место " + $scope.seats[id].SeatNumber + "</p>";
    //});
})



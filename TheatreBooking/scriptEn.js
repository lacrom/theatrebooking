angular.module('application', [])
.controller('MainController', function ($scope, $http) {

    $scope.seats = {};
    $scope.mySelection = {};
    $scope.showSuccess = false;

	$scope.getAllSeats = function () {
	    $http.get("/Seats/GetSeats")
	        .then(function(result) {
	            //console.log(result.data);

	            //fillseats
	            $scope.seats = [];
	            _(result.data).each(function(s) {
	                $scope.seats[s.ID] = s;
	            });

	            $("#loader").addClass("hidden");
	            $("#main").removeClass("hidden");

	        }, function(error) {
	            console.log(error);
	        });
	}

    $scope.getAllSeats();

    $scope.seatSelected = function (event) {
        var elemID = parseInt(event.target.id);

        var currentSeat = $scope.seats[elemID];

        //if was selected by me or is available
        if ($scope.mySelection[elemID] !== undefined || currentSeat.Status === 0) {

            var prevStatus = currentSeat.Status;

            if (currentSeat.Status === 1) {
                currentSeat.Status = 0;
                delete $scope.mySelection[currentSeat.ID];
            } else if (currentSeat.Status === 0) {
                currentSeat.Status = 1;
                $scope.mySelection[currentSeat.ID] = currentSeat;
            }

            //send select request
            if (elemID !== undefined) {
                $http.get("/Seats/Select?id=" + elemID + "&selected=" + prevStatus)
                    .then(function (result) {

                        if (result.data) {
                            //add to mySelection if it was selected by me
                            if (result.data.Status === 1) {
                                $scope.mySelection[result.data.ID] = result.data;
                            } else if (result.data.Status === 0) {
                                delete $scope.mySelection[result.data.ID];
                            }
                            $scope.seats[result.data.ID] = result.data;
                        } else {
                            delete $scope.mySelection[currentSeat.ID];
                        }
                        
                        //$scope.getAllSeats();

                        //console.log(result.data);
                    }, function (error) {
                        console.log(error);
                    });

                
            };
        }
    }

    $scope.book = function () {

        var ids = _($scope.mySelection).filter(function(s) {
            return s !== undefined
        }).map(function(s) {
            return s.ID;
        });

        if (ids !== undefined && ids.length > 0) {
            $http.post("/Seats/Book", { ids: ids, FirstName: $scope.firstName, LastName: $scope.lastName, Email: $scope.email, PhoneNumber: $scope.phoneNumber, face: $scope.face, participation: $scope.participation })
                .then(function (result) {
                    $scope.getAllSeats();
                    $scope.mySelection = {};
                    $scope.firstName = undefined;
                    $scope.lastName = undefined;
                    $scope.email = undefined;
                    $scope.phoneNumber = undefined;
                    $scope.face = undefined;
                    $scope.participation = undefined;
                    $scope.form.$setPristine();
                }, function (error) {
                    console.log(error);
                });
        }
    }
	
	Tipped.create('.seat', function(element) {
	    var id = $(element).attr('id');
	    var price = "<div>Booked by Big Theater</div>";
	    var information = "<p>" + $scope.seats[id].InformationEn + "</p>";
	    var bronfo = "";
	    if ($scope.seats[id].Price != null) {
	        price = "<div>Price: <strong>" + $scope.seats[id].Price + "</strong> rub.</div>";
	    }
	    if ($scope.seats[id].Status === 2) {
	        bronfo = "<div>Booked: <strong>" + $scope.seats[id].BookedBy.FirstName + " " + $scope.seats[id].BookedBy.LastName + "</strong></div>";
	        if ($scope.seats[id].BookedBy.Face) {
	            bronfo = bronfo + "<div><strong>" + $scope.seats[id].BookedBy.Face + "</strong></div>";
	        }
	    }
	        return {
	            content: "<p class='tooltip-header'>" + $scope.seats[id].AreaDescriptionEn + "</p>" +
	                "<div>" + $scope.seats[id].RowNameEn + " <strong>" + $scope.seats[id].RowNumber + "</strong> Seat <strong>" + $scope.seats[id].SeatNumber + "</strong></div>" + information + price + bronfo
	    }
    }, {
        behavior: 'mouse',
        cache: false
	}
	);
      //return "<p class='tooltip-header'>" + $scope.seats[id].AreaDescription + "</p><p>" + $scope.seats[id].RowName + " " + $scope.seats[id].RowNumber + " Место " + $scope.seats[id].SeatNumber + "</p>";
    //});

    $scope.isEmptyArray = function(arr) {
        return _.isEmpty(arr);
    }
})



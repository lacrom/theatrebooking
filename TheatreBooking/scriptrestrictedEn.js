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
	setInterval($scope.getAllSeats, 15000);

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



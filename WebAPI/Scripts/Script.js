

//var AngularApp = angular
//    .module("AngularApp", [])
//    .controller("ProductAPIController", function ($scope, $http) {
//        $http.get('http://localhost:60517/api/ProductAPI').then(function (response) {
//            $scope.products = response.data;
//        });
//    });

//////var AngularApp = angular
//////    .module("AngularApp", [])
//////    .controller("UserController", function ($scope, $http) {
//////        $http.get('http://localhost:60517/api/user').then(function (response) {
//////            $scope.users = response.data;
//////        });
//////    });



var myController = app.controller("ReviewController", function ($scope,productService) {

        $scope.IsNewRecord = 1; //The flag for the new record
       // $scope.product = product;
        loadRecords();

        //Function to load all Employee records
        function loadRecords() {
            var promiseGet = productService.getProducts(); //The MEthod Call from service

            promiseGet.then(function (pl) { $scope.products = pl.data },
                function (errorPl) {
                    $log.error('failure loading products', errorPl);
                });
        }

        $scope.save = function () {
            var product = {
                productID: $scope.productID,
                productName: $scope.productName,
                productDescr: $scope.productDescr,
                productRating: $scope.productRating,
                productReview: $scope.productReview

            };
            
            
            if ($scope.IsNewRecord == 1) {
                alert('in Add');
                var promisePost = productService.post(product);
                promisePost.then(function (pl) {
                    $scope.productID = pl.data.productID;
                    loadRecords();
                    $scope.IsNewRecord = 0;
                    $scope.productID = 0;
                    $scope.productName = "";
                    $scope.productRating = 0;
                    $scope.productDescr = "";
                    $scope.productReview = "";
                }, function (err) {
                    console.log("Err" + err);
                });
            } else { //Else Edit the record
                alert('in Edit');
              
                var promisePut = productService.put($scope.productID, product);
                promisePut.then(function (pl) {
                    $scope.Message = "Updated Successfuly";
                    loadRecords();
                    $scope.IsNewRecord = 1;
                    $scope.productID = 0;
                    $scope.productName = "";
                    $scope.productRating = 0;
                    $scope.productDescr = "";
                    $scope.productReview = "";
                }, function (err) {
                    console.log("Err" + err);
                });
            }

            

        }

        //Method to Delete
        $scope.delete = function () {
            var promiseDelete = productService.delete($scope.productID);
            promiseDelete.then(function (pl) {
                $scope.Message = "Deleted Successfuly";
                $scope.productID = 0;
                $scope.productName = "";
                $scope.productRating = 0;
                $scope.productDescr = "";
                $scope.productReview = "";
                loadRecords();
            }, function (err) {
                console.log("Err" + err);
            });
            }
});



//app.service('productService', function ($http) {
//    //create New Record

//    this.post = function (product) {
//        var request = $http({
//            method: "post",
//            url: "/api/productAPI",
//            data: product

//        });
//        return request;
//    }

//    //Get all products
//    this.getProducts = function () {
//        return $http.get("/api/ProductAPI")

//    }

//    //Update the Record
//    this.put = function (productId, product) {
//        var request = $http({
//            method: "put",
//            url: "/api/productAPI/" + productId,
//            data: product
//        });
//        return request;
//    }
//    //Delete the Record
//    this.delete = function (productId) {
//        var request = $http({
//            method: "delete",
//            url: "/api/productAPI/" + productId
//        });
//        return request;
//    }
//})

